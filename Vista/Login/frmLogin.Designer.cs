namespace Vista.Login
{
    partial class frmLogin
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
            this.tlpRaiz = new System.Windows.Forms.TableLayoutPanel();
            this.pnlMarca = new Vista.Comun.PanelDegradado();
            this.tlpCentroMarca = new System.Windows.Forms.TableLayoutPanel();
            this.pnlMarcaContenido = new System.Windows.Forms.Panel();
            this.lblLema = new System.Windows.Forms.Label();
            this.lblMarca = new System.Windows.Forms.Label();
            this.lblLogo = new System.Windows.Forms.Label();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.tlpCentroLogin = new System.Windows.Forms.TableLayoutPanel();
            this.pnlFormulario = new System.Windows.Forms.Panel();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.chkMostrar = new System.Windows.Forms.CheckBox();
            this.btnIngresar = new Vista.Comun.BotonModerno();
            this.btnSalir = new Vista.Comun.BotonModerno();
            this.tlpRaiz.SuspendLayout();
            this.pnlMarca.SuspendLayout();
            this.tlpCentroMarca.SuspendLayout();
            this.pnlMarcaContenido.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.tlpCentroLogin.SuspendLayout();
            this.pnlFormulario.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpRaiz
            // 
            this.tlpRaiz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRaiz.Name = "tlpRaiz";
            this.tlpRaiz.ColumnCount = 2;
            this.tlpRaiz.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpRaiz.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpRaiz.RowCount = 1;
            this.tlpRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRaiz.Controls.Add(this.pnlMarca, 0, 0);
            this.tlpRaiz.Controls.Add(this.pnlLogin, 1, 0);
            // 
            // pnlMarca
            // 
            this.pnlMarca.ColorFin = System.Drawing.Color.FromArgb(12, 36, 40);
            this.pnlMarca.ColorInicio = System.Drawing.Color.FromArgb(13, 148, 136);
            this.pnlMarca.Angulo = 120F;
            this.pnlMarca.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMarca.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.pnlMarca.Name = "pnlMarca";
            this.pnlMarca.Controls.Add(this.tlpCentroMarca);
            // 
            // tlpCentroMarca
            // 
            this.tlpCentroMarca.BackColor = System.Drawing.Color.Transparent;
            this.tlpCentroMarca.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCentroMarca.Name = "tlpCentroMarca";
            this.tlpCentroMarca.ColumnCount = 3;
            this.tlpCentroMarca.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCentroMarca.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpCentroMarca.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCentroMarca.RowCount = 3;
            this.tlpCentroMarca.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCentroMarca.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpCentroMarca.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCentroMarca.Controls.Add(this.pnlMarcaContenido, 1, 1);
            // 
            // pnlMarcaContenido
            // 
            this.pnlMarcaContenido.BackColor = System.Drawing.Color.Transparent;
            this.pnlMarcaContenido.Margin = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.pnlMarcaContenido.Name = "pnlMarcaContenido";
            this.pnlMarcaContenido.Size = new System.Drawing.Size(360, 300);
            this.pnlMarcaContenido.Controls.Add(this.lblLema);
            this.pnlMarcaContenido.Controls.Add(this.lblMarca);
            this.pnlMarcaContenido.Controls.Add(this.lblLogo);
            // 
            // lblLema
            // 
            this.lblLema.BackColor = System.Drawing.Color.Transparent;
            this.lblLema.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLema.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblLema.ForeColor = System.Drawing.Color.FromArgb(204, 251, 241);
            this.lblLema.Height = 90;
            this.lblLema.Name = "lblLema";
            this.lblLema.Text = "Sistema de gestión veterinaria
Expedientes, vacunas y citas en un solo lugar.";
            this.lblLema.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblMarca
            // 
            this.lblMarca.BackColor = System.Drawing.Color.Transparent;
            this.lblMarca.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMarca.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Bold);
            this.lblMarca.ForeColor = System.Drawing.Color.White;
            this.lblMarca.Height = 60;
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Text = "VetCare";
            this.lblMarca.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLogo
            // 
            this.lblLogo.BackColor = System.Drawing.Color.Transparent;
            this.lblLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI Emoji", 56F);
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Height = 120;
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Text = "🐾";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLogin
            // 
            this.pnlLogin.BackColor = System.Drawing.Color.White;
            this.pnlLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Controls.Add(this.tlpCentroLogin);
            // 
            // tlpCentroLogin
            // 
            this.tlpCentroLogin.BackColor = System.Drawing.Color.White;
            this.tlpCentroLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCentroLogin.Name = "tlpCentroLogin";
            this.tlpCentroLogin.ColumnCount = 3;
            this.tlpCentroLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCentroLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpCentroLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCentroLogin.RowCount = 3;
            this.tlpCentroLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCentroLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpCentroLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCentroLogin.Controls.Add(this.pnlFormulario, 1, 1);
            // 
            // pnlFormulario
            // 
            this.pnlFormulario.BackColor = System.Drawing.Color.White;
            this.pnlFormulario.Name = "pnlFormulario";
            this.pnlFormulario.Size = new System.Drawing.Size(360, 430);
            this.pnlFormulario.Margin = new System.Windows.Forms.Padding(24, 24, 24, 24);
            this.pnlFormulario.Controls.Add(this.lblBienvenida);
            this.pnlFormulario.Controls.Add(this.lblSubtitulo);
            this.pnlFormulario.Controls.Add(this.lblUsuario);
            this.pnlFormulario.Controls.Add(this.txtUsuario);
            this.pnlFormulario.Controls.Add(this.lblContrasena);
            this.pnlFormulario.Controls.Add(this.txtContrasena);
            this.pnlFormulario.Controls.Add(this.chkMostrar);
            this.pnlFormulario.Controls.Add(this.btnIngresar);
            this.pnlFormulario.Controls.Add(this.btnSalir);
            // 
            // lblBienvenida
            // 
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblBienvenida.ForeColor = System.Drawing.Color.FromArgb(17, 94, 89);
            this.lblBienvenida.Location = new System.Drawing.Point(0, 0);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Text = "Bienvenido de nuevo";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSubtitulo.Location = new System.Drawing.Point(2, 46);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Text = "Ingrese sus credenciales para continuar";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblUsuario.Location = new System.Drawing.Point(2, 104);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Text = "Usuario";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtUsuario.Location = new System.Drawing.Point(0, 128);
            this.txtUsuario.MaxLength = 50;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(360, 30);
            // 
            // lblContrasena
            // 
            this.lblContrasena.AutoSize = true;
            this.lblContrasena.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblContrasena.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblContrasena.Location = new System.Drawing.Point(2, 176);
            this.lblContrasena.Name = "lblContrasena";
            this.lblContrasena.Text = "Contraseña";
            // 
            // txtContrasena
            // 
            this.txtContrasena.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtContrasena.Location = new System.Drawing.Point(0, 200);
            this.txtContrasena.MaxLength = 100;
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.Size = new System.Drawing.Size(360, 30);
            this.txtContrasena.UseSystemPasswordChar = true;
            // 
            // chkMostrar
            // 
            this.chkMostrar.AutoSize = true;
            this.chkMostrar.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.chkMostrar.Location = new System.Drawing.Point(2, 244);
            this.chkMostrar.Name = "chkMostrar";
            this.chkMostrar.Text = "Mostrar contraseña";
            this.chkMostrar.UseVisualStyleBackColor = true;
            this.chkMostrar.CheckedChanged += new System.EventHandler(this.chkMostrar_CheckedChanged);
            // 
            // btnIngresar
            // 
            this.btnIngresar.BackColor = System.Drawing.Color.FromArgb(15, 118, 110);
            this.btnIngresar.ForeColor = System.Drawing.Color.White;
            this.btnIngresar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(360, 46);
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.UseVisualStyleBackColor = false;
            this.btnIngresar.Location = new System.Drawing.Point(0, 290);
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.btnSalir.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(360, 42);
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Location = new System.Drawing.Point(0, 348);
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnIngresar;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(460, 560);
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VetCare - Iniciar sesión";
            this.Controls.Add(this.tlpRaiz);
            this.Resize += new System.EventHandler(this.frmLogin_Resize);
            this.pnlFormulario.ResumeLayout(false);
            this.tlpCentroLogin.ResumeLayout(false);
            this.tlpCentroLogin.PerformLayout();
            this.pnlLogin.ResumeLayout(false);
            this.pnlMarcaContenido.ResumeLayout(false);
            this.tlpCentroMarca.ResumeLayout(false);
            this.tlpCentroMarca.PerformLayout();
            this.pnlMarca.ResumeLayout(false);
            this.tlpRaiz.ResumeLayout(false);
            this.tlpRaiz.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRaiz;
        private Vista.Comun.PanelDegradado pnlMarca;
        private System.Windows.Forms.TableLayoutPanel tlpCentroMarca;
        private System.Windows.Forms.Panel pnlMarcaContenido;
        private System.Windows.Forms.Label lblLema;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.TableLayoutPanel tlpCentroLogin;
        private System.Windows.Forms.Panel pnlFormulario;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.CheckBox chkMostrar;
        private Vista.Comun.BotonModerno btnIngresar;
        private Vista.Comun.BotonModerno btnSalir;
    }
}
