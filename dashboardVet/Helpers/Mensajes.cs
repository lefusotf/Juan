using System;
using System.Windows.Forms;

namespace dashboardVet.Helpers
{
    public static class Mensajes
    {
        private const string Titulo = "Sistema Veterinaria";

        public static void Info(string mensaje)
        {
            MessageBox.Show(mensaje, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Advertencia(string mensaje)
        {
            MessageBox.Show(mensaje, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static readonly System.Collections.Generic.Dictionary<Form, ErrorProvider> Proveedores =
            new System.Collections.Generic.Dictionary<Form, ErrorProvider>();

        public static bool Invalido(string error, Control foco)
        {
            if (error == null) return false;
            MarcarCampo(error, foco);
            Advertencia(error);
            if (foco != null) foco.Focus();
            return true;
        }

        private static void MarcarCampo(string error, Control foco)
        {
            if (foco == null) return;
            Form formulario = foco.FindForm();
            if (formulario == null) return;

            ErrorProvider proveedor;
            if (!Proveedores.TryGetValue(formulario, out proveedor))
            {
                proveedor = new ErrorProvider { BlinkStyle = ErrorBlinkStyle.NeverBlink };
                Proveedores[formulario] = proveedor;
                formulario.FormClosed += (s, e) => Proveedores.Remove(formulario);
            }
            proveedor.SetError(foco, error);

            EventHandler limpiar = null;
            limpiar = (s, e) =>
            {
                proveedor.SetError(foco, "");
                foco.TextChanged -= limpiar;
            };
            foco.TextChanged += limpiar;
        }

        public static bool Confirmar(string pregunta)
        {
            return MessageBox.Show(pregunta, Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}
