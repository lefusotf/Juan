namespace Vista.Vacunas
{
    partial class frmVacunas
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabAplicacion = new System.Windows.Forms.TabPage();
            this.tabCatalogo = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabAplicacion);
            this.tabControl.Controls.Add(this.tabCatalogo);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1000, 680);
            this.tabControl.TabIndex = 0;
            // 
            // tabAplicacion
            // 
            this.tabAplicacion.BackColor = System.Drawing.Color.FromArgb(244, 247, 246);
            this.tabAplicacion.Location = new System.Drawing.Point(4, 28);
            this.tabAplicacion.Name = "tabAplicacion";
            this.tabAplicacion.Size = new System.Drawing.Size(992, 648);
            this.tabAplicacion.TabIndex = 0;
            this.tabAplicacion.Text = "Aplicación de vacunas";
            // 
            // tabCatalogo
            // 
            this.tabCatalogo.BackColor = System.Drawing.Color.FromArgb(244, 247, 246);
            this.tabCatalogo.Location = new System.Drawing.Point(4, 28);
            this.tabCatalogo.Name = "tabCatalogo";
            this.tabCatalogo.Size = new System.Drawing.Size(992, 648);
            this.tabCatalogo.TabIndex = 1;
            this.tabCatalogo.Text = "Catálogo de vacunas";
            // 
            // frmVacunas
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(244, 247, 246);
            this.ClientSize = new System.Drawing.Size(1000, 680);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "frmVacunas";
            this.Text = "Vacunas";
            this.Load += new System.EventHandler(this.frmVacunas_Load);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabAplicacion;
        private System.Windows.Forms.TabPage tabCatalogo;
    }
}
