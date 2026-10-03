namespace Vista.Mascotas
{
    partial class frmMascotas
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
            this.lblPropietario = new System.Windows.Forms.Label();
            this.cboPropietario = new System.Windows.Forms.ComboBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblEspecie = new System.Windows.Forms.Label();
            this.cboEspecie = new System.Windows.Forms.ComboBox();
            this.lblRaza = new System.Windows.Forms.Label();
            this.txtRaza = new System.Windows.Forms.TextBox();
            this.lblSexo = new System.Windows.Forms.Label();
            this.cboSexo = new System.Windows.Forms.ComboBox();
            this.lblNacimiento = new System.Windows.Forms.Label();
            this.dtpNacimiento = new System.Windows.Forms.DateTimePicker();
            this.lblPeso = new System.Windows.Forms.Label();
            this.nudPeso = new System.Windows.Forms.NumericUpDown();
            this.lblColor = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.lblSeccion = new System.Windows.Forms.Label();
            this.flpBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNuevo = new Vista.Comun.BotonModerno();
            this.btnGuardar = new Vista.Comun.BotonModerno();
            this.btnEliminar = new Vista.Comun.BotonModerno();
            this.pnlTabla = new Vista.Comun.PanelTarjeta();
            this.dgv = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.nudPeso)).BeginInit();
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
            this.lblTitulo.Text = "Mascotas";
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
            this.tlpCampos.RowCount = 4;
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpCampos.Controls.Add(this.lblPropietario, 0, 0);
            this.tlpCampos.Controls.Add(this.cboPropietario, 1, 0);
            this.tlpCampos.Controls.Add(this.lblNombre, 2, 0);
            this.tlpCampos.Controls.Add(this.txtNombre, 3, 0);
            this.tlpCampos.Controls.Add(this.lblEspecie, 0, 1);
            this.tlpCampos.Controls.Add(this.cboEspecie, 1, 1);
            this.tlpCampos.Controls.Add(this.lblRaza, 2, 1);
            this.tlpCampos.Controls.Add(this.txtRaza, 3, 1);
            this.tlpCampos.Controls.Add(this.lblSexo, 0, 2);
            this.tlpCampos.Controls.Add(this.cboSexo, 1, 2);
            this.tlpCampos.Controls.Add(this.lblNacimiento, 2, 2);
            this.tlpCampos.Controls.Add(this.dtpNacimiento, 3, 2);
            this.tlpCampos.Controls.Add(this.lblPeso, 0, 3);
            this.tlpCampos.Controls.Add(this.nudPeso, 1, 3);
            this.tlpCampos.Controls.Add(this.lblColor, 2, 3);
            this.tlpCampos.Controls.Add(this.txtColor, 3, 3);
            // 
            // lblPropietario
            // 
            this.lblPropietario.AutoSize = true;
            this.lblPropietario.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPropietario.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPropietario.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblPropietario.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.lblPropietario.Name = "lblPropietario";
            this.lblPropietario.Text = "Propietario *";
            // 
            // cboPropietario
            // 
            this.cboPropietario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboPropietario.Margin = new System.Windows.Forms.Padding(3, 6, 16, 6);
            this.cboPropietario.Name = "cboPropietario";
            this.cboPropietario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPropietario.FormattingEnabled = true;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Text = "Nombre *";
            // 
            // txtNombre
            // 
            this.txtNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNombre.Margin = new System.Windows.Forms.Padding(3, 6, 16, 6);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.MaxLength = 100;
            // 
            // lblEspecie
            // 
            this.lblEspecie.AutoSize = true;
            this.lblEspecie.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEspecie.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblEspecie.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblEspecie.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.lblEspecie.Name = "lblEspecie";
            this.lblEspecie.Text = "Especie *";
            // 
            // cboEspecie
            // 
            this.cboEspecie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboEspecie.Margin = new System.Windows.Forms.Padding(3, 6, 16, 6);
            this.cboEspecie.Name = "cboEspecie";
            this.cboEspecie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboEspecie.FormattingEnabled = true;
            this.cboEspecie.MaxLength = 50;
            this.cboEspecie.Items.AddRange(new object[] {"Perro", "Gato", "Ave", "Conejo", "Roedor", "Reptil"});
            // 
            // lblRaza
            // 
            this.lblRaza.AutoSize = true;
            this.lblRaza.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRaza.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblRaza.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblRaza.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.lblRaza.Name = "lblRaza";
            this.lblRaza.Text = "Raza";
            // 
            // txtRaza
            // 
            this.txtRaza.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRaza.Margin = new System.Windows.Forms.Padding(3, 6, 16, 6);
            this.txtRaza.Name = "txtRaza";
            this.txtRaza.MaxLength = 80;
            // 
            // lblSexo
            // 
            this.lblSexo.AutoSize = true;
            this.lblSexo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSexo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSexo.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblSexo.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Text = "Sexo *";
            // 
            // cboSexo
            // 
            this.cboSexo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboSexo.Margin = new System.Windows.Forms.Padding(3, 6, 16, 6);
            this.cboSexo.Name = "cboSexo";
            this.cboSexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSexo.FormattingEnabled = true;
            this.cboSexo.Items.AddRange(new object[] {"Macho", "Hembra"});
            // 
            // lblNacimiento
            // 
            this.lblNacimiento.AutoSize = true;
            this.lblNacimiento.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNacimiento.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblNacimiento.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblNacimiento.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.lblNacimiento.Name = "lblNacimiento";
            this.lblNacimiento.Text = "Fecha nacimiento";
            // 
            // dtpNacimiento
            // 
            this.dtpNacimiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpNacimiento.Margin = new System.Windows.Forms.Padding(3, 6, 16, 6);
            this.dtpNacimiento.Name = "dtpNacimiento";
            this.dtpNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNacimiento.ShowCheckBox = true;
            // 
            // lblPeso
            // 
            this.lblPeso.AutoSize = true;
            this.lblPeso.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPeso.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPeso.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblPeso.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.lblPeso.Name = "lblPeso";
            this.lblPeso.Text = "Peso (kg)";
            // 
            // nudPeso
            // 
            this.nudPeso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudPeso.Margin = new System.Windows.Forms.Padding(3, 6, 16, 6);
            this.nudPeso.Name = "nudPeso";
            this.nudPeso.DecimalPlaces = 2;
            this.nudPeso.Maximum = new decimal(new int[] {999, 0, 0, 0});
            this.nudPeso.Minimum = new decimal(new int[] {0, 0, 0, 0});
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblColor.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblColor.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblColor.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.lblColor.Name = "lblColor";
            this.lblColor.Text = "Color";
            // 
            // txtColor
            // 
            this.txtColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtColor.Margin = new System.Windows.Forms.Padding(3, 6, 16, 6);
            this.txtColor.Name = "txtColor";
            this.txtColor.MaxLength = 50;
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
            // frmMascotas
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 244);
            this.ClientSize = new System.Drawing.Size(1000, 680);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "frmMascotas";
            this.Text = "Mascotas";
            this.frmMascotas.Controls.Add(this.tlpRaiz);
            this.Load += new System.EventHandler(this.frmMascotas_Load);
            this.Resize += new System.EventHandler(this.frmMascotas_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.nudPeso)).EndInit();
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
        private System.Windows.Forms.Label lblPropietario;
        private System.Windows.Forms.ComboBox cboPropietario;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblEspecie;
        private System.Windows.Forms.ComboBox cboEspecie;
        private System.Windows.Forms.Label lblRaza;
        private System.Windows.Forms.TextBox txtRaza;
        private System.Windows.Forms.Label lblSexo;
        private System.Windows.Forms.ComboBox cboSexo;
        private System.Windows.Forms.Label lblNacimiento;
        private System.Windows.Forms.DateTimePicker dtpNacimiento;
        private System.Windows.Forms.Label lblPeso;
        private System.Windows.Forms.NumericUpDown nudPeso;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label lblSeccion;
        private System.Windows.Forms.FlowLayoutPanel flpBotones;
        private Vista.Comun.BotonModerno btnNuevo;
        private Vista.Comun.BotonModerno btnGuardar;
        private Vista.Comun.BotonModerno btnEliminar;
        private Vista.Comun.PanelTarjeta pnlTabla;
        private System.Windows.Forms.DataGridView dgv;
    }
}
