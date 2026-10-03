using System;
using System.Drawing;
using System.Windows.Forms;
using Modelos.Datos;
using Vista.Comun;

namespace Vista.Bitacora
{
    /// <summary>Consulta de la bitácora: actividades y errores registrados por el sistema.</summary>
    public class frmBitacora : Form
    {
        private readonly ComboBox cboNivel = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 120 };
        private readonly TextBox txtBuscar = new TextBox { Width = 260 };
        private readonly DataGridView dgv = new DataGridView();

        public frmBitacora()
        {
            Font = Estilo.Fuente;
            BackColor = Estilo.Fondo;
            Text = "Bitácora";

            Label titulo = new Label
            {
                Text = "Bitácora del sistema",
                Font = Estilo.FuenteTitulo,
                ForeColor = Estilo.PrimarioOscuro,
                Dock = DockStyle.Top,
                Height = 48,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(16, 0, 0, 0)
            };

            cboNivel.Items.AddRange(new object[] { "Todos", "INFO", "ERROR" });
            cboNivel.SelectedIndex = 0;
            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Cargar(); }
            };
            Button btnBuscar = new Button { Text = "Buscar" };
            Estilo.Boton(btnBuscar, Estilo.Neutro);
            btnBuscar.Height = 28;
            btnBuscar.Width = 80;
            btnBuscar.Click += (s, e) => Cargar();

            FlowLayoutPanel barra = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 44, Padding = new Padding(16, 6, 0, 0) };
            barra.Controls.AddRange(new Control[]
            {
                new Label { Text = "Nivel:", AutoSize = true, Margin = new Padding(0, 8, 4, 0) }, cboNivel,
                new Label { Text = "Buscar:", AutoSize = true, Margin = new Padding(12, 8, 4, 0) }, txtBuscar, btnBuscar
            });

            Estilo.Grid(dgv);
            Panel cuerpo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(16, 0, 16, 16) };
            cuerpo.Controls.Add(dgv);

            Controls.Add(cuerpo);
            Controls.Add(barra);
            Controls.Add(titulo);
            Load += (s, e) => Cargar();
        }

        private void Cargar()
        {
            try
            {
                string nivel = cboNivel.SelectedIndex <= 0 ? "" : cboNivel.SelectedItem.ToString();
                dgv.DataSource = BitacoraDatos.Listar(nivel, txtBuscar.Text.Trim());
                if (dgv.Columns.Contains("idBitacora")) dgv.Columns["idBitacora"].Visible = false;
                if (dgv.Columns.Contains("fecha"))
                {
                    dgv.Columns["fecha"].HeaderText = "Fecha";
                    dgv.Columns["fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                    dgv.Columns["fecha"].FillWeight = 40;
                }
                if (dgv.Columns.Contains("nivel")) { dgv.Columns["nivel"].HeaderText = "Nivel"; dgv.Columns["nivel"].FillWeight = 15; }
                if (dgv.Columns.Contains("nombreUsuario")) { dgv.Columns["nombreUsuario"].HeaderText = "Usuario"; dgv.Columns["nombreUsuario"].FillWeight = 25; }
                if (dgv.Columns.Contains("modulo")) { dgv.Columns["modulo"].HeaderText = "Módulo"; dgv.Columns["modulo"].FillWeight = 25; }
                if (dgv.Columns.Contains("mensaje")) { dgv.Columns["mensaje"].HeaderText = "Mensaje"; dgv.Columns["mensaje"].FillWeight = 120; }
            }
            catch (Exception ex)
            {
                Mensajes.Error("Bitácora", ex, "cargar");
            }
        }
    }
}
