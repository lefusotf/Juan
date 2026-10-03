using System;
using System.Data;
using System.Windows.Forms;
using Modelos.Datos;
using Modelos.Entidades;
using Modelos.Seguridad;
using Modelos.Utilidades;
using Vista.Comun;

namespace Vista.Citas
{
    public partial class frmCitas : Form
    {
        private const string Modulo = "Citas";
        private int _id;          // id del registro seleccionado (0 = registro nuevo)
        private bool _cargando;   // evita reaccionar a la selección mientras se llena la tabla

        public frmCitas()
        {
            InitializeComponent();
        }

        private void frmCitas_Load(object sender, EventArgs e)
        {
            Responsive.Aplicar(tlpCampos, ClientSize.Width);
            CargarCombos();

            // Control de permisos: sin permiso de gestión solo se puede consultar
            bool puede = Sesion.Tiene(Permisos.CitasGestionar);
            btnNuevo.Enabled = puede;
            btnGuardar.Enabled = puede;
            btnEliminar.Enabled = puede;
            pnlDatos.Enabled = puede;

            CargarDatos();
            Nuevo();
        }

        private void CargarCombos()
        {
            cboMascota.DataSource = MascotaDatos.ListarParaCombo();
            cboMascota.DisplayMember = "descripcion";
            cboMascota.ValueMember = "idMascota";

            cboVeterinario.DataSource = UsuarioDatos.ListarVeterinarios();
            cboVeterinario.DisplayMember = "nombreCompleto";
            cboVeterinario.ValueMember = "idUsuario";
        }

        private void CargarDatos()
        {
            try
            {
                _cargando = true;
                dgv.DataSource = CitaDatos.Listar(txtBuscar.Text.Trim());
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
                Mensajes.Error(Modulo, ex, "cargar");
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
            if (Mensajes.Invalido(Validaciones.Requerido(txtMotivo.Text, "Motivo"), txtMotivo)) return false;

            bool programada = cboEstado.SelectedItem.ToString() == "Programada";
            if (programada && _id == 0 && FechaSinSegundos() < DateTime.Now)
            {
                Mensajes.Advertencia("La fecha y hora de una cita nueva no puede estar en el pasado.");
                dtpFechaHora.Focus();
                return false;
            }
            if (programada)
            {
                try
                {
                    if (CitaDatos.VeterinarioOcupado(Convert.ToInt32(cboVeterinario.SelectedValue), FechaSinSegundos(), _id))
                    {
                        Mensajes.Advertencia("El veterinario ya tiene una cita programada a esa fecha y hora.");
                        dtpFechaHora.Focus();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Mensajes.Error(Modulo, ex, "guardar");
                    return false;
                }
            }
            return true;
        }

        private void GuardarRegistro(bool esNuevo)
        {
            Cita c = new Cita
            {
                IdCita = _id,
                IdMascota = Convert.ToInt32(cboMascota.SelectedValue),
                IdVeterinario = Convert.ToInt32(cboVeterinario.SelectedValue),
                FechaHora = FechaSinSegundos(),
                Motivo = txtMotivo.Text.Trim(),
                Estado = cboEstado.SelectedItem.ToString(),
                IdUsuarioRegistro = Sesion.UsuarioActual.IdUsuario
            };
            if (esNuevo) CitaDatos.Insertar(c); else CitaDatos.Actualizar(c);
        }

        private void EliminarRegistro(int id)
        {
            CitaDatos.Eliminar(id);
        }

        private DateTime FechaSinSegundos()
        {
            DateTime v = dtpFechaHora.Value;
            return new DateTime(v.Year, v.Month, v.Day, v.Hour, v.Minute, 0);
        }

        // ---------------- Eventos de la pantalla ----------------

        // Diseño adaptable: una o dos columnas de campos según el ancho disponible
        private void frmCitas_Resize(object sender, EventArgs e)
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
