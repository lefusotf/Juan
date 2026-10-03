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
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.flpTabs = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTabAplicacion = new Vista.Comun.BotonModerno();
            this.btnTabCatalogo = new Vista.Comun.BotonModerno();
            this.flpTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContenido
            // 
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Name = "pnlContenido";
            // 
            // flpTabs
            // 
            this.flpTabs.AutoSize = true;
            this.flpTabs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpTabs.Name = "flpTabs";
            this.flpTabs.Padding = new System.Windows.Forms.Padding(28, 18, 28, 0);
            this.flpTabs.Controls.Add(this.btnTabAplicacion);
            this.flpTabs.Controls.Add(this.btnTabCatalogo);
            // 
            // btnTabAplicacion
            // 
            this.btnTabAplicacion.BackColor = System.Drawing.Color.FromArgb(15, 118, 110);
            this.btnTabAplicacion.ForeColor = System.Drawing.Color.White;
            this.btnTabAplicacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabAplicacion.Name = "btnTabAplicacion";
            this.btnTabAplicacion.Size = new System.Drawing.Size(210, 38);
            this.btnTabAplicacion.Text = "Aplicación de vacunas";
            this.btnTabAplicacion.UseVisualStyleBackColor = false;
            this.btnTabAplicacion.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnTabAplicacion.Click += new System.EventHandler(this.btnTabAplicacion_Click);
            // 
            // btnTabCatalogo
            // 
            this.btnTabCatalogo.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.btnTabCatalogo.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.btnTabCatalogo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabCatalogo.Name = "btnTabCatalogo";
            this.btnTabCatalogo.Size = new System.Drawing.Size(210, 38);
            this.btnTabCatalogo.Text = "Catálogo de vacunas";
            this.btnTabCatalogo.UseVisualStyleBackColor = false;
            this.btnTabCatalogo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.btnTabCatalogo.Click += new System.EventHandler(this.btnTabCatalogo_Click);
            // 
            // frmVacunas
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 244);
            this.ClientSize = new System.Drawing.Size(1000, 680);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "frmVacunas";
            this.Text = "Vacunas";
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.flpTabs);
            this.Load += new System.EventHandler(this.frmVacunas_Load);
            this.flpTabs.ResumeLayout(false);
            this.flpTabs.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.FlowLayoutPanel flpTabs;
        private Vista.Comun.BotonModerno btnTabAplicacion;
        private Vista.Comun.BotonModerno btnTabCatalogo;
    }
}
