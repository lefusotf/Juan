using System;
using System.Data;
using System.Windows.Forms;
using Modelos.Datos;
using Modelos.Entidades;
using Modelos.Seguridad;
using Modelos.Utilidades;
using Vista.Comun;

namespace Vista.Vacunas
{
    public partial class frmAplicacionVacunas : Form
    {
        private const string Modulo = "Vacunas";
        private int _id;          // id del registro seleccionado (0 = registro nuevo)
        private bool _cargando;   // evita reaccionar a la selección mientras se llena la tabla

        public frmAplicacionVacunas()
        {
            InitializeComponent();
        }

        private void frmAplicacionVacunas_Load(object sender, EventArgs e)
        {
            Responsive.Aplicar(tlpCampos, ClientSize.Width);
            ConfigurarEntradas();
            CargarCombos();

            // Control de permisos: sin permiso de gestión solo se puede consultar
            bool puede = Sesion.Tiene(Permisos.VacunasGestionar);
            btnNuevo.Enabled = puede;
            btnGuardar.Enabled = puede;
            btnEliminar.Enabled = puede;
            pnlDatos.Enabled = puede;

            CargarDatos();
            Nuevo();
        }

        // Restricciones de escritura: bloquean letras o números según el campo
        private void ConfigurarEntradas()
        {
            // (sin acciones)
        }

        private void CargarCombos()
        {
            cboMascota.DataSource = MascotaDatos.ListarParaCombo();
            cboMascota.DisplayMember = "descripcion";
            cboMascota.ValueMember = "idMascota";

            cboVacuna.DataSource = VacunaDatos.ListarParaCombo();
            cboVacuna.DisplayMember = "nombre";
            cboVacuna.ValueMember = "idVacuna";

            cboVeterinario.DataSource = UsuarioDatos.ListarVeterinarios();
            cboVeterinario.DisplayMember = "nombreCompleto";
            cboVeterinario.ValueMember = "idUsuario";
        }

        private void CargarDatos()
        {
            try
            {
                _cargando = true;
                dgv.DataSource = AplicacionVacunaDatos.Listar(txtBuscar.Text.Trim());
                ConfigurarColumnas();
                dgv.ClearSelection();
            }
            catch (Exception ex)
            {
                Mensajes.Error(Modulo, ex, "cargar");
            }
            finally
            {
                _cargando = false;
            }
        }

        private void ConfigurarColumnas()
        {
            GridUtil.Ocultar(dgv, "idAplicacion", "idMascota", "idVacuna", "idVeterinario");
            GridUtil.Encabezado(dgv, "mascota", "Mascota");
            GridUtil.Encabezado(dgv, "vacuna", "Vacuna");
            GridUtil.Encabezado(dgv, "veterinario", "Veterinario");
            GridUtil.Encabezado(dgv, "fechaAplicacion", "Aplicación");
            GridUtil.Encabezado(dgv, "proximaDosis", "Próxima dosis");
            GridUtil.Encabezado(dgv, "observaciones", "Observaciones");
            GridUtil.Formato(dgv, "fechaAplicacion", "dd/MM/yyyy");
            GridUtil.Formato(dgv, "proximaDosis", "dd/MM/yyyy");
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (_cargando || dgv.CurrentRow == null || !dgv.CurrentRow.Selected) return;
            try
            {
                _id = Convert.ToInt32(dgv.CurrentRow.Cells["idAplicacion"].Value);
                MostrarFila(dgv.CurrentRow);
            }
            catch (Exception ex)
            {
                Mensajes.Error(Modulo, ex, "cargar");
            }
        }

        private void MostrarFila(DataGridViewRow f)
        {
            cboMascota.SelectedValue = Convert.ToInt32(f.Cells["idMascota"].Value);
            cboVacuna.SelectedValue = Convert.ToInt32(f.Cells["idVacuna"].Value);
            cboVeterinario.SelectedValue = Convert.ToInt32(f.Cells["idVeterinario"].Value);
            dtpAplicacion.Value = (DateTime)f.Cells["fechaAplicacion"].Value;
            if (f.Cells["proximaDosis"].Value is DateTime)
            {
                dtpProxima.Value = (DateTime)f.Cells["proximaDosis"].Value;
                dtpProxima.Checked = true;
            }
            else
            {
                dtpProxima.Checked = false;
            }
            txtObservaciones.Text = f.Cells["observaciones"].Value.ToString();
        }

        private void Nuevo()
        {
            _id = 0;
            LimpiarCampos();
            dgv.ClearSelection();
        }

        private void LimpiarCampos()
        {
            if (cboMascota.Items.Count > 0) cboMascota.SelectedIndex = 0;
            if (cboVacuna.Items.Count > 0) cboVacuna.SelectedIndex = 0;
            if (Sesion.UsuarioActual.Rol == "Veterinario")
                cboVeterinario.SelectedValue = Sesion.UsuarioActual.IdUsuario;
            else if (cboVeterinario.Items.Count > 0)
                cboVeterinario.SelectedIndex = 0;
            dtpAplicacion.Value = DateTime.Today;
            SugerirProximaDosis();
            txtObservaciones.Clear();
            cboMascota.Focus();
        }

        private bool Validar()
        {
            if (cboMascota.SelectedValue == null || cboVacuna.SelectedValue == null)
            {
                Mensajes.Advertencia("Seleccione la mascota y la vacuna a aplicar.");
                return false;
            }
            if (cboVeterinario.SelectedValue == null)
            {
                Mensajes.Advertencia("Seleccione el veterinario que aplica la vacuna.");
                cboVeterinario.Focus();
                return false;
            }
            if (dtpAplicacion.Value.Date > DateTime.Today)
            {
                Mensajes.Advertencia("La fecha de aplicación no puede ser futura.");
                dtpAplicacion.Focus();
                return false;
            }
            if (dtpProxima.Checked && dtpProxima.Value.Date <= dtpAplicacion.Value.Date)
            {
                Mensajes.Advertencia("La próxima dosis debe ser posterior a la fecha de aplicación.");
                dtpProxima.Focus();
                return false;
            }
            return true;
        }

        private void GuardarRegistro(bool esNuevo)
        {
            AplicacionVacuna a = new AplicacionVacuna
            {
                IdAplicacion = _id,
                IdMascota = Convert.ToInt32(cboMascota.SelectedValue),
                IdVacuna = Convert.ToInt32(cboVacuna.SelectedValue),
                IdVeterinario = Convert.ToInt32(cboVeterinario.SelectedValue),
                FechaAplicacion = dtpAplicacion.Value.Date,
                ProximaDosis = dtpProxima.Checked ? (DateTime?)dtpProxima.Value.Date : null,
                Observaciones = txtObservaciones.Text.Trim()
            };
            if (esNuevo) AplicacionVacunaDatos.Insertar(a); else AplicacionVacunaDatos.Actualizar(a);
        }

        private void EliminarRegistro(int id)
        {
            AplicacionVacunaDatos.Eliminar(id);
        }

        // La próxima dosis se sugiere según el intervalo de la vacuna elegida
        private void SugerirProximaDosis()
        {
            DataRowView fila = cboVacuna.SelectedItem as DataRowView;
            if (fila == null) return;
            dtpProxima.Value = dtpAplicacion.Value.Date.AddDays(Convert.ToInt32(fila["intervaloDias"]));
            dtpProxima.Checked = true;
        }

        private void cboVacuna_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SugerirProximaDosis();
        }

        private void dtpAplicacion_ValueChanged(object sender, EventArgs e)
        {
            SugerirProximaDosis();
        }

        // ---------------- Eventos de la pantalla ----------------

        // Diseño adaptable: una o dos columnas de campos según el ancho disponible
        private void frmAplicacionVacunas_Resize(object sender, EventArgs e)
        {
            Responsive.Aplicar(tlpCampos, ClientSize.Width);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                CargarDatos();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;
            bool esNuevo = _id == 0;
            try
            {
                GuardarRegistro(esNuevo);
                Logger.Info(Modulo, esNuevo ? "Registro creado" : "Registro actualizado (id " + _id + ")");
                Mensajes.Info(esNuevo ? "Registro guardado correctamente." : "Registro actualizado correctamente.");
                CargarDatos();
                Nuevo();
            }
            catch (Exception ex)
            {
                Mensajes.Error(Modulo, ex, "guardar");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_id == 0)
            {
                Mensajes.Advertencia("Seleccione un registro de la tabla para eliminarlo.");
                return;
            }
            if (!Mensajes.Confirmar("¿Está seguro de eliminar el registro seleccionado?")) return;
            try
            {
                int id = _id;
                EliminarRegistro(id);
                Logger.Info(Modulo, "Registro eliminado (id " + id + ")");
                Mensajes.Info("Registro eliminado correctamente.");
                CargarDatos();
                Nuevo();
            }
            catch (Exception ex)
            {
                Mensajes.Error(Modulo, ex, "eliminar");
            }
        }
    }
}
