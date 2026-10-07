using System.Data;

namespace Modelos
{
    public class UsuarioDAL : DALBase
    {
        public DataTable Listar(string filtro)
        {
            return Consultar(
                "SELECT idUsuario, nombreCompleto, nombreUsuario, correo, idRol, rol, estado, fechaCreacion " +
                "FROM vw_UsuariosDetalle " +
                "WHERE nombreCompleto LIKE @f OR nombreUsuario LIKE @f OR rol LIKE @f ORDER BY nombreCompleto",
                Like("@f", filtro));
        }

        public DataTable ListarVeterinarios()
        {
            return Consultar(
                "SELECT u.idUsuario, u.nombreCompleto FROM usuario u INNER JOIN rol r ON r.idRol = u.idRol " +
                "WHERE r.nombre = 'Veterinario' AND u.estado = 'Activo' ORDER BY u.nombreCompleto");
        }

        public bool ExisteNombreUsuario(string nombreUsuario, int idExcluir)
        {
            return Existe("SELECT COUNT(*) FROM usuario WHERE nombreUsuario=@u AND idUsuario<>@id",
                P("@u", nombreUsuario), P("@id", idExcluir));
        }

        public bool ExisteCorreo(string correo, int idExcluir)
        {
            return Existe("SELECT COUNT(*) FROM usuario WHERE correo=@c AND idUsuario<>@id",
                P("@c", correo), P("@id", idExcluir));
        }

        private void ProtegerUltimoAdministrador(int idUsuario, int? nuevoIdRol, string nuevoEstado, bool eliminando)
        {
            DataTable t = Consultar(
                "SELECT u.estado, r.nombre FROM usuario u INNER JOIN rol r ON r.idRol = u.idRol WHERE u.idUsuario=@id",
                P("@id", idUsuario));
            if (t.Rows.Count == 0) return;

            bool eraAdminActivo = t.Rows[0]["nombre"].ToString() == "Administrador" && t.Rows[0]["estado"].ToString() == "Activo";
            if (!eraAdminActivo) return;

            if (!eliminando && nuevoIdRol.HasValue)
            {
                object nombreRol = Escalar("SELECT nombre FROM rol WHERE idRol=@r", P("@r", nuevoIdRol.Value));
                if (nombreRol != null && nombreRol.ToString() == "Administrador" && nuevoEstado == "Activo") return;
            }

            int otros = (int)Escalar(
                "SELECT COUNT(*) FROM usuario u INNER JOIN rol r ON r.idRol = u.idRol " +
                "WHERE r.nombre='Administrador' AND u.estado='Activo' AND u.idUsuario<>@id",
                P("@id", idUsuario));
            if (otros == 0)
                throw new AppException("ERR-NEG-001", "Debe existir al menos un administrador activo en el sistema.");
        }

        public void Insertar(string nombreCompleto, string nombreUsuario, string contrasena, string correo, int idRol,
            string estado)
        {
            Ejecutar(
                "INSERT INTO usuario (nombreCompleto, nombreUsuario, contrasena, correo, idRol, estado) " +
                "VALUES (@nombre, @usuario, @clave, @correo, @rol, @estado)",
                P("@nombre", nombreCompleto), P("@usuario", nombreUsuario),
                P("@clave", EncriptadorContrasena.Hashear(contrasena)),
                P("@correo", correo), P("@rol", idRol), P("@estado", estado));
        }

        public void Actualizar(int idUsuario, string nombreCompleto, string nombreUsuario, string contrasena,
            string correo, int idRol, string estado)
        {
            ProtegerUltimoAdministrador(idUsuario, idRol, estado, false);

            if (string.IsNullOrEmpty(contrasena))
            {
                Ejecutar(
                    "UPDATE usuario SET nombreCompleto=@nombre, nombreUsuario=@usuario, correo=@correo, idRol=@rol, estado=@estado " +
                    "WHERE idUsuario=@id",
                    P("@nombre", nombreCompleto), P("@usuario", nombreUsuario), P("@correo", correo),
                    P("@rol", idRol), P("@estado", estado), P("@id", idUsuario));
            }
            else
            {
                Ejecutar(
                    "UPDATE usuario SET nombreCompleto=@nombre, nombreUsuario=@usuario, contrasena=@clave, correo=@correo, " +
                    "idRol=@rol, estado=@estado WHERE idUsuario=@id",
                    P("@nombre", nombreCompleto), P("@usuario", nombreUsuario),
                    P("@clave", EncriptadorContrasena.Hashear(contrasena)),
                    P("@correo", correo), P("@rol", idRol), P("@estado", estado), P("@id", idUsuario));
            }
        }

        public void Eliminar(int idUsuario)
        {
            ProtegerUltimoAdministrador(idUsuario, null, null, true);

            DataRow r = Consultar(
                "SELECT (SELECT COUNT(*) FROM cita WHERE idVeterinario=@id OR idUsuarioRegistro=@id) AS citas, " +
                "(SELECT COUNT(*) FROM consulta WHERE idVeterinario=@id) AS consultas, " +
                "(SELECT COUNT(*) FROM aplicacionVacuna WHERE idVeterinario=@id) AS vacunas",
                P("@id", idUsuario)).Rows[0];
            if ((int)r["citas"] + (int)r["consultas"] + (int)r["vacunas"] > 0)
                throw new AppException("ERR-NEG-001",
                    "No se puede eliminar al usuario porque tiene citas, consultas o vacunas registradas a su nombre. " +
                    "Para que no pueda entrar al sistema, cambie su estado a Inactivo.");

            Ejecutar("DELETE FROM usuario WHERE idUsuario=@id", P("@id", idUsuario));
        }
    }
}
