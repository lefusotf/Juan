using System;
using System.Drawing;
using System.Windows.Forms;
using Vista.Comun;

namespace Vista.Vacunas
{
    /// <summary>Contenedor con pestañas tipo "pastilla": aplicación de vacunas y catálogo de vacunas.</summary>
    public partial class frmVacunas : Form
    {
        private readonly Form _aplicacion = new frmAplicacionVacunas();
        private readonly Form _catalogo = new frmCatalogoVacunas();

        public frmVacunas()
        {
            InitializeComponent();
        }

        private void frmVacunas_Load(object sender, EventArgs e)
        {
            Incrustar(_aplicacion);
            Incrustar(_catalogo);
            Mostrar(true);
        }

        private void Incrustar(Form formulario)
        {
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            pnlContenido.Controls.Add(formulario);
        }

        private void Mostrar(bool aplicacion)
        {
            _aplicacion.Visible = aplicacion;
            _catalogo.Visible = !aplicacion;

            Color activo = Tema.Primario, inactivo = Color.FromArgb(226, 232, 240);
            btnTabAplicacion.BackColor = aplicacion ? activo : inactivo;
            btnTabAplicacion.ForeColor = aplicacion ? Color.White : Tema.Texto;
            btnTabCatalogo.BackColor = aplicacion ? inactivo : activo;
            btnTabCatalogo.ForeColor = aplicacion ? Tema.Texto : Color.White;
        }

        private void btnTabAplicacion_Click(object sender, EventArgs e) { Mostrar(true); }
        private void btnTabCatalogo_Click(object sender, EventArgs e) { Mostrar(false); }
    }
}
