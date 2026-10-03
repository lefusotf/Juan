namespace Vista.Citas
{
    partial class frmCitas
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
            System.Windows.Forms.DataGridViewCellStyle estiloEnc0 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle estiloAlt0 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle estiloCelda0 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpRaiz = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.flpBusqueda = new System.Windows.Forms.FlowLayoutPanel();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new Vista.Comun.BotonModerno();
            this.pnlDatos = new Vista.Comun.PanelTarjeta();
            this.tlpCampos = new System.Windows.Forms.TableLayoutPanel();
            this.lblMascota = new System.Windows.Forms.Label();
            this.cboMascota = new System.Windows.Forms.ComboBox();
            this.lblVeterinario = new System.Windows.Forms.Label();
            this.cboVeterinario = new System.Windows.Forms.ComboBox();
            this.lblFechaHora = new System.Windows.Forms.Label();
            this.dtpFechaHora = new System.Windows.Forms.DateTimePicker();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cboEstado = new System.Windows.Forms.ComboBox();
            this.lblMotivo = new System.Windows.Forms.Label();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.lblSeccion = new System.Windows.Forms.Label();
            this.flpBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNuevo = new Vista.Comun.BotonModerno();
            this.btnGuardar = new Vista.Comun.BotonModerno();
            this.btnEliminar = new Vista.Comun.BotonModerno();
            this.pnlTabla = new Vista.Comun.PanelTarjeta();
            this.dgv = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.tlpRaiz.SuspendLayout();
            this.flpBusqueda.SuspendLayout();
            this.pnlDatos.SuspendLayout();
            this.tlpCampos.SuspendLayout();
            this.flpBotones.SuspendLayout();
            this.pnlTabla.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpRaiz
            // 
            this.tlpRaiz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRaiz.Name = "tlpRaiz";
            this.tlpRaiz.Padding = new System.Windows.Forms.Padding(28, 24, 28, 20);
            this.tlpRaiz.ColumnCount = 1;
            this.tlpRaiz.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRaiz.RowCount = 5;
            this.tlpRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRaiz.Controls.Add(this.lblTitulo, 0, 0);
            this.tlpRaiz.Controls.Add(this.flpBusqueda, 0, 1);
            this.tlpRaiz.Controls.Add(this.pnlDatos, 0, 2);
            this.tlpRaiz.Controls.Add(this.flpBotones, 0, 3);
            this.tlpRaiz.Controls.Add(this.pnlTabla, 0, 4);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(17, 94, 89);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "Citas médicas";
            // 
            // flpBusqueda
            // 
            this.flpBusqueda.AutoSize = true;
            this.flpBusqueda.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpBusqueda.Margin = new System.Windows.Forms.Padding(0, 10, 0, 14);
            this.flpBusqueda.Name = "flpBusqueda";
            this.flpBusqueda.Controls.Add(this.txtBuscar);
            this.flpBusqueda.Controls.Add(this.btnBuscar);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(0, 4, 10, 0);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(320, 27);
            this.txtBuscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscar_KeyDown);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(110, 34);
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // pnlDatos
            // 
            this.pnlDatos.AutoSize = true;
            this.pnlDatos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDatos.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.pnlDatos.Name = "pnlDatos";
            this.pnlDatos.Padding = new System.Windows.Forms.Padding(22, 18, 22, 14);
            this.pnlDatos.Controls.Add(this.tlpCampos);
            this.pnlDatos.Controls.Add(this.lblSeccion);
            // 
            // tlpCampos
            // 
            this.tlpCampos.AutoSize = true;
            this.tlpCampos.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpCampos.Name = "tlpCampos";
            this.tlpCampos.ColumnCount = 4;
            this.tlpCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCampos.RowCount = 3;
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpCampos.Controls.Add(this.lblMascota, 0, 0);
            this.tlpCampos.Controls.Add(this.cboMascota, 1, 0);
            this.tlpCampos.Controls.Add(this.lblVeterinario, 2, 0);
            this.tlpCampos.Controls.Add(this.cboVeterinario, 3, 0);
            this.tlpCampos.Controls.Add(this.lblFechaHora, 0, 1);
            this.tlpCampos.Controls.Add(this.dtpFechaHora, 1, 1);
            this.tlpCampos.Controls.Add(this.lblEstado, 2, 1);
            this.tlpCampos.Controls.Add(this.cboEstado, 3, 1);
            this.tlpCampos.Controls.Add(this.lblMotivo, 0, 2);
            this.tlpCampos.Controls.Add(this.txtMotivo, 1, 2);
            this.tlpCampos.SetColumnSpan(this.txtMotivo, 3);
            // 
            // lblMascota
            // 
            this.lblMascota.AutoSize = true;
            this.lblMascota.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMascota.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMascota.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblMascota.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.lblMascota.Name = "lblMascota";
            this.lblMascota.Text = "Mascota *";
            // 
            // cboMascota
            // 
            this.cboMascota.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboMascota.Margin = new System.Windows.Forms.Padding(3, 6, 16, 6);
            this.cboMascota.Name = "cboMascota";
            this.cboMascota.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMascota.FormattingEnabled = true;
            // 
            // lblVeterinario
            // 
            this.lblVeterinario.AutoSize = true;
            this.lblVeterinario.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblVeterinario.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblVeterinario.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblVeterinario.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.lblVeterinario.Name = "lblVeterinario";
            this.lblVeterinario.Text = "Veterinario *";
            // 
            // cboVeterinario
            // 
            this.cboVeterinario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboVeterinario.Margin = new System.Windows.Forms.Padding(3, 6, 16, 6);
            this.cboVeterinario.Name = "cboVeterinario";
            this.cboVeterinario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVeterinario.FormattingEnabled = true;
            // 
            // lblFechaHora
            // 
            this.lblFechaHora.AutoSize = true;
            this.lblFechaHora.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFechaHora.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblFechaHora.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblFechaHora.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.lblFechaHora.Name = "lblFechaHora";
            this.lblFechaHora.Text = "Fecha y hora *";
            // 
            // dtpFechaHora
            // 
            this.dtpFechaHora.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFechaHora.Margin = new System.Windows.Forms.Padding(3, 6, 16, 6);
            this.dtpFechaHora.Name = "dtpFechaHora";
            this.dtpFechaHora.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaHora.CustomFormat = "dd/MM/yyyy HH:mm";
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Text = "Estado";
            // 
            // cboEstado
            // 
            this.cboEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboEstado.Margin = new System.Windows.Forms.Padding(3, 6, 16, 6);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.Items.AddRange(new object[] {"Programada", "Atendida", "Cancelada"});
            // 
            // lblMotivo
            // 
            this.lblMotivo.AutoSize = true;
            this.lblMotivo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMotivo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMotivo.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblMotivo.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.Text = "Motivo *";
            // 
            // txtMotivo
            // 
            this.txtMotivo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMotivo.Margin = new System.Windows.Forms.Padding(3, 6, 16, 6);
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.MaxLength = 250;
            // 
            // lblSeccion
            // 
            this.lblSeccion.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSeccion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSeccion.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblSeccion.Height = 34;
            this.lblSeccion.Name = "lblSeccion";
            this.lblSeccion.Text = "Datos del registro";
            this.lblSeccion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flpBotones
            // 
            this.flpBotones.AutoSize = true;
            this.flpBotones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpBotones.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.flpBotones.Name = "flpBotones";
            this.flpBotones.Controls.Add(this.btnNuevo);
            this.flpBotones.Controls.Add(this.btnGuardar);
            this.flpBotones.Controls.Add(this.btnEliminar);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(130, 42);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(15, 118, 110);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(130, 42);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(130, 42);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // pnlTabla
            // 
            this.pnlTabla.Name = "pnlTabla";
            this.pnlTabla.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.pnlTabla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTabla.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.pnlTabla.Controls.Add(this.dgv);
            // 
            // dgv
            // 
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Name = "dgv";
            estiloEnc0.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            estiloEnc0.BackColor = System.Drawing.Color.FromArgb(241, 245, 244);
            estiloEnc0.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            estiloEnc0.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            estiloEnc0.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            estiloEnc0.SelectionBackColor = System.Drawing.Color.FromArgb(241, 245, 244);
            estiloEnc0.SelectionForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            estiloCelda0.BackColor = System.Drawing.Color.White;
            estiloCelda0.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            estiloCelda0.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            estiloCelda0.SelectionBackColor = System.Drawing.Color.FromArgb(204, 251, 241);
            estiloCelda0.SelectionForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            estiloAlt0.BackColor = System.Drawing.Color.FromArgb(249, 251, 251);
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AlternatingRowsDefaultCellStyle = estiloAlt0;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv.ColumnHeadersDefaultCellStyle = estiloEnc0;
            this.dgv.ColumnHeadersHeight = 42;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv.DefaultCellStyle = estiloCelda0;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.dgv.MultiSelect = false;
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 38;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.SelectionChanged += new System.EventHandler(this.dgv_SelectionChanged);
            // 
            // frmCitas
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 244);
            this.ClientSize = new System.Drawing.Size(1000, 680);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "frmCitas";
            this.Text = "Citas médicas";
            this.Controls.Add(this.tlpRaiz);
            this.Load += new System.EventHandler(this.frmCitas_Load);
            this.Resize += new System.EventHandler(this.frmCitas_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.pnlTabla.ResumeLayout(false);
            this.flpBotones.ResumeLayout(false);
            this.flpBotones.PerformLayout();
            this.tlpCampos.ResumeLayout(false);
            this.tlpCampos.PerformLayout();
            this.pnlDatos.ResumeLayout(false);
            this.pnlDatos.PerformLayout();
            this.flpBusqueda.ResumeLayout(false);
            this.flpBusqueda.PerformLayout();
            this.tlpRaiz.ResumeLayout(false);
            this.tlpRaiz.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRaiz;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.FlowLayoutPanel flpBusqueda;
        private System.Windows.Forms.TextBox txtBuscar;
        private Vista.Comun.BotonModerno btnBuscar;
        private Vista.Comun.PanelTarjeta pnlDatos;
        private System.Windows.Forms.TableLayoutPanel tlpCampos;
        private System.Windows.Forms.Label lblMascota;
        private System.Windows.Forms.ComboBox cboMascota;
        private System.Windows.Forms.Label lblVeterinario;
        private System.Windows.Forms.ComboBox cboVeterinario;
        private System.Windows.Forms.Label lblFechaHora;
        private System.Windows.Forms.DateTimePicker dtpFechaHora;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.ComboBox cboEstado;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Label lblSeccion;
        private System.Windows.Forms.FlowLayoutPanel flpBotones;
        private Vista.Comun.BotonModerno btnNuevo;
        private Vista.Comun.BotonModerno btnGuardar;
        private Vista.Comun.BotonModerno btnEliminar;
        private Vista.Comun.PanelTarjeta pnlTabla;
        private System.Windows.Forms.DataGridView dgv;
    }
}
