using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Vista.Comun
{
    /// <summary>Limpieza y formato del texto antes de guardarlo en la base de datos.</summary>
    public static class Texto
    {
        private static readonly string[] Particulas = { "de", "del", "la", "las", "los", "y", "e" };

        /// <summary>Quita espacios al inicio y al final y reemplaza espacios repetidos por uno solo.</summary>
        public static string Limpiar(string valor)
        {
            return Regex.Replace((valor ?? "").Trim(), @"\s+", " ");
        }

        /// <summary>Primera letra en mayúscula (para motivos, diagnósticos, etc.).</summary>
        public static string PrimeraMayuscula(string valor)
        {
            string v = Limpiar(valor);
            return v.Length == 0 ? v : char.ToUpper(v[0], new CultureInfo("es-ES")) + v.Substring(1);
        }

        /// <summary>Pone en mayúscula la inicial de cada palabra: "juan de la cruz" pasa a "Juan de la Cruz".</summary>
        public static string Capitalizar(string valor)
        {
            CultureInfo cultura = new CultureInfo("es-ES");
            string[] palabras = Limpiar(valor).ToLower(cultura).Split(' ');
            for (int i = 0; i < palabras.Length; i++)
            {
                if (palabras[i].Length == 0) continue;
                if (i > 0 && Array.IndexOf(Particulas, palabras[i]) >= 0) continue;

                StringBuilder sb = new StringBuilder(palabras[i].Length);
                bool inicio = true;
                foreach (char c in palabras[i])
                {
                    sb.Append(inicio ? char.ToUpper(c, cultura) : c);
                    inicio = c == '-' || c == '\'';
                }
                palabras[i] = sb.ToString();
            }
            return string.Join(" ", palabras);
        }
    }
}
