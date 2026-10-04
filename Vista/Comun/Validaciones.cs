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
            string v = (valor ?? "").Trim();
            if (!Regex.IsMatch(v, @"^\d{8}-\d$")) return "El DUI debe tener 9 dígitos con el formato 00000000-0.";
            if (v == "00000000-0") return "El DUI ingresado no es válido.";
            return null;
        }

        public static string Telefono(string valor)
        {
            return Regex.IsMatch((valor ?? "").Trim(), @"^[267]\d{3}-?\d{4}$")
                ? null : "El teléfono debe tener 8 dígitos y comenzar con 2, 6 o 7 (ejemplo 7890-1234).";
        }

        /// <summary>Nombre de persona o mascota: obligatorio, solo letras (sin números ni símbolos raros), mínimo 2 caracteres.</summary>
        public static string Nombre(string valor, string campo)
        {
            string v = (valor ?? "").Trim();
            if (v.Length == 0) return "El campo '" + campo + "' es obligatorio.";
            if (v.Length < 2) return "El campo '" + campo + "' debe tener al menos 2 letras.";
            if (!Regex.IsMatch(v, @"^\p{L}[\p{L} '.\-]*$"))
                return "El campo '" + campo + "' solo puede contener letras, sin números ni símbolos.";
            return null;
        }

        /// <summary>Igual que Nombre pero el campo es opcional.</summary>
        public static string NombreOpcional(string valor, string campo)
        {
            return string.IsNullOrWhiteSpace(valor) ? null : Nombre(valor, campo);
        }

        /// <summary>Texto con letras, números, espacios, punto y guion (ej. nombre de vacuna).</summary>
        public static string LetrasYNumeros(string valor, string campo)
        {
            string v = (valor ?? "").Trim();
            if (v.Length == 0) return "El campo '" + campo + "' es obligatorio.";
            if (v.Length < 2) return "El campo '" + campo + "' debe tener al menos 2 caracteres.";
            if (!Regex.IsMatch(v, @"^[\p{L}\d][\p{L}\d .\-]*$"))
                return "El campo '" + campo + "' solo puede contener letras, números, espacios, punto o guion.";
            return null;
        }

        /// <summary>Texto libre obligatorio con una longitud mínima.</summary>
        public static string Minimo(string valor, int minimo, string campo)
        {
            string v = (valor ?? "").Trim();
            if (v.Length == 0) return "El campo '" + campo + "' es obligatorio.";
            if (v.Length < minimo) return "El campo '" + campo + "' debe tener al menos " + minimo + " caracteres.";
            return null;
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
