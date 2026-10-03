using System.Text.RegularExpressions;

namespace Vista.Comun
{
    /// <summary>Validaciones de entrada. Cada método devuelve un mensaje de error o null si el valor es válido.</summary>
    public static class Validaciones
    {
        public static string Requerido(string valor, string campo)
        {
            return string.IsNullOrWhiteSpace(valor) ? "El campo '" + campo + "' es obligatorio." : null;
        }

        public static string LongitudMaxima(string valor, int max, string campo)
        {
            return valor != null && valor.Trim().Length > max
                ? "El campo '" + campo + "' no puede superar " + max + " caracteres." : null;
        }

        public static string Dui(string valor)
        {
            return Regex.IsMatch((valor ?? "").Trim(), @"^\d{8}-\d$")
                ? null : "El DUI debe tener el formato 00000000-0.";
        }

        public static string Telefono(string valor)
        {
            return Regex.IsMatch((valor ?? "").Trim(), @"^\d{4}-?\d{4}$")
                ? null : "El teléfono debe tener 8 dígitos (ejemplo 7890-1234).";
        }

        /// <summary>El correo es opcional; si se escribe debe tener formato válido.</summary>
        public static string Correo(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor)) return null;
            return Regex.IsMatch(valor.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                ? null : "El correo electrónico no tiene un formato válido.";
        }

        public static string NombreUsuario(string valor)
        {
            return Regex.IsMatch((valor ?? "").Trim(), @"^[A-Za-z0-9_.]{4,30}$")
                ? null : "El usuario debe tener de 4 a 30 caracteres (letras, números, punto o guion bajo).";
        }

        public static string Contrasena(string valor)
        {
            if (valor == null || valor.Length < 8 || !Regex.IsMatch(valor, @"[A-Z]") ||
                !Regex.IsMatch(valor, @"[a-z]") || !Regex.IsMatch(valor, @"\d"))
                return "La contraseña debe tener mínimo 8 caracteres, con mayúscula, minúscula y número.";
            return null;
        }
    }
}
