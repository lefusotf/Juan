namespace dashboardVet.Inicio
{
    partial class Inicio
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
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.flpTarjetas = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlTProp = new dashboardVet.Controles.PanelTarjeta();
            this.lblIProp = new System.Windows.Forms.Label();
            this.lblVProp = new System.Windows.Forms.Label();
            this.lblEProp = new System.Windows.Forms.Label();
            this.pnlTMasc = new dashboardVet.Controles.PanelTarjeta();
            this.lblIMasc = new System.Windows.Forms.Label();
            this.lblVMasc = new System.Windows.Forms.Label();
            this.lblEMasc = new System.Windows.Forms.Label();
            this.pnlTCita = new dashboardVet.Controles.PanelTarjeta();
            this.lblICita = new System.Windows.Forms.Label();
            this.lblVCita = new System.Windows.Forms.Label();
            this.lblECita = new System.Windows.Forms.Label();
            this.pnlTCons = new dashboardVet.Controles.PanelTarjeta();
            this.lblICons = new System.Windows.Forms.Label();
            this.lblVCons = new System.Windows.Forms.Label();
            this.lblECons = new System.Windows.Forms.Label();
            this.pnlTVac = new dashboardVet.Controles.PanelTarjeta();
            this.lblIVac = new System.Windows.Forms.Label();
            this.lblVVac = new System.Windows.Forms.Label();
            this.lblEVac = new System.Windows.Forms.Label();
            this.pnlCitas = new dashboardVet.Controles.PanelTarjeta();
            this.dgvCitas = new System.Windows.Forms.DataGridView();
            this.lblCitas = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).BeginInit();
            this.tlpRaiz.SuspendLayout();
            this.flpTarjetas.SuspendLayout();
            this.pnlTProp.SuspendLayout();
            this.pnlTMasc.SuspendLayout();
            this.pnlTCita.SuspendLayout();
            this.pnlTCons.SuspendLayout();
            this.pnlTVac.SuspendLayout();
            this.pnlCitas.SuspendLayout();
            this.SuspendLayout();

            this.tlpRaiz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRaiz.Name = "tlpRaiz";
            this.tlpRaiz.Padding = new System.Windows.Forms.Padding(28, 24, 28, 20);
            this.tlpRaiz.ColumnCount = 1;
            this.tlpRaiz.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRaiz.RowCount = 4;
            this.tlpRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRaiz.Controls.Add(this.lblBienvenida, 0, 0);
            this.tlpRaiz.Controls.Add(this.lblSubtitulo, 0, 1);
            this.tlpRaiz.Controls.Add(this.flpTarjetas, 0, 2);
            this.tlpRaiz.Controls.Add(this.pnlCitas, 0, 3);

            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblBienvenida.ForeColor = System.Drawing.Color.FromArgb(17, 94, 89);
            this.lblBienvenida.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Text = "Bienvenido(a)";

            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSubtitulo.Margin = new System.Windows.Forms.Padding(0, 2, 0, 18);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Text = "Resumen general de la clínica";

            this.flpTarjetas.AutoSize = true;
            this.flpTarjetas.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpTarjetas.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.flpTarjetas.Name = "flpTarjetas";
            this.flpTarjetas.Controls.Add(this.pnlTProp);
            this.flpTarjetas.Controls.Add(this.pnlTMasc);
            this.flpTarjetas.Controls.Add(this.pnlTCita);
            this.flpTarjetas.Controls.Add(this.pnlTCons);
            this.flpTarjetas.Controls.Add(this.pnlTVac);

            this.pnlTProp.Margin = new System.Windows.Forms.Padding(0, 0, 16, 16);
            this.pnlTProp.Name = "pnlTProp";
            this.pnlTProp.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.pnlTProp.Size = new System.Drawing.Size(236, 112);
            this.pnlTProp.Controls.Add(this.lblIProp);
            this.pnlTProp.Controls.Add(this.lblVProp);
            this.pnlTProp.Controls.Add(this.lblEProp);

            this.lblIProp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblIProp.AutoSize = true;
            this.lblIProp.Font = new System.Drawing.Font("Segoe UI Emoji", 22F);
            this.lblIProp.ForeColor = System.Drawing.Color.FromArgb(15, 118, 110);
            this.lblIProp.Location = new System.Drawing.Point(168, 14);
            this.lblIProp.Name = "lblIProp";
            this.lblIProp.Text = "\U0001F464";

            this.lblVProp.AutoSize = true;
            this.lblVProp.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblVProp.ForeColor = System.Drawing.Color.FromArgb(15, 118, 110);
            this.lblVProp.Location = new System.Drawing.Point(18, 14);
            this.lblVProp.Name = "lblVProp";
            this.lblVProp.Text = "0";

            this.lblEProp.AutoSize = true;
            this.lblEProp.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEProp.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblEProp.Location = new System.Drawing.Point(20, 70);
            this.lblEProp.Name = "lblEProp";
            this.lblEProp.Text = "Propietarios";

            this.pnlTMasc.Margin = new System.Windows.Forms.Padding(0, 0, 16, 16);
            this.pnlTMasc.Name = "pnlTMasc";
            this.pnlTMasc.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.pnlTMasc.Size = new System.Drawing.Size(236, 112);
            this.pnlTMasc.Controls.Add(this.lblIMasc);
            this.pnlTMasc.Controls.Add(this.lblVMasc);
            this.pnlTMasc.Controls.Add(this.lblEMasc);

            this.lblIMasc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblIMasc.AutoSize = true;
            this.lblIMasc.Font = new System.Drawing.Font("Segoe UI Emoji", 22F);
            this.lblIMasc.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblIMasc.Location = new System.Drawing.Point(168, 14);
            this.lblIMasc.Name = "lblIMasc";
            this.lblIMasc.Text = "\U0001F43E";

            this.lblVMasc.AutoSize = true;
            this.lblVMasc.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblVMasc.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblVMasc.Location = new System.Drawing.Point(18, 14);
            this.lblVMasc.Name = "lblVMasc";
            this.lblVMasc.Text = "0";

            this.lblEMasc.AutoSize = true;
            this.lblEMasc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEMasc.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblEMasc.Location = new System.Drawing.Point(20, 70);
            this.lblEMasc.Name = "lblEMasc";
            this.lblEMasc.Text = "Mascotas";

            this.pnlTCita.Margin = new System.Windows.Forms.Padding(0, 0, 16, 16);
            this.pnlTCita.Name = "pnlTCita";
            this.pnlTCita.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.pnlTCita.Size = new System.Drawing.Size(236, 112);
            this.pnlTCita.Controls.Add(this.lblICita);
            this.pnlTCita.Controls.Add(this.lblVCita);
            this.pnlTCita.Controls.Add(this.lblECita);

            this.lblICita.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblICita.AutoSize = true;
            this.lblICita.Font = new System.Drawing.Font("Segoe UI Emoji", 22F);
            this.lblICita.ForeColor = System.Drawing.Color.FromArgb(217, 119, 6);
            this.lblICita.Location = new System.Drawing.Point(168, 14);
            this.lblICita.Name = "lblICita";
            this.lblICita.Text = "\U0001F4C5";

            this.lblVCita.AutoSize = true;
            this.lblVCita.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblVCita.ForeColor = System.Drawing.Color.FromArgb(217, 119, 6);
            this.lblVCita.Location = new System.Drawing.Point(18, 14);
            this.lblVCita.Name = "lblVCita";
            this.lblVCita.Text = "0";

            this.lblECita.AutoSize = true;
            this.lblECita.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblECita.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblECita.Location = new System.Drawing.Point(20, 70);
            this.lblECita.Name = "lblECita";
            this.lblECita.Text = "Citas programadas";

            this.pnlTCons.Margin = new System.Windows.Forms.Padding(0, 0, 16, 16);
            this.pnlTCons.Name = "pnlTCons";
            this.pnlTCons.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.pnlTCons.Size = new System.Drawing.Size(236, 112);
            this.pnlTCons.Controls.Add(this.lblICons);
            this.pnlTCons.Controls.Add(this.lblVCons);
            this.pnlTCons.Controls.Add(this.lblECons);

            this.lblICons.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblICons.AutoSize = true;
            this.lblICons.Font = new System.Drawing.Font("Segoe UI Emoji", 22F);
            this.lblICons.ForeColor = System.Drawing.Color.FromArgb(124, 58, 237);
            this.lblICons.Location = new System.Drawing.Point(168, 14);
            this.lblICons.Name = "lblICons";
            this.lblICons.Text = "\U0001FA7A";

            this.lblVCons.AutoSize = true;
            this.lblVCons.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblVCons.ForeColor = System.Drawing.Color.FromArgb(124, 58, 237);
            this.lblVCons.Location = new System.Drawing.Point(18, 14);
            this.lblVCons.Name = "lblVCons";
            this.lblVCons.Text = "0";

            this.lblECons.AutoSize = true;
            this.lblECons.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblECons.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblECons.Location = new System.Drawing.Point(20, 70);
            this.lblECons.Name = "lblECons";
            this.lblECons.Text = "Consultas";

            this.pnlTVac.Margin = new System.Windows.Forms.Padding(0, 0, 16, 16);
            this.pnlTVac.Name = "pnlTVac";
            this.pnlTVac.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.pnlTVac.Size = new System.Drawing.Size(236, 112);
            this.pnlTVac.Controls.Add(this.lblIVac);
            this.pnlTVac.Controls.Add(this.lblVVac);
            this.pnlTVac.Controls.Add(this.lblEVac);

            this.lblIVac.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblIVac.AutoSize = true;
            this.lblIVac.Font = new System.Drawing.Font("Segoe UI Emoji", 22F);
            this.lblIVac.ForeColor = System.Drawing.Color.FromArgb(225, 29, 72);
            this.lblIVac.Location = new System.Drawing.Point(168, 14);
            this.lblIVac.Name = "lblIVac";
            this.lblIVac.Text = "\U0001F489";

            this.lblVVac.AutoSize = true;
            this.lblVVac.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblVVac.ForeColor = System.Drawing.Color.FromArgb(225, 29, 72);
            this.lblVVac.Location = new System.Drawing.Point(18, 14);
            this.lblVVac.Name = "lblVVac";
            this.lblVVac.Text = "0";

            this.lblEVac.AutoSize = true;
            this.lblEVac.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEVac.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblEVac.Location = new System.Drawing.Point(20, 70);
            this.lblEVac.Name = "lblEVac";
            this.lblEVac.Text = "Vacunas aplicadas";

            this.pnlCitas.Name = "pnlCitas";
            this.pnlCitas.Padding = new System.Windows.Forms.Padding(18, 18, 18, 18);
            this.pnlCitas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCitas.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.pnlCitas.Controls.Add(this.dgvCitas);
            this.pnlCitas.Controls.Add(this.lblCitas);

            this.dgvCitas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCitas.Name = "dgvCitas";
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
            this.dgvCitas.AllowUserToAddRows = false;
            this.dgvCitas.AllowUserToDeleteRows = false;
            this.dgvCitas.AllowUserToResizeRows = false;
            this.dgvCitas.AlternatingRowsDefaultCellStyle = estiloAlt0;
            this.dgvCitas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCitas.BackgroundColor = System.Drawing.Color.White;
            this.dgvCitas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCitas.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCitas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvCitas.ColumnHeadersDefaultCellStyle = estiloEnc0;
            this.dgvCitas.ColumnHeadersHeight = 42;
            this.dgvCitas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCitas.DefaultCellStyle = estiloCelda0;
            this.dgvCitas.EnableHeadersVisualStyles = false;
            this.dgvCitas.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.dgvCitas.MultiSelect = false;
            this.dgvCitas.ReadOnly = true;
            this.dgvCitas.RowHeadersVisible = false;
            this.dgvCitas.RowTemplate.Height = 38;
            this.dgvCitas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.lblCitas.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCitas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCitas.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblCitas.Height = 36;
            this.lblCitas.Name = "lblCitas";
            this.lblCitas.Text = "Próximas citas";
            this.lblCitas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 244);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "Inicio";
            this.Size = new System.Drawing.Size(1000, 680);
            this.Controls.Add(this.tlpRaiz);
            this.Load += new System.EventHandler(this.Inicio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).EndInit();
            this.pnlCitas.ResumeLayout(false);
            this.pnlTVac.ResumeLayout(false);
            this.pnlTCons.ResumeLayout(false);
            this.pnlTCita.ResumeLayout(false);
            this.pnlTMasc.ResumeLayout(false);
            this.pnlTProp.ResumeLayout(false);
            this.flpTarjetas.ResumeLayout(false);
            this.flpTarjetas.PerformLayout();
            this.tlpRaiz.ResumeLayout(false);
            this.tlpRaiz.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRaiz;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.FlowLayoutPanel flpTarjetas;
        private dashboardVet.Controles.PanelTarjeta pnlTProp;
        private System.Windows.Forms.Label lblIProp;
        private System.Windows.Forms.Label lblVProp;
        private System.Windows.Forms.Label lblEProp;
        private dashboardVet.Controles.PanelTarjeta pnlTMasc;
        private System.Windows.Forms.Label lblIMasc;
        private System.Windows.Forms.Label lblVMasc;
        private System.Windows.Forms.Label lblEMasc;
        private dashboardVet.Controles.PanelTarjeta pnlTCita;
        private System.Windows.Forms.Label lblICita;
        private System.Windows.Forms.Label lblVCita;
        private System.Windows.Forms.Label lblECita;
        private dashboardVet.Controles.PanelTarjeta pnlTCons;
        private System.Windows.Forms.Label lblICons;
        private System.Windows.Forms.Label lblVCons;
        private System.Windows.Forms.Label lblECons;
        private dashboardVet.Controles.PanelTarjeta pnlTVac;
        private System.Windows.Forms.Label lblIVac;
        private System.Windows.Forms.Label lblVVac;
        private System.Windows.Forms.Label lblEVac;
        private dashboardVet.Controles.PanelTarjeta pnlCitas;
        private System.Windows.Forms.DataGridView dgvCitas;
        private System.Windows.Forms.Label lblCitas;
    }
}
