using System;
using System.Data;
using System.Windows.Forms;
using Modelos.Datos;
using Modelos.Entidades;
using Modelos.Seguridad;
using Modelos.Utilidades;
using Vista.Comun;

namespace Vista.Consultas
{
    public partial class frmConsultas : Form
    {
        private const string Modulo = "Consultas";
        private int _id;          // id del registro seleccionado (0 = registro nuevo)
        private bool _cargando;   // evita reaccionar a la selección mientras se llena la tabla

        public frmConsultas()
        {
            InitializeComponent();
        }

        private void frmConsultas_Load(object sender, EventArgs e)
        {
            CargarCombos();

            // Control de permisos: sin permiso de gestión solo se puede consultar
            bool puede = Sesion.Tiene(Permisos.ConsultasGestionar);
            btnNuevo.Enabled = puede;
            btnGuardar.Enabled = puede;
            btnEliminar.Enabled = puede;
            gbDatos.Enabled = puede;

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
                dgv.DataSource = ConsultaDatos.Listar(txtBuscar.Text.Trim());
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
            GridUtil.Ocultar(dgv, "idConsulta", "idMascota", "idVeterinario", "observaciones");
            GridUtil.Encabezado(dgv, "mascota", "Mascota");
            GridUtil.Encabezado(dgv, "propietario", "Propietario");
            GridUtil.Encabezado(dgv, "veterinario", "Veterinario");
            GridUtil.Encabezado(dgv, "fecha", "Fecha");
            GridUtil.Encabezado(dgv, "motivo", "Motivo");
            GridUtil.Encabezado(dgv, "diagnostico", "Diagnóstico");
            GridUtil.Encabezado(dgv, "tratamiento", "Tratamiento");
            GridUtil.Formato(dgv, "fecha", "dd/MM/yyyy HH:mm");
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (_cargando || dgv.CurrentRow == null || !dgv.CurrentRow.Selected) return;
            try
            {
                _id = Convert.ToInt32(dgv.CurrentRow.Cells["idConsulta"].Value);
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
            dtpFecha.Value = (DateTime)f.Cells["fecha"].Value;
            txtMotivo.Text = f.Cells["motivo"].Value.ToString();
            txtDiagnostico.Text = f.Cells["diagnostico"].Value.ToString();
            txtTratamiento.Text = f.Cells["tratamiento"].Value.ToString();
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
            // Si quien registra es veterinario, se preselecciona a sí mismo
            if (Sesion.UsuarioActual.Rol == "Veterinario")
                cboVeterinario.SelectedValue = Sesion.UsuarioActual.IdUsuario;
            else if (cboVeterinario.Items.Count > 0)
                cboVeterinario.SelectedIndex = 0;
            dtpFecha.Value = DateTime.Now;
            txtMotivo.Clear();
            txtDiagnostico.Clear();
            txtTratamiento.Clear();
            txtObservaciones.Clear();
            cboMascota.Focus();
        }

        private bool Validar()
        {
            if (cboMascota.SelectedValue == null)
            {
                Mensajes.Advertencia("Seleccione una mascota.");
                cboMascota.Focus();
                return false;
            }
            if (cboVeterinario.SelectedValue == null)
            {
                Mensajes.Advertencia("Seleccione el veterinario que atendió la consulta.");
                cboVeterinario.Focus();
                return false;
            }
            if (dtpFecha.Value > DateTime.Now.AddMinutes(5))
            {
                Mensajes.Advertencia("La fecha de la consulta no puede ser futura.");
                dtpFecha.Focus();
                return false;
            }
            if (Mensajes.Invalido(Validaciones.Requerido(txtMotivo.Text, "Motivo"), txtMotivo)) return false;
            if (Mensajes.Invalido(Validaciones.Requerido(txtDiagnostico.Text, "Diagnóstico"), txtDiagnostico)) return false;
            return true;
        }

        private void GuardarRegistro(bool esNuevo)
        {
            DateTime v = dtpFecha.Value;
            Consulta c = new Consulta
            {
                IdConsulta = _id,
                IdMascota = Convert.ToInt32(cboMascota.SelectedValue),
                IdVeterinario = Convert.ToInt32(cboVeterinario.SelectedValue),
                Fecha = new DateTime(v.Year, v.Month, v.Day, v.Hour, v.Minute, 0),
                Motivo = txtMotivo.Text.Trim(),
                Diagnostico = txtDiagnostico.Text.Trim(),
                Tratamiento = txtTratamiento.Text.Trim(),
                Observaciones = txtObservaciones.Text.Trim()
            };
            if (esNuevo) ConsultaDatos.Insertar(c); else ConsultaDatos.Actualizar(c);
        }

        private void EliminarRegistro(int id)
        {
            ConsultaDatos.Eliminar(id);
        }

        // ---------------- Eventos de la pantalla ----------------

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
