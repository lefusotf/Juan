using System;
using System.Data;
using System.Windows.Forms;
using dashboardVet.Base;
using dashboardVet.Controles;
using dashboardVet.Helpers;
using Modelos;

namespace dashboardVet.Citas
{
    public partial class Citas : FrmBase
    {
        private readonly MascotaDAL _mascotaDAL = new MascotaDAL();
        private readonly CitaDAL _citaDAL = new CitaDAL();
        private readonly UsuarioDAL _usuarioDAL = new UsuarioDAL();
        private const string Modulo = "Citas";
        private int _id;
        private bool _cargando;

        public Citas()
        {
            InitializeComponent();
        }

        private void Citas_Load(object sender, EventArgs e)
        {
            Responsive.Aplicar(tlpCampos, ClientSize.Width);
            ConfigurarEntradas();
            CargarCombos();

            bool puede = Sesion.Tiene(Permisos.CitasGestionar);
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

            cboVeterinario.DataSource = _usuarioDAL.ListarVeterinarios();
            cboVeterinario.DisplayMember = "nombreCompleto";
            cboVeterinario.ValueMember = "idUsuario";
        }

        private void CargarDatos()
        {
            try
            {
                _cargando = true;
                dgv.DataSource = _citaDAL.Listar(txtBuscar.Text.Trim());
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
            GridUtil.Ocultar(dgv, "idCita", "idMascota", "idVeterinario");
            GridUtil.Encabezado(dgv, "mascota", "Mascota");
            GridUtil.Encabezado(dgv, "propietario", "Propietario");
            GridUtil.Encabezado(dgv, "veterinario", "Veterinario");
            GridUtil.Encabezado(dgv, "fechaHora", "Fecha y hora");
            GridUtil.Encabezado(dgv, "motivo", "Motivo");
            GridUtil.Encabezado(dgv, "estado", "Estado");
            GridUtil.Formato(dgv, "fechaHora", "dd/MM/yyyy HH:mm");
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (_cargando || dgv.CurrentRow == null || !dgv.CurrentRow.Selected) return;
            try
            {
                _id = Convert.ToInt32(dgv.CurrentRow.Cells["idCita"].Value);
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
            cboVeterinario.SelectedValue = Convert.ToInt32(f.Cells["idVeterinario"].Value);
            dtpFechaHora.Value = (DateTime)f.Cells["fechaHora"].Value;
            cboEstado.SelectedItem = f.Cells["estado"].Value.ToString();
            txtMotivo.Text = f.Cells["motivo"].Value.ToString();
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
            if (cboVeterinario.Items.Count > 0) cboVeterinario.SelectedIndex = 0;
            dtpFechaHora.Value = DateTime.Now.AddHours(1);
            cboEstado.SelectedIndex = 0;
            txtMotivo.Clear();
            cboMascota.Focus();
        }

        private bool Validar()
        {
            if (cboMascota.SelectedValue == null)
            {
                Mensajes.Advertencia("Seleccione una mascota. Si no existe, regístrela primero en el módulo de mascotas.");
                cboMascota.Focus();
                return false;
            }
            if (cboVeterinario.SelectedValue == null)
            {
                Mensajes.Advertencia("No hay veterinarios activos disponibles. El administrador debe registrar uno.");
                cboVeterinario.Focus();
                return false;
            }
            if (Mensajes.Invalido(Validaciones.Minimo(txtMotivo.Text, 3, "Motivo"), txtMotivo)) return false;
            if (Mensajes.Invalido(Validaciones.LongitudMaxima(txtMotivo.Text, 250, "Motivo"), txtMotivo)) return false;

            bool programada = cboEstado.SelectedItem.ToString() == "Programada";
            DateTime fecha = FechaSinSegundos();

            if (programada && _id == 0 && fecha < DateTime.Now)
            {
                Mensajes.Invalido("La fecha y hora de una cita nueva no puede estar en el pasado.", dtpFechaHora);
                return false;
            }
            if (programada && Mensajes.Invalido(Validaciones.HorarioCita(fecha), dtpFechaHora)) return false;

            if (programada)
            {
                try
                {
                    if (_citaDAL.VeterinarioOcupado(Convert.ToInt32(cboVeterinario.SelectedValue), fecha, _id))
                    {
                        Mensajes.Invalido("El veterinario ya tiene una cita programada en ese horario (debe haber al menos 30 minutos entre citas).", dtpFechaHora);
                        return false;
                    }
                    if (_citaDAL.MascotaTieneCitaEseDia(Convert.ToInt32(cboMascota.SelectedValue), fecha, _id))
                    {
                        Mensajes.Invalido("La mascota ya tiene una cita programada ese mismo día.", cboMascota);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    ManejadorUIErrores.MostrarError(ex, Modulo);
                    return false;
                }
            }
            return true;
        }

        private void GuardarRegistro(bool esNuevo)
        {
            int idMascota = Convert.ToInt32(cboMascota.SelectedValue);
            int idVeterinario = Convert.ToInt32(cboVeterinario.SelectedValue);
            DateTime fechaHora = FechaSinSegundos();
            string motivo = Texto.PrimeraMayuscula(txtMotivo.Text);
            string estado = cboEstado.SelectedItem.ToString();
            if (esNuevo) _citaDAL.Insertar(idMascota, idVeterinario, fechaHora, motivo, estado, Sesion.UsuarioActual.IdUsuario);
            else _citaDAL.Actualizar(_id, idMascota, idVeterinario, fechaHora, motivo, estado);
        }

        private void EliminarRegistro(int id)
        {
            _citaDAL.Eliminar(id);
        }

        private DateTime FechaSinSegundos()
        {
            DateTime v = dtpFechaHora.Value;
            return new DateTime(v.Year, v.Month, v.Day, v.Hour, v.Minute, 0);
        }

        private void Citas_Resize(object sender, EventArgs e)
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
