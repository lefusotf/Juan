using System;
using System.Windows.Forms;
using Modelos.Seguridad;
using Modelos.Utilidades;
using Vista.Bitacora;
using Vista.Citas;
using Vista.Comun;
using Vista.Consultas;
using Vista.Mascotas;
using Vista.Propietarios;
using Vista.Usuarios;
using Vista.Vacunas;

namespace Vista.Dashboard
{
    /// <summary>
    /// Ventana principal. El menú lateral solo muestra los módulos a los que el rol del usuario tiene permiso.
    /// </summary>
    public partial class frmDashboardPrincipal : Form
    {
        private Form activeForm = null;

        /// <summary>True si el usuario cerró sesión (se vuelve al login); false si cerró la ventana (se sale).</summary>
        public bool CerrarSesion { get; private set; }

        public frmDashboardPrincipal()
        {
            InitializeComponent();
        }

        private void frmDashboardPrincipal_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = Sesion.UsuarioActual.NombreCompleto + "\n" + Sesion.UsuarioActual.Rol;
            lblBienvenida.Text = "Bienvenido(a), " + Sesion.UsuarioActual.NombreCompleto +
                                 "\n\nSeleccione una opción del menú lateral.";

            // Cada opción del menú solo es visible si el rol tiene el permiso correspondiente
            btnPropietarios.Visible = Sesion.Tiene(Permisos.PropietariosVer);
            btnMascotas.Visible = Sesion.Tiene(Permisos.MascotasVer);
            btnCitas.Visible = Sesion.Tiene(Permisos.CitasVer);
            btnConsultas.Visible = Sesion.Tiene(Permisos.ConsultasVer);
            btnVacunas.Visible = Sesion.Tiene(Permisos.VacunasVer);
            btnUsuarios.Visible = Sesion.Tiene(Permisos.UsuariosGestionar);
            btnRoles.Visible = Sesion.Tiene(Permisos.RolesGestionar);
            btnBitacora.Visible = Sesion.Tiene(Permisos.BitacoraVer);
        }

        private void AbrirForm(Form formularioAbrir)
        {
            // Liberar el formulario anterior para no acumular memoria
            if (activeForm != null)
            {
                activeForm.Close();
                pnlContenedor.Controls.Remove(activeForm);
                activeForm.Dispose();
            }

            activeForm = formularioAbrir;
            formularioAbrir.TopLevel = false;
            formularioAbrir.FormBorderStyle = FormBorderStyle.None;
            formularioAbrir.Dock = DockStyle.Fill;

            pnlContenedor.Controls.Add(formularioAbrir);
            formularioAbrir.BringToFront();
            formularioAbrir.Show();
        }

        private void btnPropietarios_Click(object sender, EventArgs e) { AbrirForm(new frmPropietarios()); }
        private void btnMascotas_Click(object sender, EventArgs e) { AbrirForm(new frmMascotas()); }
        private void btnCitas_Click(object sender, EventArgs e) { AbrirForm(new frmCitas()); }
        private void btnConsultas_Click(object sender, EventArgs e) { AbrirForm(new frmConsultas()); }
        private void btnVacunas_Click(object sender, EventArgs e) { AbrirForm(new frmVacunas()); }
        private void btnUsuarios_Click(object sender, EventArgs e) { AbrirForm(new frmUsuarios()); }
        private void btnRoles_Click(object sender, EventArgs e) { AbrirForm(new frmRoles()); }
        private void btnBitacora_Click(object sender, EventArgs e) { AbrirForm(new frmBitacora()); }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (!Mensajes.Confirmar("¿Desea cerrar la sesión?")) return;
            Logger.Info("Login", "Cierre de sesión");
            Sesion.Cerrar();
            CerrarSesion = true;
            Close();
        }
    }
}
