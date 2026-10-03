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
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.lblApp = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnBitacora = new System.Windows.Forms.Button();
            this.btnRoles = new System.Windows.Forms.Button();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.btnVacunas = new System.Windows.Forms.Button();
            this.btnConsultas = new System.Windows.Forms.Button();
            this.btnCitas = new System.Windows.Forms.Button();
            this.btnMascotas = new System.Windows.Forms.Button();
            this.btnPropietarios = new System.Windows.Forms.Button();
            this.pnlContenedor = new System.Windows.Forms.Panel();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.pnlMenu.SuspendLayout();
            this.pnlContenedor.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(22, 90, 80);
            this.pnlMenu.Controls.Add(this.btnSalir);
            this.pnlMenu.Controls.Add(this.btnBitacora);
            this.pnlMenu.Controls.Add(this.btnRoles);
            this.pnlMenu.Controls.Add(this.btnUsuarios);
            this.pnlMenu.Controls.Add(this.btnVacunas);
            this.pnlMenu.Controls.Add(this.btnConsultas);
            this.pnlMenu.Controls.Add(this.btnCitas);
            this.pnlMenu.Controls.Add(this.btnMascotas);
            this.pnlMenu.Controls.Add(this.btnPropietarios);
            this.pnlMenu.Controls.Add(this.lblUsuario);
            this.pnlMenu.Controls.Add(this.lblApp);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(230, 700);
            this.pnlMenu.TabIndex = 0;
            // 
            // lblApp
            // 
            this.lblApp.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblApp.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblApp.ForeColor = System.Drawing.Color.White;
            this.lblApp.Location = new System.Drawing.Point(0, 0);
            this.lblApp.Name = "lblApp";
            this.lblApp.Size = new System.Drawing.Size(230, 60);
            this.lblApp.TabIndex = 0;
            this.lblApp.Text = "Veterinaria";
            this.lblApp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(200, 230, 225);
            this.lblUsuario.Location = new System.Drawing.Point(0, 60);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(230, 50);
            this.lblUsuario.TabIndex = 1;
            this.lblUsuario.Text = "Usuario";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(0, 110);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnSalir.Size = new System.Drawing.Size(230, 46);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.Text = "Cerrar sesión";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnBitacora
            // 
            this.btnBitacora.BackColor = System.Drawing.Color.FromArgb(22, 90, 80);
            this.btnBitacora.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBitacora.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBitacora.FlatAppearance.BorderSize = 0;
            this.btnBitacora.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBitacora.ForeColor = System.Drawing.Color.White;
            this.btnBitacora.Location = new System.Drawing.Point(0, 156);
            this.btnBitacora.Name = "btnBitacora";
            this.btnBitacora.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnBitacora.Size = new System.Drawing.Size(230, 46);
            this.btnBitacora.TabIndex = 3;
            this.btnBitacora.Text = "Bitácora";
            this.btnBitacora.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBitacora.UseVisualStyleBackColor = false;
            this.btnBitacora.Click += new System.EventHandler(this.btnBitacora_Click);
            // 
            // btnRoles
            // 
            this.btnRoles.BackColor = System.Drawing.Color.FromArgb(22, 90, 80);
            this.btnRoles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRoles.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRoles.FlatAppearance.BorderSize = 0;
            this.btnRoles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoles.ForeColor = System.Drawing.Color.White;
            this.btnRoles.Location = new System.Drawing.Point(0, 202);
            this.btnRoles.Name = "btnRoles";
            this.btnRoles.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnRoles.Size = new System.Drawing.Size(230, 46);
            this.btnRoles.TabIndex = 4;
            this.btnRoles.Text = "Roles y permisos";
            this.btnRoles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRoles.UseVisualStyleBackColor = false;
            this.btnRoles.Click += new System.EventHandler(this.btnRoles_Click);
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.BackColor = System.Drawing.Color.FromArgb(22, 90, 80);
            this.btnUsuarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUsuarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUsuarios.FlatAppearance.BorderSize = 0;
            this.btnUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsuarios.ForeColor = System.Drawing.Color.White;
            this.btnUsuarios.Location = new System.Drawing.Point(0, 248);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnUsuarios.Size = new System.Drawing.Size(230, 46);
            this.btnUsuarios.TabIndex = 5;
            this.btnUsuarios.Text = "Usuarios";
            this.btnUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuarios.UseVisualStyleBackColor = false;
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            // 
            // btnVacunas
            // 
            this.btnVacunas.BackColor = System.Drawing.Color.FromArgb(22, 90, 80);
            this.btnVacunas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVacunas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVacunas.FlatAppearance.BorderSize = 0;
            this.btnVacunas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVacunas.ForeColor = System.Drawing.Color.White;
            this.btnVacunas.Location = new System.Drawing.Point(0, 294);
            this.btnVacunas.Name = "btnVacunas";
            this.btnVacunas.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnVacunas.Size = new System.Drawing.Size(230, 46);
            this.btnVacunas.TabIndex = 6;
            this.btnVacunas.Text = "Vacunas";
            this.btnVacunas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVacunas.UseVisualStyleBackColor = false;
            this.btnVacunas.Click += new System.EventHandler(this.btnVacunas_Click);
            // 
            // btnConsultas
            // 
            this.btnConsultas.BackColor = System.Drawing.Color.FromArgb(22, 90, 80);
            this.btnConsultas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnConsultas.FlatAppearance.BorderSize = 0;
            this.btnConsultas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultas.ForeColor = System.Drawing.Color.White;
            this.btnConsultas.Location = new System.Drawing.Point(0, 340);
            this.btnConsultas.Name = "btnConsultas";
            this.btnConsultas.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnConsultas.Size = new System.Drawing.Size(230, 46);
            this.btnConsultas.TabIndex = 7;
            this.btnConsultas.Text = "Consultas médicas";
            this.btnConsultas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultas.UseVisualStyleBackColor = false;
            this.btnConsultas.Click += new System.EventHandler(this.btnConsultas_Click);
            // 
            // btnCitas
            // 
            this.btnCitas.BackColor = System.Drawing.Color.FromArgb(22, 90, 80);
            this.btnCitas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCitas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCitas.FlatAppearance.BorderSize = 0;
            this.btnCitas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCitas.ForeColor = System.Drawing.Color.White;
            this.btnCitas.Location = new System.Drawing.Point(0, 386);
            this.btnCitas.Name = "btnCitas";
            this.btnCitas.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnCitas.Size = new System.Drawing.Size(230, 46);
            this.btnCitas.TabIndex = 8;
            this.btnCitas.Text = "Citas";
            this.btnCitas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCitas.UseVisualStyleBackColor = false;
            this.btnCitas.Click += new System.EventHandler(this.btnCitas_Click);
            // 
            // btnMascotas
            // 
            this.btnMascotas.BackColor = System.Drawing.Color.FromArgb(22, 90, 80);
            this.btnMascotas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMascotas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMascotas.FlatAppearance.BorderSize = 0;
            this.btnMascotas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMascotas.ForeColor = System.Drawing.Color.White;
            this.btnMascotas.Location = new System.Drawing.Point(0, 432);
            this.btnMascotas.Name = "btnMascotas";
            this.btnMascotas.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnMascotas.Size = new System.Drawing.Size(230, 46);
            this.btnMascotas.TabIndex = 9;
            this.btnMascotas.Text = "Mascotas";
            this.btnMascotas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMascotas.UseVisualStyleBackColor = false;
            this.btnMascotas.Click += new System.EventHandler(this.btnMascotas_Click);
            // 
            // btnPropietarios
            // 
            this.btnPropietarios.BackColor = System.Drawing.Color.FromArgb(22, 90, 80);
            this.btnPropietarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPropietarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPropietarios.FlatAppearance.BorderSize = 0;
            this.btnPropietarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPropietarios.ForeColor = System.Drawing.Color.White;
            this.btnPropietarios.Location = new System.Drawing.Point(0, 478);
            this.btnPropietarios.Name = "btnPropietarios";
            this.btnPropietarios.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnPropietarios.Size = new System.Drawing.Size(230, 46);
            this.btnPropietarios.TabIndex = 10;
            this.btnPropietarios.Text = "Propietarios";
            this.btnPropietarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPropietarios.UseVisualStyleBackColor = false;
            this.btnPropietarios.Click += new System.EventHandler(this.btnPropietarios_Click);
            // 
            // pnlContenedor
            // 
            this.pnlContenedor.BackColor = System.Drawing.Color.FromArgb(244, 247, 246);
            this.pnlContenedor.Controls.Add(this.lblBienvenida);
            this.pnlContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenedor.Location = new System.Drawing.Point(230, 0);
            this.pnlContenedor.Name = "pnlContenedor";
            this.pnlContenedor.Size = new System.Drawing.Size(770, 700);
            this.pnlContenedor.TabIndex = 1;
            // 
            // lblBienvenida
            // 
            this.lblBienvenida.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblBienvenida.ForeColor = System.Drawing.Color.FromArgb(22, 90, 80);
            this.lblBienvenida.Location = new System.Drawing.Point(0, 0);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(770, 700);
            this.lblBienvenida.TabIndex = 0;
            this.lblBienvenida.Text = "Bienvenido(a)";
            this.lblBienvenida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmDashboardPrincipal
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.pnlContenedor);
            this.Controls.Add(this.pnlMenu);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(1016, 739);
            this.Name = "frmDashboardPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Gestión Veterinaria";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDashboardPrincipal_Load);
            this.pnlMenu.ResumeLayout(false);
            this.pnlContenedor.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Label lblApp;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnBitacora;
        private System.Windows.Forms.Button btnRoles;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Button btnVacunas;
        private System.Windows.Forms.Button btnConsultas;
        private System.Windows.Forms.Button btnCitas;
        private System.Windows.Forms.Button btnMascotas;
        private System.Windows.Forms.Button btnPropietarios;
        private System.Windows.Forms.Panel pnlContenedor;
        private System.Windows.Forms.Label lblBienvenida;
    }
}
