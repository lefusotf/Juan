using System.Collections.Generic;
using System.Windows.Forms;

namespace Vista.Comun
{
    /// <summary>
    /// Diseño adaptable para los formularios de captura: en pantallas anchas los campos se muestran en
    /// dos columnas (4 columnas de tabla: etiqueta, campo, etiqueta, campo); en pantallas angostas pasan
    /// a una sola columna. Se usa sobre un TableLayoutPanel cuyos hijos son pares etiqueta/campo.
    /// </summary>
    public static class Responsive
    {
        private class Par
        {
            public Control Etiqueta;
            public Control Campo;
            public bool Completo;   // el campo ocupa todo el ancho
        }

        private class Estado
        {
            public List<Par> Pares;
            public bool Compacto;
        }

        public static void Aplicar(TableLayoutPanel tlp, int anchoDisponible, int umbral = 860)
        {
            bool compacto = anchoDisponible < umbral;

            Estado estado = tlp.Tag as Estado;
            if (estado == null)
            {
                estado = new Estado { Pares = Capturar(tlp), Compacto = false };
                tlp.Tag = estado;
            }
            if (estado.Compacto == compacto) return;

            estado.Compacto = compacto;
            Distribuir(tlp, estado.Pares, compacto ? 2 : 4);
        }

        private static List<Par> Capturar(TableLayoutPanel tlp)
        {
            List<Par> pares = new List<Par>();
            foreach (Control c in tlp.Controls)
            {
                int columna = tlp.GetColumn(c);
                if (columna % 2 != 0) continue;
                Control campo = tlp.GetControlFromPosition(columna + 1, tlp.GetRow(c));
                if (campo == null) continue;
                pares.Add(new Par { Etiqueta = c, Campo = campo, Completo = tlp.GetColumnSpan(campo) > 1 });
            }
            pares.Sort((a, b) =>
            {
                int fa = tlp.GetRow(a.Etiqueta), fb = tlp.GetRow(b.Etiqueta);
                return fa != fb ? fa.CompareTo(fb) : tlp.GetColumn(a.Etiqueta).CompareTo(tlp.GetColumn(b.Etiqueta));
            });
            return pares;
        }

        private static void Distribuir(TableLayoutPanel tlp, List<Par> pares, int columnas)
        {
            tlp.SuspendLayout();
            tlp.Controls.Clear();
            tlp.ColumnStyles.Clear();
            tlp.RowStyles.Clear();
            tlp.ColumnCount = columnas;
            int grupos = columnas / 2;
            for (int i = 0; i < grupos; i++)
            {
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / grupos));
            }

            int col = 0, fila = 0;
            foreach (Par p in pares)
            {
                if (p.Completo && col != 0) { col = 0; fila++; }

                tlp.Controls.Add(p.Etiqueta, col, fila);
                tlp.Controls.Add(p.Campo, col + 1, fila);
                tlp.SetColumnSpan(p.Campo, p.Completo ? columnas - 1 : 1);

                if (p.Completo) { col = 0; fila++; }
                else { col += 2; if (col >= columnas) { col = 0; fila++; } }
            }

            int filas = col == 0 ? fila : fila + 1;
            tlp.RowCount = filas < 1 ? 1 : filas;
            for (int i = 0; i < tlp.RowCount; i++)
                tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlp.ResumeLayout(true);
        }
    }
}
