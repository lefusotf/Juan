using System;
using System.Windows.Forms;

namespace Vista.Vacunas
{
    /// <summary>Contenedor con TabControl: aplicación de vacunas a mascotas y catálogo de vacunas.</summary>
    public partial class frmVacunas : Form
    {
        public frmVacunas()
        {
            InitializeComponent();
        }

        private void frmVacunas_Load(object sender, EventArgs e)
        {
            // Cada pestaña muestra un formulario incrustado
            Incrustar(tabAplicacion, new frmAplicacionVacunas());
            Incrustar(tabCatalogo, new frmCatalogoVacunas());
        }

        private static void Incrustar(TabPage pagina, Form formulario)
        {
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            pagina.Controls.Add(formulario);
            formulario.Show();
        }
    }
}
