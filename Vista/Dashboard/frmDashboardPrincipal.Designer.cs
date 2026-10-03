namespace Vista.Dashboard
{
    partial class frmDashboardPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.pnlContenedor = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblSeccion = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.btnToggle = new Vista.Comun.BotonMenu();
            this.pnlLinea = new System.Windows.Forms.Panel();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.btnSalir = new Vista.Comun.BotonMenu();
            this.btnBitacora = new Vista.Comun.BotonMenu();
            this.btnRoles = new Vista.Comun.BotonMenu();
            this.btnUsuarios = new Vista.Comun.BotonMenu();
            this.btnVacunas = new Vista.Comun.BotonMenu();
            this.btnConsultas = new Vista.Comun.BotonMenu();
            this.btnCitas = new Vista.Comun.BotonMenu();
            this.btnMascotas = new Vista.Comun.BotonMenu();
            this.btnPropietarios = new Vista.Comun.BotonMenu();
            this.btnInicio = new Vista.Comun.BotonMenu();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblApp = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContenedor
            // 
            this.pnlContenedor.BackColor = System.Drawing.Color.FromArgb(241, 245, 244);
            this.pnlContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenedor.Name = "pnlContenedor";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1030, 64);
            this.pnlTop.Controls.Add(this.lblSeccion);
            this.pnlTop.Controls.Add(this.lblFecha);
            this.pnlTop.Controls.Add(this.btnToggle);
            this.pnlTop.Controls.Add(this.pnlLinea);
            // 
            // lblSeccion
            // 
            this.lblSeccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSeccion.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblSeccion.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblSeccion.Name = "lblSeccion";
            this.lblSeccion.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblSeccion.Text = "Inicio";
            this.lblSeccion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFecha
            // 
            this.lblFecha.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblFecha.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Padding = new System.Windows.Forms.Padding(0, 0, 24, 0);
            this.lblFecha.Size = new System.Drawing.Size(340, 64);
            this.lblFecha.Text = "Fecha";
            this.lblFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnToggle
            // 
            this.btnToggle.Compacto = true;
            this.btnToggle.ColorFondo = System.Drawing.Color.White;
            this.btnToggle.ColorHover = System.Drawing.Color.FromArgb(241, 245, 244);
            this.btnToggle.ColorTexto = System.Drawing.Color.FromArgb(31, 41, 55);
            this.btnToggle.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnToggle.Icono = "\u2630";
            this.btnToggle.Name = "btnToggle";
            this.btnToggle.Size = new System.Drawing.Size(64, 64);
            this.btnToggle.Text = "Menú";
            this.btnToggle.Click += new System.EventHandler(this.btnToggle_Click);
            // 
            // pnlLinea
            // 
            this.pnlLinea.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlLinea.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLinea.Height = 1;
            this.pnlLinea.Name = "pnlLinea";
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(15, 42, 46);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(250, 760);
            this.pnlMenu.Controls.Add(this.btnSalir);
            this.pnlMenu.Controls.Add(this.btnBitacora);
            this.pnlMenu.Controls.Add(this.btnRoles);
            this.pnlMenu.Controls.Add(this.btnUsuarios);
            this.pnlMenu.Controls.Add(this.btnVacunas);
            this.pnlMenu.Controls.Add(this.btnConsultas);
            this.pnlMenu.Controls.Add(this.btnCitas);
            this.pnlMenu.Controls.Add(this.btnMascotas);
            this.pnlMenu.Controls.Add(this.btnPropietarios);
            this.pnlMenu.Controls.Add(this.btnInicio);
            this.pnlMenu.Controls.Add(this.lblUsuario);
            this.pnlMenu.Controls.Add(this.lblApp);
            // 
            // btnSalir
            // 
            this.btnSalir.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSalir.Icono = "\U0001F6AA";
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(250, 50);
            this.btnSalir.Text = "Cerrar sesión";
            this.btnSalir.ColorTexto = System.Drawing.Color.FromArgb(252, 165, 165);
            this.btnSalir.ColorHover = System.Drawing.Color.FromArgb(127, 29, 29);
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnBitacora
            // 
            this.btnBitacora.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBitacora.Icono = "\U0001F4CB";
            this.btnBitacora.Name = "btnBitacora";
            this.btnBitacora.Size = new System.Drawing.Size(250, 50);
            this.btnBitacora.Text = "Bitácora";
            this.btnBitacora.Click += new System.EventHandler(this.btnBitacora_Click);
            // 
            // btnRoles
            // 
            this.btnRoles.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRoles.Icono = "\U0001F510";
            this.btnRoles.Name = "btnRoles";
            this.btnRoles.Size = new System.Drawing.Size(250, 50);
            this.btnRoles.Text = "Roles y permisos";
            this.btnRoles.Click += new System.EventHandler(this.btnRoles_Click);
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUsuarios.Icono = "\U0001F465";
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Size = new System.Drawing.Size(250, 50);
            this.btnUsuarios.Text = "Usuarios";
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            // 
            // btnVacunas
            // 
            this.btnVacunas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVacunas.Icono = "\U0001F489";
            this.btnVacunas.Name = "btnVacunas";
            this.btnVacunas.Size = new System.Drawing.Size(250, 50);
            this.btnVacunas.Text = "Vacunas";
            this.btnVacunas.Click += new System.EventHandler(this.btnVacunas_Click);
            // 
            // btnConsultas
            // 
            this.btnConsultas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnConsultas.Icono = "\U0001FA7A";
            this.btnConsultas.Name = "btnConsultas";
            this.btnConsultas.Size = new System.Drawing.Size(250, 50);
            this.btnConsultas.Text = "Consultas médicas";
            this.btnConsultas.Click += new System.EventHandler(this.btnConsultas_Click);
            // 
            // btnCitas
            // 
            this.btnCitas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCitas.Icono = "\U0001F4C5";
            this.btnCitas.Name = "btnCitas";
            this.btnCitas.Size = new System.Drawing.Size(250, 50);
            this.btnCitas.Text = "Citas";
            this.btnCitas.Click += new System.EventHandler(this.btnCitas_Click);
            // 
            // btnMascotas
            // 
            this.btnMascotas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMascotas.Icono = "\U0001F43E";
            this.btnMascotas.Name = "btnMascotas";
            this.btnMascotas.Size = new System.Drawing.Size(250, 50);
            this.btnMascotas.Text = "Mascotas";
            this.btnMascotas.Click += new System.EventHandler(this.btnMascotas_Click);
            // 
            // btnPropietarios
            // 
            this.btnPropietarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPropietarios.Icono = "\U0001F464";
            this.btnPropietarios.Name = "btnPropietarios";
            this.btnPropietarios.Size = new System.Drawing.Size(250, 50);
            this.btnPropietarios.Text = "Propietarios";
            this.btnPropietarios.Click += new System.EventHandler(this.btnPropietarios_Click);
            // 
            // btnInicio
            // 
            this.btnInicio.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInicio.Icono = "\U0001F3E0";
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(250, 50);
            this.btnInicio.Text = "Inicio";
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            // 
            // lblUsuario
            // 
            this.lblUsuario.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(153, 246, 228);
            this.lblUsuario.Height = 64;
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Text = "Usuario";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblApp
            // 
            this.lblApp.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblApp.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblApp.ForeColor = System.Drawing.Color.White;
            this.lblApp.Height = 84;
            this.lblApp.Name = "lblApp";
            this.lblApp.Text = "\U0001F43E  VetCare";
            this.lblApp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmDashboardPrincipal
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1280, 760);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(780, 560);
            this.Name = "frmDashboardPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VetCare - Sistema de Gestión Veterinaria";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Controls.Add(this.pnlContenedor);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlMenu);
            this.Load += new System.EventHandler(this.frmDashboardPrincipal_Load);
            this.Resize += new System.EventHandler(this.frmDashboardPrincipal_Resize);
            this.pnlMenu.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlContenedor;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblSeccion;
        private System.Windows.Forms.Label lblFecha;
        private Vista.Comun.BotonMenu btnToggle;
        private System.Windows.Forms.Panel pnlLinea;
        private System.Windows.Forms.Panel pnlMenu;
        private Vista.Comun.BotonMenu btnSalir;
        private Vista.Comun.BotonMenu btnBitacora;
        private Vista.Comun.BotonMenu btnRoles;
        private Vista.Comun.BotonMenu btnUsuarios;
        private Vista.Comun.BotonMenu btnVacunas;
        private Vista.Comun.BotonMenu btnConsultas;
        private Vista.Comun.BotonMenu btnCitas;
        private Vista.Comun.BotonMenu btnMascotas;
        private Vista.Comun.BotonMenu btnPropietarios;
        private Vista.Comun.BotonMenu btnInicio;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblApp;
    }
}
