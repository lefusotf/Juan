using System.Drawing;
using System.Windows.Forms;
using Vista.Comun;

namespace Vista.Vacunas
{
    /// <summary>Contenedor con TabControl: catálogo de vacunas y aplicación de vacunas a mascotas.</summary>
    public class frmVacunas : Form
    {
        public frmVacunas()
        {
            Font = Estilo.Fuente;
            BackColor = Estilo.Fondo;
            Text = "Vacunas";

            TabControl tabs = new TabControl { Dock = DockStyle.Fill };
            tabs.TabPages.Add(CrearPestana("Aplicación de vacunas", new frmAplicacionVacunas()));
            tabs.TabPages.Add(CrearPestana("Catálogo de vacunas", new frmCatalogoVacunas()));
            Controls.Add(tabs);
        }

        private static TabPage CrearPestana(string titulo, Form formulario)
        {
            TabPage pagina = new TabPage(titulo) { BackColor = Estilo.Fondo };
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            pagina.Controls.Add(formulario);
            formulario.Show();
            return pagina;
        }
    }
}
