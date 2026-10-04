using System;
using System.Linq;
using System.Windows.Forms;

namespace Vista.Comun
{
    /// <summary>
    /// Restricciones de escritura en tiempo real: bloquea las teclas no permitidas, limpia lo que se pega
    /// y da formato automático (teléfono 0000-0000, DUI 00000000-0).
    /// </summary>
    public static class Entrada
    {
        private const string SimbolosNombre = " '.-";

        private static bool EsLetraNombre(char c) { return char.IsLetter(c) || SimbolosNombre.IndexOf(c) >= 0; }
        private static bool EsLetraONumero(char c) { return char.IsLetterOrDigit(c) || " -.".IndexOf(c) >= 0; }
        private static bool EsUsuario(char c) { return (c < 128 && char.IsLetterOrDigit(c)) || c == '.' || c == '_'; }

        /// <summary>Solo letras (con acentos y ñ), espacios, apóstrofo, punto y guion: nombres, especie, raza, color.</summary>
        public static void SoloLetras(Control c) { Restringir(c, EsLetraNombre); }

        /// <summary>Letras, números, espacios, punto y guion (ej. nombre de una vacuna).</summary>
        public static void LetrasYNumeros(Control c) { Restringir(c, EsLetraONumero); }

        /// <summary>Nombre de usuario: letras sin acentos, números, punto y guion bajo (sin espacios).</summary>
        public static void Usuario(Control c) { Restringir(c, EsUsuario); }

        /// <summary>No permite espacios (correo, contraseña).</summary>
        public static void SinEspacios(Control c) { Restringir(c, ch => !char.IsWhiteSpace(ch)); }

        /// <summary>Teléfono: solo números, máximo 8 dígitos, formato automático 0000-0000.</summary>
        public static void Telefono(TextBox t)
        {
            FormatoNumerico(t, 8, d => d.Length > 4 ? d.Substring(0, 4) + "-" + d.Substring(4) : d);
        }

        /// <summary>DUI: solo números, máximo 9 dígitos, formato automático 00000000-0.</summary>
        public static void Dui(TextBox t)
        {
            FormatoNumerico(t, 9, d => d.Length > 8 ? d.Substring(0, 8) + "-" + d.Substring(8) : d);
        }

        private static void Restringir(Control control, Func<char, bool> permitido)
        {
            control.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !permitido(e.KeyChar)) e.Handled = true;
            };

            TextBoxBase caja = control as TextBoxBase;
            if (caja == null) return;

            // Limpia también lo que se pega con el portapapeles
            caja.TextChanged += (s, e) =>
            {
                if (caja.Text.All(permitido)) return;
                int posicion = caja.SelectionStart;
                string limpio = new string(caja.Text.Where(permitido).ToArray());
                posicion -= caja.Text.Length - limpio.Length;
                caja.Text = limpio;
                caja.SelectionStart = Math.Max(0, Math.Min(posicion, limpio.Length));
            };
        }

        private static void FormatoNumerico(TextBox t, int maxDigitos, Func<string, string> formato)
        {
            t.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
            };
            t.TextChanged += (s, e) =>
            {
                string digitos = new string(t.Text.Where(char.IsDigit).ToArray());
                if (digitos.Length > maxDigitos) digitos = digitos.Substring(0, maxDigitos);
                string texto = formato(digitos);
                if (t.Text == texto) return;
                t.Text = texto;
                t.SelectionStart = t.Text.Length;
            };
        }
    }
}
