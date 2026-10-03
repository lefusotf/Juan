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
            this.tlpRaiz = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.tlpCuerpo = new System.Windows.Forms.TableLayoutPanel();
            this.pnlRoles = new Vista.Comun.PanelTarjeta();
            this.lstRoles = new System.Windows.Forms.ListBox();
            this.lblRolesTitulo = new System.Windows.Forms.Label();
            this.pnlPermisos = new Vista.Comun.PanelTarjeta();
            this.clbPermisos = new System.Windows.Forms.CheckedListBox();
            this.pnlEspacio = new System.Windows.Forms.Panel();
            this.btnGuardar = new Vista.Comun.BotonModerno();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblPermisosTitulo = new System.Windows.Forms.Label();
            this.tlpRaiz.SuspendLayout();
            this.tlpCuerpo.SuspendLayout();
            this.pnlRoles.SuspendLayout();
            this.pnlPermisos.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpRaiz
            // 
            this.tlpRaiz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRaiz.Name = "tlpRaiz";
            this.tlpRaiz.Padding = new System.Windows.Forms.Padding(28, 24, 28, 20);
            this.tlpRaiz.ColumnCount = 1;
            this.tlpRaiz.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRaiz.RowCount = 2;
            this.tlpRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRaiz.Controls.Add(this.lblTitulo, 0, 0);
            this.tlpRaiz.Controls.Add(this.tlpCuerpo, 0, 1);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(17, 94, 89);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "Roles y permisos";
            // 
            // tlpCuerpo
            // 
            this.tlpCuerpo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCuerpo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpCuerpo.Name = "tlpCuerpo";
            this.tlpCuerpo.ColumnCount = 2;
            this.tlpCuerpo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpCuerpo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpCuerpo.RowCount = 1;
            this.tlpCuerpo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCuerpo.Controls.Add(this.pnlRoles, 0, 0);
            this.tlpCuerpo.Controls.Add(this.pnlPermisos, 1, 0);
            // 
            // pnlRoles
            // 
            this.pnlRoles.Name = "pnlRoles";
            this.pnlRoles.Padding = new System.Windows.Forms.Padding(18, 18, 18, 18);
            this.pnlRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRoles.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.pnlRoles.Controls.Add(this.lstRoles);
            this.pnlRoles.Controls.Add(this.lblRolesTitulo);
            // 
            // lstRoles
            // 
            this.lstRoles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRoles.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lstRoles.FormattingEnabled = true;
            this.lstRoles.ItemHeight = 20;
            this.lstRoles.Name = "lstRoles";
            this.lstRoles.SelectedIndexChanged += new System.EventHandler(this.lstRoles_SelectedIndexChanged);
            // 
            // lblRolesTitulo
            // 
            this.lblRolesTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRolesTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblRolesTitulo.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblRolesTitulo.Height = 34;
            this.lblRolesTitulo.Name = "lblRolesTitulo";
            this.lblRolesTitulo.Text = "Roles";
            this.lblRolesTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlPermisos
            // 
            this.pnlPermisos.Name = "pnlPermisos";
            this.pnlPermisos.Padding = new System.Windows.Forms.Padding(18, 18, 18, 18);
            this.pnlPermisos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPermisos.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.pnlPermisos.Controls.Add(this.clbPermisos);
            this.pnlPermisos.Controls.Add(this.pnlEspacio);
            this.pnlPermisos.Controls.Add(this.btnGuardar);
            this.pnlPermisos.Controls.Add(this.lblDescripcion);
            this.pnlPermisos.Controls.Add(this.lblPermisosTitulo);
            // 
            // clbPermisos
            // 
            this.clbPermisos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbPermisos.CheckOnClick = true;
            this.clbPermisos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbPermisos.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.clbPermisos.FormattingEnabled = true;
            this.clbPermisos.Name = "clbPermisos";
            // 
            // pnlEspacio
            // 
            this.pnlEspacio.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlEspacio.Height = 12;
            this.pnlEspacio.Name = "pnlEspacio";
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(15, 118, 110);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(200, 44);
            this.btnGuardar.Text = "Guardar permisos";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblDescripcion.Height = 30;
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Text = "Descripción del rol";
            this.lblDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPermisosTitulo
            // 
            this.lblPermisosTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPermisosTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPermisosTitulo.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblPermisosTitulo.Height = 34;
            this.lblPermisosTitulo.Name = "lblPermisosTitulo";
            this.lblPermisosTitulo.Text = "Permisos del rol seleccionado";
            this.lblPermisosTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmRoles
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 244);
            this.ClientSize = new System.Drawing.Size(1000, 680);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "frmRoles";
            this.Text = "Roles y permisos";
            this.Controls.Add(this.tlpRaiz);
            this.Load += new System.EventHandler(this.frmRoles_Load);
            this.pnlPermisos.ResumeLayout(false);
            this.pnlRoles.ResumeLayout(false);
            this.tlpCuerpo.ResumeLayout(false);
            this.tlpCuerpo.PerformLayout();
            this.tlpRaiz.ResumeLayout(false);
            this.tlpRaiz.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRaiz;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TableLayoutPanel tlpCuerpo;
        private Vista.Comun.PanelTarjeta pnlRoles;
        private System.Windows.Forms.ListBox lstRoles;
        private System.Windows.Forms.Label lblRolesTitulo;
        private Vista.Comun.PanelTarjeta pnlPermisos;
        private System.Windows.Forms.CheckedListBox clbPermisos;
        private System.Windows.Forms.Panel pnlEspacio;
        private Vista.Comun.BotonModerno btnGuardar;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblPermisosTitulo;
    }
}
