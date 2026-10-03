using System;
using System.Globalization;
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
    /// Ventana principal. El menú lateral solo muestra los módulos a los que el rol del usuario tiene permiso
    /// y se contrae automáticamente (solo iconos) cuando la ventana es angosta.
    /// </summary>
    public partial class frmDashboardPrincipal : Form
    {
        private const int AnchoMenu = 250;
        private const int AnchoMenuCompacto = 72;
        private const int UmbralCompacto = 1100;

        private Control activeControl = null;
        private BotonMenu[] _opciones;
        private readonly ToolTip _tip = new ToolTip();
        private bool _compacto;
        private bool _ventanaPequena;

        /// <summary>True si el usuario cerró sesión (se vuelve al login); false si cerró la ventana (se sale).</summary>
        public bool CerrarSesion { get; private set; }

        public frmDashboardPrincipal()
        {
            InitializeComponent();
        }

        private void frmDashboardPrincipal_Load(object sender, EventArgs e)
        {
            _opciones = new BotonMenu[]
            {
                btnInicio, btnPropietarios, btnMascotas, btnCitas, btnConsultas,
                btnVacunas, btnUsuarios, btnRoles, btnBitacora, btnSalir
            };

            lblUsuario.Text = Sesion.UsuarioActual.NombreCompleto + "\n" + Sesion.UsuarioActual.Rol;
            string fecha = DateTime.Now.ToString("dddd, d 'de' MMMM 'de' yyyy", new CultureInfo("es-ES"));
            lblFecha.Text = char.ToUpper(fecha[0]) + fecha.Substring(1);

            // Cada opción del menú solo es visible si el rol tiene el permiso correspondiente
            btnPropietarios.Visible = Sesion.Tiene(Permisos.PropietariosVer);
            btnMascotas.Visible = Sesion.Tiene(Permisos.MascotasVer);
            btnCitas.Visible = Sesion.Tiene(Permisos.CitasVer);
            btnConsultas.Visible = Sesion.Tiene(Permisos.ConsultasVer);
            btnVacunas.Visible = Sesion.Tiene(Permisos.VacunasVer);
            btnUsuarios.Visible = Sesion.Tiene(Permisos.UsuariosGestionar);
            btnRoles.Visible = Sesion.Tiene(Permisos.RolesGestionar);
            btnBitacora.Visible = Sesion.Tiene(Permisos.BitacoraVer);

            _ventanaPequena = ClientSize.Width < UmbralCompacto;
            _compacto = _ventanaPequena;
            AplicarMenu();

            Abrir(new ucInicio(), btnInicio, "Inicio");
        }

        // Diseño adaptable: contrae el menú cuando la ventana es angosta
        private void frmDashboardPrincipal_Resize(object sender, EventArgs e)
        {
            if (_opciones == null) return;
            bool pequena = ClientSize.Width < UmbralCompacto;
            if (pequena == _ventanaPequena) return;
            _ventanaPequena = pequena;
            _compacto = pequena;
            AplicarMenu();
        }

        private void AplicarMenu()
        {
            pnlMenu.Width = _compacto ? AnchoMenuCompacto : AnchoMenu;
            lblApp.Text = _compacto ? "\U0001F43E" : "\U0001F43E  VetCare";
            lblUsuario.Visible = !_compacto;
            foreach (BotonMenu b in _opciones)
            {
                b.Compacto = _compacto;
                _tip.SetToolTip(b, _compacto ? b.Text : "");
            }
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            _compacto = !_compacto;
            AplicarMenu();
        }

        /// <summary>Muestra un formulario o control dentro del panel de contenido y resalta su opción del menú.</summary>
        private void Abrir(Control nuevo, BotonMenu boton, string titulo)
        {
            // Liberar el contenido anterior para no acumular memoria
            if (activeControl != null)
            {
                pnlContenedor.Controls.Remove(activeControl);
                activeControl.Dispose();
            }

            Form formulario = nuevo as Form;
            if (formulario != null)
            {
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
            }

            activeControl = nuevo;
            nuevo.Dock = DockStyle.Fill;
            pnlContenedor.Controls.Add(nuevo);
            nuevo.BringToFront();
            nuevo.Show();

            foreach (BotonMenu b in _opciones) b.Activo = false;
            boton.Activo = true;
            lblSeccion.Text = titulo;
        }

        private void btnInicio_Click(object sender, EventArgs e) { Abrir(new ucInicio(), btnInicio, "Inicio"); }
        private void btnPropietarios_Click(object sender, EventArgs e) { Abrir(new frmPropietarios(), btnPropietarios, "Propietarios"); }
        private void btnMascotas_Click(object sender, EventArgs e) { Abrir(new frmMascotas(), btnMascotas, "Mascotas"); }
        private void btnCitas_Click(object sender, EventArgs e) { Abrir(new frmCitas(), btnCitas, "Citas médicas"); }
        private void btnConsultas_Click(object sender, EventArgs e) { Abrir(new frmConsultas(), btnConsultas, "Historial médico"); }
        private void btnVacunas_Click(object sender, EventArgs e) { Abrir(new frmVacunas(), btnVacunas, "Vacunas"); }
        private void btnUsuarios_Click(object sender, EventArgs e) { Abrir(new frmUsuarios(), btnUsuarios, "Usuarios"); }
        private void btnRoles_Click(object sender, EventArgs e) { Abrir(new frmRoles(), btnRoles, "Roles y permisos"); }
        private void btnBitacora_Click(object sender, EventArgs e) { Abrir(new frmBitacora(), btnBitacora, "Bitácora"); }

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
