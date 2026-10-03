using System.Collections.Generic;

namespace Modelos.Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }   // texto plano solo al crear/cambiar; en BD es hash BCrypt
        public string Correo { get; set; }
        public int IdRol { get; set; }
        public string Rol { get; set; }
        public string Estado { get; set; }
        public HashSet<string> Permisos { get; set; } = new HashSet<string>();
    }
}
