using System.Drawing;
using System.Windows.Forms;

namespace Vista.Comun
{
    /// <summary>Colores y estilos compartidos para mantener una interfaz coherente en todo el sistema.</summary>
    public static class Estilo
    {
        public static readonly Color Primario = Color.FromArgb(31, 122, 109);
        public static readonly Color PrimarioOscuro = Color.FromArgb(22, 90, 80);
        public static readonly Color Peligro = Color.FromArgb(192, 57, 43);
        public static readonly Color Neutro = Color.FromArgb(108, 117, 125);
        public static readonly Color Fondo = Color.FromArgb(244, 247, 246);
        public static readonly Font Fuente = new Font("Segoe UI", 10F);
        public static readonly Font FuenteTitulo = new Font("Segoe UI", 16F, FontStyle.Bold);

        public static void Boton(Button b, Color color)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.BackColor = color;
            b.ForeColor = Color.White;
            b.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            b.Cursor = Cursors.Hand;
            b.Height = 36;
            b.Width = 110;
        }

        public static void Grid(DataGridView g)
        {
            g.Dock = DockStyle.Fill;
            g.ReadOnly = true;
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.AllowUserToResizeRows = false;
            g.MultiSelect = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            g.RowHeadersVisible = false;
            g.BackgroundColor = Color.White;
            g.BorderStyle = BorderStyle.None;
            g.EnableHeadersVisualStyles = false;
            g.ColumnHeadersDefaultCellStyle.BackColor = Primario;
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            g.ColumnHeadersHeight = 34;
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 225);
            g.DefaultCellStyle.SelectionForeColor = Color.Black;
            g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 250, 249);
        }
    }
}
