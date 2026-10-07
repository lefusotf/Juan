using System;
using System.Data;
using System.Windows.Forms;
using dashboardVet.Base;
using dashboardVet.Controles;
using dashboardVet.Helpers;
using Modelos;

namespace dashboardVet.Consultas
{
    public partial class Consultas : FrmBase
    {
        private readonly MascotaDAL _mascotaDAL = new MascotaDAL();
        private readonly ConsultaDAL _consultaDAL = new ConsultaDAL();
        private readonly UsuarioDAL _usuarioDAL = new UsuarioDAL();
        private const string Modulo = "Consultas";
        private int _id;
        private bool _cargando;

        public Consultas()
        {
            InitializeComponent();
        }

        private void Consultas_Load(object sender, EventArgs e)
        {
            Responsive.Aplicar(tlpCampos, ClientSize.Width);
            ConfigurarEntradas();
            CargarCombos();

            bool puede = Sesion.Tiene(Permisos.ConsultasGestionar);
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
                dgv.DataSource = _consultaDAL.Listar(txtBuscar.Text.Trim());
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
                ManejadorUIErrores.MostrarError(ex, Modulo);
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
                Mensajes.Invalido("La fecha de la consulta no puede ser futura.", dtpFecha);
                return false;
            }
            if (Mensajes.Invalido(Validaciones.Minimo(txtMotivo.Text, 3, "Motivo"), txtMotivo)) return false;
            if (Mensajes.Invalido(Validaciones.LongitudMaxima(txtMotivo.Text, 250, "Motivo"), txtMotivo)) return false;
            if (Mensajes.Invalido(Validaciones.Minimo(txtDiagnostico.Text, 5, "Diagnóstico"), txtDiagnostico)) return false;
            if (Mensajes.Invalido(Validaciones.LongitudMaxima(txtDiagnostico.Text, 500, "Diagnóstico"), txtDiagnostico)) return false;
            if (!string.IsNullOrWhiteSpace(txtTratamiento.Text) &&
                Mensajes.Invalido(Validaciones.Minimo(txtTratamiento.Text, 3, "Tratamiento"), txtTratamiento)) return false;
            if (Mensajes.Invalido(Validaciones.LongitudMaxima(txtTratamiento.Text, 500, "Tratamiento"), txtTratamiento)) return false;
            if (Mensajes.Invalido(Validaciones.LongitudMaxima(txtObservaciones.Text, 500, "Observaciones"), txtObservaciones)) return false;

            try
            {
                DataRow mascota = _mascotaDAL.ObtenerBasico(Convert.ToInt32(cboMascota.SelectedValue));
                if (mascota != null && mascota["fechaNacimiento"] is DateTime &&
                    dtpFecha.Value.Date < (DateTime)mascota["fechaNacimiento"])
                {
                    Mensajes.Invalido("La fecha de la consulta no puede ser anterior al nacimiento de la mascota.", dtpFecha);
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
            DateTime v = dtpFecha.Value;
            int idMascota = Convert.ToInt32(cboMascota.SelectedValue);
            int idVeterinario = Convert.ToInt32(cboVeterinario.SelectedValue);
            DateTime fecha = new DateTime(v.Year, v.Month, v.Day, v.Hour, v.Minute, 0);
            string motivo = Texto.PrimeraMayuscula(txtMotivo.Text);
            string diagnostico = Texto.PrimeraMayuscula(txtDiagnostico.Text);
            string tratamiento = Texto.PrimeraMayuscula(txtTratamiento.Text);
            string observaciones = Texto.PrimeraMayuscula(txtObservaciones.Text);
            if (esNuevo) _consultaDAL.Insertar(idMascota, idVeterinario, fecha, motivo, diagnostico, tratamiento, observaciones);
            else _consultaDAL.Actualizar(_id, idMascota, idVeterinario, fecha, motivo, diagnostico, tratamiento, observaciones);
        }

        private void EliminarRegistro(int id)
        {
            _consultaDAL.Eliminar(id);
        }

        private void Consultas_Resize(object sender, EventArgs e)
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
