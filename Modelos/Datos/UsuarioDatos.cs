using System.Data;
using Modelos.Conexion_DB;
using Modelos.Entidades;
using Modelos.Seguridad;

namespace Modelos.Datos
{
    public static class UsuarioDatos
    {
        /// <summary>Valida credenciales con BCrypt. Devuelve el usuario con sus permisos o null si son incorrectas.</summary>
        public static Usuario Autenticar(string nombreUsuario, string contrasena)
        {
            DataTable t = Conexion.Consultar(
                "SELECT u.idUsuario, u.nombreCompleto, u.nombreUsuario, u.contrasena, u.correo, u.idRol, r.nombre AS rol, u.estado " +
                "FROM usuario u INNER JOIN rol r ON r.idRol = u.idRol WHERE u.nombreUsuario = @u",
                Conexion.P("@u", nombreUsuario));

            if (t.Rows.Count == 0) return null;
            DataRow f = t.Rows[0];

            if (f["estado"].ToString() != "Activo") return null;
            if (!EncriptadorContrasena.Verificar(contrasena, f["contrasena"].ToString())) return null;

            Usuario usuario = new Usuario
            {
                IdUsuario = (int)f["idUsuario"],
                NombreCompleto = f["nombreCompleto"].ToString(),
                NombreUsuario = f["nombreUsuario"].ToString(),
                Correo = f["correo"] as string,
                IdRol = (int)f["idRol"],
                Rol = f["rol"].ToString(),
                Estado = f["estado"].ToString()
            };

            DataTable permisos = Conexion.Consultar(
                "SELECT p.codigo FROM rolPermiso rp INNER JOIN permiso p ON p.idPermiso = rp.idPermiso WHERE rp.idRol = @r",
                Conexion.P("@r", usuario.IdRol));
            foreach (DataRow p in permisos.Rows)
                usuario.Permisos.Add(p["codigo"].ToString());

            return usuario;
        }

        public static DataTable Listar(string filtro)
        {
            return Conexion.Consultar(
                "SELECT idUsuario, nombreCompleto, nombreUsuario, correo, idRol, rol, estado, fechaCreacion " +
                "FROM vw_UsuariosDetalle " +
                "WHERE nombreCompleto LIKE @f OR nombreUsuario LIKE @f OR rol LIKE @f ORDER BY nombreCompleto",
                Conexion.P("@f", "%" + (filtro ?? "") + "%"));
        }

        public static DataTable ListarVeterinarios()
        {
            return Conexion.Consultar(
                "SELECT u.idUsuario, u.nombreCompleto FROM usuario u INNER JOIN rol r ON r.idRol = u.idRol " +
                "WHERE r.nombre = 'Veterinario' AND u.estado = 'Activo' ORDER BY u.nombreCompleto");
        }

        public static bool ExisteNombreUsuario(string nombreUsuario, int idExcluir)
        {
            return (int)Conexion.Escalar("SELECT COUNT(*) FROM usuario WHERE nombreUsuario=@u AND idUsuario<>@id",
                Conexion.P("@u", nombreUsuario), Conexion.P("@id", idExcluir)) > 0;
        }

        public static bool ExisteCorreo(string correo, int idExcluir)
        {
            return (int)Conexion.Escalar("SELECT COUNT(*) FROM usuario WHERE correo=@c AND idUsuario<>@id",
                Conexion.P("@c", correo), Conexion.P("@id", idExcluir)) > 0;
        }

        /// <summary>
        /// Regla de negocio: siempre debe quedar al menos un administrador activo. Lanza una excepción si la
        /// operación (eliminar, desactivar o cambiar de rol) dejaría al sistema sin administradores.
        /// </summary>
        private static void ProtegerUltimoAdministrador(int idUsuario, int? nuevoIdRol, string nuevoEstado, bool eliminando)
        {
            DataTable t = Conexion.Consultar(
                "SELECT u.estado, r.nombre FROM usuario u INNER JOIN rol r ON r.idRol = u.idRol WHERE u.idUsuario=@id",
                Conexion.P("@id", idUsuario));
            if (t.Rows.Count == 0) return;

            bool eraAdminActivo = t.Rows[0]["nombre"].ToString() == "Administrador" && t.Rows[0]["estado"].ToString() == "Activo";
            if (!eraAdminActivo) return;

            if (!eliminando && nuevoIdRol.HasValue)
            {
                object nombreRol = Conexion.Escalar("SELECT nombre FROM rol WHERE idRol=@r", Conexion.P("@r", nuevoIdRol.Value));
                if (nombreRol != null && nombreRol.ToString() == "Administrador" && nuevoEstado == "Activo") return;
            }

            int otros = (int)Conexion.Escalar(
                "SELECT COUNT(*) FROM usuario u INNER JOIN rol r ON r.idRol = u.idRol " +
                "WHERE r.nombre='Administrador' AND u.estado='Activo' AND u.idUsuario<>@id",
                Conexion.P("@id", idUsuario));
            if (otros == 0)
                throw new System.InvalidOperationException("Debe existir al menos un administrador activo en el sistema.");
        }

        public static void Insertar(Usuario u)
        {
            Conexion.EjecutarNoQuery(
                "INSERT INTO usuario (nombreCompleto, nombreUsuario, contrasena, correo, idRol, estado) " +
                "VALUES (@nombre, @usuario, @clave, @correo, @rol, @estado)",
                Conexion.P("@nombre", u.NombreCompleto),
                Conexion.P("@usuario", u.NombreUsuario),
                Conexion.P("@clave", EncriptadorContrasena.Hashear(u.Contrasena)),
                Conexion.P("@correo", u.Correo),
                Conexion.P("@rol", u.IdRol),
                Conexion.P("@estado", u.Estado));
        }

        /// <summary>Actualiza al usuario. Si Contrasena viene vacía se conserva la actual.</summary>
        public static void Actualizar(Usuario u)
        {
            ProtegerUltimoAdministrador(u.IdUsuario, u.IdRol, u.Estado, false);

            if (string.IsNullOrEmpty(u.Contrasena))
            {
                Conexion.EjecutarNoQuery(
                    "UPDATE usuario SET nombreCompleto=@nombre, nombreUsuario=@usuario, correo=@correo, idRol=@rol, estado=@estado " +
                    "WHERE idUsuario=@id",
                    Conexion.P("@nombre", u.NombreCompleto),
                    Conexion.P("@usuario", u.NombreUsuario),
                    Conexion.P("@correo", u.Correo),
                    Conexion.P("@rol", u.IdRol),
                    Conexion.P("@estado", u.Estado),
                    Conexion.P("@id", u.IdUsuario));
            }
            else
            {
                Conexion.EjecutarNoQuery(
                    "UPDATE usuario SET nombreCompleto=@nombre, nombreUsuario=@usuario, contrasena=@clave, correo=@correo, " +
                    "idRol=@rol, estado=@estado WHERE idUsuario=@id",
                    Conexion.P("@nombre", u.NombreCompleto),
                    Conexion.P("@usuario", u.NombreUsuario),
                    Conexion.P("@clave", EncriptadorContrasena.Hashear(u.Contrasena)),
                    Conexion.P("@correo", u.Correo),
                    Conexion.P("@rol", u.IdRol),
                    Conexion.P("@estado", u.Estado),
                    Conexion.P("@id", u.IdUsuario));
            }
        }

        public static void Eliminar(int idUsuario)
        {
            ProtegerUltimoAdministrador(idUsuario, null, null, true);

            DataRow r = Conexion.Consultar(
                "SELECT (SELECT COUNT(*) FROM cita WHERE idVeterinario=@id OR idUsuarioRegistro=@id) AS citas, " +
                "(SELECT COUNT(*) FROM consulta WHERE idVeterinario=@id) AS consultas, " +
                "(SELECT COUNT(*) FROM aplicacionVacuna WHERE idVeterinario=@id) AS vacunas",
                Conexion.P("@id", idUsuario)).Rows[0];
            if ((int)r["citas"] + (int)r["consultas"] + (int)r["vacunas"] > 0)
                throw new System.InvalidOperationException("No se puede eliminar al usuario porque tiene citas, consultas o vacunas " +
                    "registradas a su nombre. Para que no pueda entrar al sistema, cambie su estado a Inactivo.");

            Conexion.EjecutarNoQuery("DELETE FROM usuario WHERE idUsuario=@id", Conexion.P("@id", idUsuario));
        }
    }
}
