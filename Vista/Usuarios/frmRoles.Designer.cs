namespace Vista.Usuarios
{
    partial class frmRoles
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.gbRoles = new System.Windows.Forms.GroupBox();
            this.lstRoles = new System.Windows.Forms.ListBox();
            this.gbPermisos = new System.Windows.Forms.GroupBox();
            this.clbPermisos = new System.Windows.Forms.CheckedListBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.gbRoles.SuspendLayout();
            this.gbPermisos.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(22, 90, 80);
            this.lblTitulo.Location = new System.Drawing.Point(14, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(400, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Roles y permisos";
            // 
            // gbRoles
            // 
            this.gbRoles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.gbRoles.Controls.Add(this.lstRoles);
            this.gbRoles.Location = new System.Drawing.Point(16, 56);
            this.gbRoles.Name = "gbRoles";
            this.gbRoles.Size = new System.Drawing.Size(260, 568);
            this.gbRoles.TabIndex = 1;
            this.gbRoles.TabStop = false;
            this.gbRoles.Text = "Roles";
            // 
            // lstRoles
            // 
            this.lstRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRoles.FormattingEnabled = true;
            this.lstRoles.ItemHeight = 19;
            this.lstRoles.Location = new System.Drawing.Point(3, 21);
            this.lstRoles.Name = "lstRoles";
            this.lstRoles.Size = new System.Drawing.Size(254, 544);
            this.lstRoles.TabIndex = 0;
            this.lstRoles.SelectedIndexChanged += new System.EventHandler(this.lstRoles_SelectedIndexChanged);
            // 
            // gbPermisos
            // 
            this.gbPermisos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPermisos.Controls.Add(this.clbPermisos);
            this.gbPermisos.Controls.Add(this.btnGuardar);
            this.gbPermisos.Controls.Add(this.lblDescripcion);
            this.gbPermisos.Location = new System.Drawing.Point(290, 56);
            this.gbPermisos.Name = "gbPermisos";
            this.gbPermisos.Size = new System.Drawing.Size(686, 568);
            this.gbPermisos.TabIndex = 2;
            this.gbPermisos.TabStop = false;
            this.gbPermisos.Text = "Permisos del rol seleccionado";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDescripcion.Location = new System.Drawing.Point(3, 21);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(680, 30);
            this.lblDescripcion.TabIndex = 0;
            this.lblDescripcion.Text = "Descripción del rol";
            // 
            // clbPermisos
            // 
            this.clbPermisos.CheckOnClick = true;
            this.clbPermisos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbPermisos.FormattingEnabled = true;
            this.clbPermisos.Location = new System.Drawing.Point(3, 51);
            this.clbPermisos.Name = "clbPermisos";
            this.clbPermisos.Size = new System.Drawing.Size(680, 478);
            this.clbPermisos.TabIndex = 1;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(31, 122, 109);
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(3, 529);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(680, 36);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar permisos";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // frmRoles
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(244, 247, 246);
            this.ClientSize = new System.Drawing.Size(992, 640);
            this.Controls.Add(this.gbPermisos);
            this.Controls.Add(this.gbRoles);
            this.Controls.Add(this.lblTitulo);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "frmRoles";
            this.Text = "Roles y permisos";
            this.Load += new System.EventHandler(this.frmRoles_Load);
            this.gbRoles.ResumeLayout(false);
            this.gbPermisos.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox gbRoles;
        private System.Windows.Forms.ListBox lstRoles;
        private System.Windows.Forms.GroupBox gbPermisos;
        private System.Windows.Forms.CheckedListBox clbPermisos;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Button btnGuardar;
    }
}
