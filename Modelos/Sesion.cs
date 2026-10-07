using System.Collections.Generic;

namespace Modelos
{
    public class UsuarioSesion
    {
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string NombreUsuario { get; set; }
        public string Correo { get; set; }
        public int IdRol { get; set; }
        public string Rol { get; set; }
        public string Estado { get; set; }
        public HashSet<string> Permisos { get; set; } = new HashSet<string>();
    }

    public static class Sesion
    {
        public static UsuarioSesion UsuarioActual { get; private set; }

        public static bool HaySesion { get { return UsuarioActual != null; } }

        public static void Iniciar(UsuarioSesion usuario)
        {
            UsuarioActual = usuario;
        }

        public static void Cerrar()
        {
            UsuarioActual = null;
        }

        public static bool Tiene(string codigoPermiso)
        {
            return UsuarioActual != null && UsuarioActual.Permisos.Contains(codigoPermiso);
        }

        public static bool TieneAlguno(params string[] codigos)
        {
            foreach (string c in codigos)
                if (Tiene(c)) return true;
            return false;
        }
    }

    public static class Permisos
    {
        public const string UsuariosGestionar = "USUARIOS_GESTIONAR";
        public const string RolesGestionar = "ROLES_GESTIONAR";
        public const string BitacoraVer = "BITACORA_VER";
        public const string PropietariosVer = "PROPIETARIOS_VER";
        public const string PropietariosGestionar = "PROPIETARIOS_GESTIONAR";
        public const string MascotasVer = "MASCOTAS_VER";
        public const string MascotasGestionar = "MASCOTAS_GESTIONAR";
        public const string CitasVer = "CITAS_VER";
        public const string CitasGestionar = "CITAS_GESTIONAR";
        public const string ConsultasVer = "CONSULTAS_VER";
        public const string ConsultasGestionar = "CONSULTAS_GESTIONAR";
        public const string VacunasVer = "VACUNAS_VER";
        public const string VacunasGestionar = "VACUNAS_GESTIONAR";
    }
}
