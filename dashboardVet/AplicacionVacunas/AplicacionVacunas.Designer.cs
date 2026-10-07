namespace dashboardVet.AplicacionVacunas
{
    partial class AplicacionVacunas
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
            this.btnBuscar = new dashboardVet.Controles.BotonModerno();
            this.pnlDatos = new dashboardVet.Controles.PanelTarjeta();
            this.tlpCampos = new System.Windows.Forms.TableLayoutPanel();
            this.lblMascota = new System.Windows.Forms.Label();
            this.cboMascota = new System.Windows.Forms.ComboBox();
            this.lblVacuna = new System.Windows.Forms.Label();
            this.cboVacuna = new System.Windows.Forms.ComboBox();
            this.lblVeterinario = new System.Windows.Forms.Label();
            this.cboVeterinario = new System.Windows.Forms.ComboBox();
            this.lblAplicacion = new System.Windows.Forms.Label();
            this.dtpAplicacion = new System.Windows.Forms.DateTimePicker();
            this.lblProxima = new System.Windows.Forms.Label();
            this.dtpProxima = new System.Windows.Forms.DateTimePicker();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.lblSeccion = new System.Windows.Forms.Label();
            this.flpBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNuevo = new dashboardVet.Controles.BotonModerno();
            this.btnGuardar = new dashboardVet.Controles.BotonModerno();
            this.btnEliminar = new dashboardVet.Controles.BotonModerno();
            this.pnlTabla = new dashboardVet.Controles.PanelTarjeta();
            this.dgv = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.tlpRaiz.SuspendLayout();
            this.flpBusqueda.SuspendLayout();
            this.pnlDatos.SuspendLayout();
            this.tlpCampos.SuspendLayout();
            this.flpBotones.SuspendLayout();
            this.pnlTabla.SuspendLayout();
            this.SuspendLayout();

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

            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(17, 94, 89);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "Aplicación de vacunas";

            this.flpBusqueda.AutoSize = true;
            this.flpBusqueda.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpBusqueda.Margin = new System.Windows.Forms.Padding(0, 10, 0, 14);
            this.flpBusqueda.Name = "flpBusqueda";
            this.flpBusqueda.Controls.Add(this.txtBuscar);
            this.flpBusqueda.Controls.Add(this.btnBuscar);

            this.txtBuscar.Margin = new System.Windows.Forms.Padding(0, 4, 10, 0);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(320, 27);
            this.txtBuscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscar_KeyDown);

            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(110, 34);
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);

            this.pnlDatos.AutoSize = true;
            this.pnlDatos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDatos.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.pnlDatos.Name = "pnlDatos";
            this.pnlDatos.Padding = new System.Windows.Forms.Padding(22, 18, 22, 14);
            this.pnlDatos.Controls.Add(this.tlpCampos);
            this.pnlDatos.Controls.Add(this.lblSeccion);

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
            this.tlpCampos.Controls.Add(this.lblVacuna, 2, 0);
            this.tlpCampos.Controls.Add(this.cboVacuna, 3, 0);
            this.tlpCampos.Controls.Add(this.lblVeterinario, 0, 1);
            this.tlpCampos.Controls.Add(this.cboVeterinario, 1, 1);
            this.tlpCampos.Controls.Add(this.lblAplicacion, 2, 1);
            this.tlpCampos.Controls.Add(this.dtpAplicacion, 3, 1);
            this.tlpCampos.Controls.Add(this.lblProxima, 0, 2);
            this.tlpCampos.Controls.Add(this.dtpProxima, 1, 2);
            this.tlpCampos.Controls.Add(this.lblObservaciones, 2, 2);
            this.tlpCampos.Controls.Add(this.txtObservaciones, 3, 2);

            this.lblMascota.AutoSize = true;
            this.lblMascota.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMascota.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMascota.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblMascota.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.lblMascota.Name = "lblMascota";
            this.lblMascota.Text = "Mascota *";

            this.cboMascota.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboMascota.Margin = new System.Windows.Forms.Padding(3, 6, 16, 6);
            this.cboMascota.Name = "cboMascota";
            this.cboMascota.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMascota.FormattingEnabled = true;

            this.lblVacuna.AutoSize = true;
            this.lblVacuna.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblVacuna.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblVacuna.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblVacuna.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.lblVacuna.Name = "lblVacuna";
            this.lblVacuna.Text = "Vacuna *";

            this.cboVacuna.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboVacuna.Margin = new System.Windows.Forms.Padding(3, 6, 16, 6);
            this.cboVacuna.Name = "cboVacuna";
            this.cboVacuna.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVacuna.FormattingEnabled = true;
            this.cboVacuna.SelectionChangeCommitted += new System.EventHandler(this.cboVacuna_SelectionChangeCommitted);

            this.lblVeterinario.AutoSize = true;
            this.lblVeterinario.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblVeterinario.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblVeterinario.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblVeterinario.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.lblVeterinario.Name = "lblVeterinario";
            this.lblVeterinario.Text = "Veterinario *";

            this.cboVeterinario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboVeterinario.Margin = new System.Windows.Forms.Padding(3, 6, 16, 6);
            this.cboVeterinario.Name = "cboVeterinario";
            this.cboVeterinario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVeterinario.FormattingEnabled = true;

            this.lblAplicacion.AutoSize = true;
            this.lblAplicacion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAplicacion.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblAplicacion.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblAplicacion.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.lblAplicacion.Name = "lblAplicacion";
            this.lblAplicacion.Text = "Fecha aplicación *";

            this.dtpAplicacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpAplicacion.Margin = new System.Windows.Forms.Padding(3, 6, 16, 6);
            this.dtpAplicacion.Name = "dtpAplicacion";
            this.dtpAplicacion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAplicacion.ValueChanged += new System.EventHandler(this.dtpAplicacion_ValueChanged);

            this.lblProxima.AutoSize = true;
            this.lblProxima.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblProxima.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblProxima.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblProxima.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.lblProxima.Name = "lblProxima";
            this.lblProxima.Text = "Próxima dosis";

            this.dtpProxima.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpProxima.Margin = new System.Windows.Forms.Padding(3, 6, 16, 6);
            this.dtpProxima.Name = "dtpProxima";
            this.dtpProxima.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpProxima.ShowCheckBox = true;

            this.lblObservaciones.AutoSize = true;
            this.lblObservaciones.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblObservaciones.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblObservaciones.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblObservaciones.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Text = "Observaciones";

            this.txtObservaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtObservaciones.Margin = new System.Windows.Forms.Padding(3, 6, 16, 6);
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.MaxLength = 250;

            this.lblSeccion.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSeccion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSeccion.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblSeccion.Height = 34;
            this.lblSeccion.Name = "lblSeccion";
            this.lblSeccion.Text = "Datos del registro";
            this.lblSeccion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.flpBotones.AutoSize = true;
            this.flpBotones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpBotones.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.flpBotones.Name = "flpBotones";
            this.flpBotones.Controls.Add(this.btnNuevo);
            this.flpBotones.Controls.Add(this.btnGuardar);
            this.flpBotones.Controls.Add(this.btnEliminar);

            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(130, 42);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);

            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(15, 118, 110);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(130, 42);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(130, 42);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);

            this.pnlTabla.Name = "pnlTabla";
            this.pnlTabla.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.pnlTabla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTabla.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.pnlTabla.Controls.Add(this.dgv);

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

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 244);
            this.ClientSize = new System.Drawing.Size(1000, 680);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "AplicacionVacunas";
            this.Text = "Aplicación de vacunas";
            this.Controls.Add(this.tlpRaiz);
            this.Load += new System.EventHandler(this.AplicacionVacunas_Load);
            this.Resize += new System.EventHandler(this.AplicacionVacunas_Resize);
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
        private dashboardVet.Controles.BotonModerno btnBuscar;
        private dashboardVet.Controles.PanelTarjeta pnlDatos;
        private System.Windows.Forms.TableLayoutPanel tlpCampos;
        private System.Windows.Forms.Label lblMascota;
        private System.Windows.Forms.ComboBox cboMascota;
        private System.Windows.Forms.Label lblVacuna;
        private System.Windows.Forms.ComboBox cboVacuna;
        private System.Windows.Forms.Label lblVeterinario;
        private System.Windows.Forms.ComboBox cboVeterinario;
        private System.Windows.Forms.Label lblAplicacion;
        private System.Windows.Forms.DateTimePicker dtpAplicacion;
        private System.Windows.Forms.Label lblProxima;
        private System.Windows.Forms.DateTimePicker dtpProxima;
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label lblSeccion;
        private System.Windows.Forms.FlowLayoutPanel flpBotones;
        private dashboardVet.Controles.BotonModerno btnNuevo;
        private dashboardVet.Controles.BotonModerno btnGuardar;
        private dashboardVet.Controles.BotonModerno btnEliminar;
        private dashboardVet.Controles.PanelTarjeta pnlTabla;
        private System.Windows.Forms.DataGridView dgv;
    }
}
