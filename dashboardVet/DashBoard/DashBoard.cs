using System;
using System.Globalization;
using System.Windows.Forms;
using dashboardVet.Base;
using dashboardVet.Controles;
using dashboardVet.Helpers;
using Modelos;

namespace dashboardVet.DashBoard
{
    public partial class DashBoard : FrmBase
    {
        private const int AnchoMenu = 250;
        private const int AnchoMenuCompacto = 72;
        private const int UmbralCompacto = 1100;

        private Control activeControl = null;
        private BotonMenu[] _opciones;
        private readonly ToolTip _tip = new ToolTip();
        private bool _compacto;
        private bool _ventanaPequena;

        public bool CerrarSesion { get; private set; }

        public DashBoard()
        {
            InitializeComponent();
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            _opciones = new BotonMenu[]
            {
                btnInicio, btnPropietarios, btnMascotas, btnCitas, btnConsultas,
                btnVacunas, btnUsuarios, btnRoles, btnBitacora, btnSalir
            };

            lblUsuario.Text = Sesion.UsuarioActual.NombreCompleto + "\n" + Sesion.UsuarioActual.Rol;
            string fecha = DateTime.Now.ToString("dddd, d 'de' MMMM 'de' yyyy", new CultureInfo("es-ES"));
            lblFecha.Text = char.ToUpper(fecha[0]) + fecha.Substring(1);

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

            Abrir(new Inicio.Inicio(), btnInicio, "Inicio");
        }

        private void DashBoard_Resize(object sender, EventArgs e)
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

        private void Abrir(Control nuevo, BotonMenu boton, string titulo)
        {
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

        private void btnInicio_Click(object sender, EventArgs e) { Abrir(new Inicio.Inicio(), btnInicio, "Inicio"); }
        private void btnPropietarios_Click(object sender, EventArgs e) { Abrir(new Propietarios.Propietarios(), btnPropietarios, "Propietarios"); }
        private void btnMascotas_Click(object sender, EventArgs e) { Abrir(new Mascotas.Mascotas(), btnMascotas, "Mascotas"); }
        private void btnCitas_Click(object sender, EventArgs e) { Abrir(new Citas.Citas(), btnCitas, "Citas médicas"); }
        private void btnConsultas_Click(object sender, EventArgs e) { Abrir(new Consultas.Consultas(), btnConsultas, "Historial médico"); }
        private void btnVacunas_Click(object sender, EventArgs e) { Abrir(new Vacunas.Vacunas(), btnVacunas, "Vacunas"); }
        private void btnUsuarios_Click(object sender, EventArgs e) { Abrir(new Usuarios.Usuarios(), btnUsuarios, "Usuarios"); }
        private void btnRoles_Click(object sender, EventArgs e) { Abrir(new Roles.Roles(), btnRoles, "Roles y permisos"); }
        private void btnBitacora_Click(object sender, EventArgs e) { Abrir(new Bitacora.Bitacora(), btnBitacora, "Bitácora"); }

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
