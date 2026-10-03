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
            Conexion.EjecutarNoQuery("DELETE FROM usuario WHERE idUsuario=@id", Conexion.P("@id", idUsuario));
        }
    }
}
