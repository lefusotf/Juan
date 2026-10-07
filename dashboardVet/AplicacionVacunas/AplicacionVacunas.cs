using System;
using System.Data;
using System.Windows.Forms;
using dashboardVet.Base;
using dashboardVet.Controles;
using dashboardVet.Helpers;
using Modelos;

namespace dashboardVet.AplicacionVacunas
{
    public partial class AplicacionVacunas : FrmBase
    {
        private readonly MascotaDAL _mascotaDAL = new MascotaDAL();
        private readonly VacunaDAL _vacunaDAL = new VacunaDAL();
        private readonly AplicacionVacunaDAL _aplicacionVacunaDAL = new AplicacionVacunaDAL();
        private readonly UsuarioDAL _usuarioDAL = new UsuarioDAL();
        private const string Modulo = "Vacunas";
        private int _id;
        private bool _cargando;

        public AplicacionVacunas()
        {
            InitializeComponent();
        }

        private void AplicacionVacunas_Load(object sender, EventArgs e)
        {
            Responsive.Aplicar(tlpCampos, ClientSize.Width);
            ConfigurarEntradas();
            CargarCombos();

            bool puede = Sesion.Tiene(Permisos.VacunasGestionar);
            btnNuevo.Enabled = puede;
            btnGuardar.Enabled = puede;
            btnEliminar.Enabled = puede;
            pnlDatos.Enabled = puede;

            CargarDatos();
            Nuevo();
        }

        private void ConfigurarEntradas()
        {
            txtBuscar.MaxLength = 60;
        }

        private void CargarCombos()
        {
            cboMascota.DataSource = _mascotaDAL.ListarParaCombo();
            cboMascota.DisplayMember = "descripcion";
            cboMascota.ValueMember = "idMascota";

            cboVacuna.DataSource = _vacunaDAL.ListarParaCombo();
            cboVacuna.DisplayMember = "nombre";
            cboVacuna.ValueMember = "idVacuna";

            cboVeterinario.DataSource = _usuarioDAL.ListarVeterinarios();
            cboVeterinario.DisplayMember = "nombreCompleto";
            cboVeterinario.ValueMember = "idUsuario";
        }

        private void CargarDatos()
        {
            try
            {
                _cargando = true;
                dgv.DataSource = _aplicacionVacunaDAL.Listar(txtBuscar.Text.Trim());
                ConfigurarColumnas();
                dgv.ClearSelection();
            }
            catch (Exception ex)
            {
                ManejadorUIErrores.MostrarError(ex, Modulo);
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
                ManejadorUIErrores.MostrarError(ex, Modulo);
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
                Mensajes.Invalido("La fecha de aplicación no puede ser futura.", dtpAplicacion);
                return false;
            }
            if (dtpProxima.Checked && dtpProxima.Value.Date <= dtpAplicacion.Value.Date)
            {
                Mensajes.Invalido("La próxima dosis debe ser posterior a la fecha de aplicación.", dtpProxima);
                return false;
            }
            if (Mensajes.Invalido(Validaciones.LongitudMaxima(txtObservaciones.Text, 250, "Observaciones"), txtObservaciones)) return false;

            int idMascota = Convert.ToInt32(cboMascota.SelectedValue);
            int idVacuna = Convert.ToInt32(cboVacuna.SelectedValue);
            try
            {
                DataRow mascota = _mascotaDAL.ObtenerBasico(idMascota);
                string especieVacuna = _vacunaDAL.ObtenerEspecieDestino(idVacuna);
                if (mascota != null)
                {
                    if (!string.Equals(mascota["especie"].ToString(), especieVacuna, StringComparison.OrdinalIgnoreCase))
                    {
                        Mensajes.Invalido("La vacuna '" + cboVacuna.Text + "' es para " + especieVacuna +
                                          " y la mascota es de especie " + mascota["especie"] + ".", cboVacuna);
                        return false;
                    }
                    if (mascota["fechaNacimiento"] is DateTime && dtpAplicacion.Value.Date < (DateTime)mascota["fechaNacimiento"])
                    {
                        Mensajes.Invalido("La fecha de aplicación no puede ser anterior al nacimiento de la mascota.", dtpAplicacion);
                        return false;
                    }
                }
                if (_aplicacionVacunaDAL.ExisteAplicacion(idMascota, idVacuna, dtpAplicacion.Value, _id))
                {
                    Mensajes.Invalido("Esa vacuna ya fue registrada para esta mascota en la misma fecha.", dtpAplicacion);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ManejadorUIErrores.MostrarError(ex, Modulo);
                return false;
            }
            return true;
        }

        private void GuardarRegistro(bool esNuevo)
        {
            int idMascota = Convert.ToInt32(cboMascota.SelectedValue);
            int idVacuna = Convert.ToInt32(cboVacuna.SelectedValue);
            int idVeterinario = Convert.ToInt32(cboVeterinario.SelectedValue);
            DateTime fecha = dtpAplicacion.Value.Date;
            DateTime? proxima = dtpProxima.Checked ? (DateTime?)dtpProxima.Value.Date : null;
            string observaciones = Texto.PrimeraMayuscula(txtObservaciones.Text);
            if (esNuevo) _aplicacionVacunaDAL.Insertar(idMascota, idVacuna, idVeterinario, fecha, proxima, observaciones);
            else _aplicacionVacunaDAL.Actualizar(_id, idMascota, idVacuna, idVeterinario, fecha, proxima, observaciones);
        }

        private void EliminarRegistro(int id)
        {
            _aplicacionVacunaDAL.Eliminar(id);
        }

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

        private void AplicacionVacunas_Resize(object sender, EventArgs e)
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
                ManejadorUIErrores.MostrarError(ex, Modulo);
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
                ManejadorUIErrores.MostrarError(ex, Modulo);
            }
        }
    }
}
