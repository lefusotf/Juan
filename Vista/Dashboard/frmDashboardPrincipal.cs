using System;
using System.Drawing;
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
    public class frmDashboardPrincipal : Form
    {
        private readonly Panel pnlMenu = new Panel();
        private readonly Panel pnlContenedor = new Panel();
        private Form activeForm = null;

        /// <summary>True si el usuario cerró sesión (se vuelve al login); false si cerró la ventana (se sale).</summary>
        public bool CerrarSesion { get; private set; }

        public frmDashboardPrincipal()
        {
            Text = "Sistema de Gestión Veterinaria";
            Font = Estilo.Fuente;
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(1000, 640);
            StartPosition = FormStartPosition.CenterScreen;
            ConstruirMenu();

            pnlContenedor.Dock = DockStyle.Fill;
            pnlContenedor.BackColor = Estilo.Fondo;
            Controls.Add(pnlContenedor);
            Controls.Add(pnlMenu);
            Load += (s, e) => MostrarBienvenida();
        }

        private void ConstruirMenu()
        {
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Width = 230;
            pnlMenu.BackColor = Estilo.PrimarioOscuro;

            Label lblApp = new Label
            {
                Text = "Veterinaria",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label lblUsuario = new Label
            {
                Text = Sesion.UsuarioActual.NombreCompleto + "\n" + Sesion.UsuarioActual.Rol,
                ForeColor = Color.FromArgb(200, 230, 225),
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Se agregan en orden inverso porque Dock=Top apila el último agregado arriba
            Button btnSalir = CrearBoton("Cerrar sesión", null, Estilo.Peligro);
            btnSalir.Dock = DockStyle.Bottom;
            btnSalir.Click += (s, e) => CerrarSesionUsuario();

            Control[] opciones =
            {
                CrearOpcion("Usuarios", Permisos.UsuariosGestionar, () => new frmUsuarios()),
                CrearOpcion("Roles y permisos", Permisos.RolesGestionar, () => new frmRoles()),
                CrearOpcion("Bitácora", Permisos.BitacoraVer, () => new frmBitacora()),
                CrearOpcion("Vacunas", Permisos.VacunasVer, () => new frmVacunas()),
                CrearOpcion("Consultas médicas", Permisos.ConsultasVer, () => new frmConsultas()),
                CrearOpcion("Citas", Permisos.CitasVer, () => new frmCitas()),
                CrearOpcion("Mascotas", Permisos.MascotasVer, () => new frmMascotas()),
                CrearOpcion("Propietarios", Permisos.PropietariosVer, () => new frmPropietarios())
            };

            pnlMenu.Controls.Add(btnSalir);
            foreach (Control c in opciones)
                if (c != null) pnlMenu.Controls.Add(c);
            pnlMenu.Controls.Add(lblUsuario);
            pnlMenu.Controls.Add(lblApp);
        }

        private Button CrearBoton(string texto, string permiso, Color fondo)
        {
            Button b = new Button
            {
                Text = texto,
                Dock = DockStyle.Top,
                Height = 46,
                FlatStyle = FlatStyle.Flat,
                BackColor = fondo,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(16, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            b.FlatAppearance.BorderSize = 0;
            return b;
        }

        /// <summary>Crea la opción de menú solo si el usuario tiene el permiso; si no, devuelve null.</summary>
        private Control CrearOpcion(string texto, string permiso, Func<Form> crearFormulario)
        {
            if (!Sesion.Tiene(permiso)) return null;
            Button b = CrearBoton(texto, permiso, Estilo.PrimarioOscuro);
            b.Click += (s, e) => AbrirForm(crearFormulario());
            return b;
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

        private void MostrarBienvenida()
        {
            Label bienvenida = new Label
            {
                Text = "Bienvenido(a), " + Sesion.UsuarioActual.NombreCompleto +
                       "\n\nSeleccione una opción del menú lateral.",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 16F),
                ForeColor = Estilo.PrimarioOscuro,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlContenedor.Controls.Add(bienvenida);
        }

        private void CerrarSesionUsuario()
        {
            if (!Mensajes.Confirmar("¿Desea cerrar la sesión?")) return;
            Logger.Info("Login", "Cierre de sesión");
            Sesion.Cerrar();
            CerrarSesion = true;
            Close();
        }
    }
}
