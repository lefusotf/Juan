using System.Windows.Forms;

namespace dashboardVet.Helpers
{
    public static class GridUtil
    {
        public static void Ocultar(DataGridView dgv, params string[] columnas)
        {
            foreach (string c in columnas)
                if (dgv.Columns.Contains(c)) dgv.Columns[c].Visible = false;
        }

        public static void Encabezado(DataGridView dgv, string columna, string texto)
        {
            if (dgv.Columns.Contains(columna)) dgv.Columns[columna].HeaderText = texto;
        }

        public static void Formato(DataGridView dgv, string columna, string formato)
        {
            if (dgv.Columns.Contains(columna)) dgv.Columns[columna].DefaultCellStyle.Format = formato;
        }
    }
}
