using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace dashboardVet.Helpers
{
    public static class Texto
    {
        private static readonly string[] Particulas = { "de", "del", "la", "las", "los", "y", "e" };

        public static string Limpiar(string valor)
        {
            return Regex.Replace((valor ?? "").Trim(), @"\s+", " ");
        }

        public static string PrimeraMayuscula(string valor)
        {
            string v = Limpiar(valor);
            return v.Length == 0 ? v : char.ToUpper(v[0], new CultureInfo("es-ES")) + v.Substring(1);
        }

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
