using System.Data;

namespace Modelos
{
    public class LoginDAL : DALBase
    {
        public UsuarioSesion Autenticar(string nombreUsuario, string contrasena)
        {
            DataTable t = Consultar(
                "SELECT u.idUsuario, u.nombreCompleto, u.nombreUsuario, u.contrasena, u.correo, u.idRol, r.nombre AS rol, u.estado " +
                "FROM usuario u INNER JOIN rol r ON r.idRol = u.idRol WHERE u.nombreUsuario = @u",
                P("@u", nombreUsuario));

            if (t.Rows.Count == 0) return null;
            DataRow f = t.Rows[0];

            if (f["estado"].ToString() != "Activo") return null;
            if (!EncriptadorContrasena.Verificar(contrasena, f["contrasena"].ToString())) return null;

            UsuarioSesion usuario = new UsuarioSesion
            {
                IdUsuario = (int)f["idUsuario"],
                NombreCompleto = f["nombreCompleto"].ToString(),
                NombreUsuario = f["nombreUsuario"].ToString(),
                Correo = f["correo"] as string,
                IdRol = (int)f["idRol"],
                Rol = f["rol"].ToString(),
                Estado = f["estado"].ToString()
            };

            DataTable permisos = Consultar(
                "SELECT p.codigo FROM rolPermiso rp INNER JOIN permiso p ON p.idPermiso = rp.idPermiso WHERE rp.idRol = @r",
                P("@r", usuario.IdRol));
            foreach (DataRow p in permisos.Rows)
                usuario.Permisos.Add(p["codigo"].ToString());

            return usuario;
        }
    }
}
