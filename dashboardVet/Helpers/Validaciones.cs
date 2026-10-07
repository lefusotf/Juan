using System;
using System.Text.RegularExpressions;

namespace dashboardVet.Helpers
{
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

        public static bool DuiDigitoVerificadorValido(string valor)
        {
            string v = (valor ?? "").Trim();
            if (!Regex.IsMatch(v, @"^\d{8}-\d$")) return false;
            int suma = 0;
            for (int i = 0; i < 8; i++) suma += (v[i] - '0') * (9 - i);
            int esperado = (10 - (suma % 10)) % 10;
            return esperado == v[9] - '0';
        }

        public static string Telefono(string valor)
        {
            string v = (valor ?? "").Trim();
            if (!Regex.IsMatch(v, @"^[267]\d{3}-?\d{4}$"))
                return "El teléfono debe tener 8 dígitos y comenzar con 2, 6 o 7 (ejemplo 7890-1234).";
            if (Regex.IsMatch(v.Replace("-", ""), @"^(\d)\1{7}$"))
                return "El teléfono ingresado no es válido (no puede repetir el mismo dígito).";
            return null;
        }

        public static string NombreCompleto(string valor, string campo)
        {
            string error = Nombre(valor, campo);
            if (error != null) return error;
            if (Texto.Limpiar(valor).Split(' ').Length < 2)
                return "Ingrese nombre y apellido en el campo '" + campo + "'.";
            return null;
        }

        public static string Nombre(string valor, string campo)
        {
            string v = (valor ?? "").Trim();
            if (v.Length == 0) return "El campo '" + campo + "' es obligatorio.";
            if (v.Length < 2) return "El campo '" + campo + "' debe tener al menos 2 letras.";
            if (!Regex.IsMatch(v, @"^\p{L}[\p{L} '.\-]*$"))
                return "El campo '" + campo + "' solo puede contener letras, sin números ni símbolos.";
            if (Regex.IsMatch(v, @"(\p{L})\1{3,}", RegexOptions.IgnoreCase))
                return "El campo '" + campo + "' no parece válido (tiene la misma letra repetida demasiadas veces).";
            return null;
        }

        public static string NombreOpcional(string valor, string campo)
        {
            return string.IsNullOrWhiteSpace(valor) ? null : Nombre(valor, campo);
        }

        public static string LetrasYNumeros(string valor, string campo)
        {
            string v = (valor ?? "").Trim();
            if (v.Length == 0) return "El campo '" + campo + "' es obligatorio.";
            if (v.Length < 2) return "El campo '" + campo + "' debe tener al menos 2 caracteres.";
            if (!Regex.IsMatch(v, @"^[\p{L}\d][\p{L}\d .\-]*$"))
                return "El campo '" + campo + "' solo puede contener letras, números, espacios, punto o guion.";
            return null;
        }

        public static string Minimo(string valor, int minimo, string campo)
        {
            string v = (valor ?? "").Trim();
            if (v.Length == 0) return "El campo '" + campo + "' es obligatorio.";
            if (v.Length < minimo) return "El campo '" + campo + "' debe tener al menos " + minimo + " caracteres.";
            return null;
        }

        public static string Correo(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor)) return null;
            string v = valor.Trim();
            if (v.Length > 150) return "El correo electrónico no puede superar 150 caracteres.";
            if (v.Contains("..") || !Regex.IsMatch(v, @"^[A-Za-z0-9._%+\-]+@[A-Za-z0-9\-]+(\.[A-Za-z0-9\-]+)*\.[A-Za-z]{2,}$"))
                return "El correo electrónico no tiene un formato válido (ejemplo nombre@dominio.com).";
            return null;
        }

        public static string NombreUsuario(string valor)
        {
            return Regex.IsMatch((valor ?? "").Trim(), @"^[A-Za-z0-9_.]{4,30}$")
                ? null : "El usuario debe tener de 4 a 30 caracteres (letras, números, punto o guion bajo).";
        }

        public static string Contrasena(string valor, string usuario)
        {
            string error = Contrasena(valor);
            if (error != null) return error;
            if (valor.Length > 50) return "La contraseña no puede superar 50 caracteres.";
            if (!string.IsNullOrWhiteSpace(usuario) && usuario.Trim().Length >= 3 &&
                valor.IndexOf(usuario.Trim(), StringComparison.OrdinalIgnoreCase) >= 0)
                return "La contraseña no puede contener el nombre de usuario.";
            if (Regex.IsMatch(valor, @"(.)\1{3,}"))
                return "La contraseña no puede repetir el mismo carácter 4 veces seguidas.";
            string minuscula = valor.ToLowerInvariant();
            foreach (string comun in new[] { "123456", "qwerty", "password", "contrasena", "abcdef" })
                if (minuscula.Contains(comun)) return "La contraseña es demasiado común o predecible. Elija otra.";
            return null;
        }

        public static string HorarioCita(DateTime fechaHora)
        {
            if (fechaHora.Hour < 7 || fechaHora.Hour >= 18)
                return "Las citas se atienden de 7:00 a.m. a 6:00 p.m. Elija una hora dentro del horario.";
            if (fechaHora > DateTime.Now.AddYears(1))
                return "No se pueden agendar citas con más de un año de anticipación.";
            return null;
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
