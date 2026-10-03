using System;
using System.Windows.Forms;
using Modelos.Datos;
using Vista.Comun;

namespace Vista.Bitacora
{
    /// <summary>Consulta de la bitácora: actividades y errores registrados por el sistema.</summary>
    public partial class frmBitacora : Form
    {
        public frmBitacora()
        {
            InitializeComponent();
        }

        private void frmBitacora_Load(object sender, EventArgs e)
        {
            cboNivel.SelectedIndex = 0;
            Cargar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Cargar();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Cargar();
            }
        }

        private void Cargar()
        {
            try
            {
                string nivel = cboNivel.SelectedIndex <= 0 ? "" : cboNivel.SelectedItem.ToString();
                dgv.DataSource = BitacoraDatos.Listar(nivel, txtBuscar.Text.Trim());
                GridUtil.Ocultar(dgv, "idBitacora");
                GridUtil.Encabezado(dgv, "fecha", "Fecha");
                GridUtil.Encabezado(dgv, "nivel", "Nivel");
                GridUtil.Encabezado(dgv, "nombreUsuario", "Usuario");
                GridUtil.Encabezado(dgv, "modulo", "Módulo");
                GridUtil.Encabezado(dgv, "mensaje", "Mensaje");
                GridUtil.Formato(dgv, "fecha", "dd/MM/yyyy HH:mm:ss");
                if (dgv.Columns.Contains("mensaje")) dgv.Columns["mensaje"].FillWeight = 150;
            }
            catch (Exception ex)
            {
                Mensajes.Error("Bitácora", ex, "cargar");
            }
        }
    }
}
