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
    public partial class frmCatalogoVacunas : Form
    {
        private const string Modulo = "Vacunas";
        private int _id;          // id del registro seleccionado (0 = registro nuevo)
        private bool _cargando;   // evita reaccionar a la selección mientras se llena la tabla

        public frmCatalogoVacunas()
        {
            InitializeComponent();
        }

        private void frmCatalogoVacunas_Load(object sender, EventArgs e)
        {
            CargarCombos();

            // Control de permisos: sin permiso de gestión solo se puede consultar
            bool puede = Sesion.Tiene(Permisos.VacunasGestionar);
            btnNuevo.Enabled = puede;
            btnGuardar.Enabled = puede;
            btnEliminar.Enabled = puede;
            gbDatos.Enabled = puede;

            CargarDatos();
            Nuevo();
        }

        private void CargarCombos()
        {
            // (sin acciones)
        }

        private void CargarDatos()
        {
            try
            {
                _cargando = true;
                dgv.DataSource = VacunaDatos.Listar(txtBuscar.Text.Trim());
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
            GridUtil.Ocultar(dgv, "idVacuna");
            GridUtil.Encabezado(dgv, "nombre", "Vacuna");
            GridUtil.Encabezado(dgv, "descripcion", "Descripción");
            GridUtil.Encabezado(dgv, "especieDestino", "Especie");
            GridUtil.Encabezado(dgv, "intervaloDias", "Intervalo (días)");
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (_cargando || dgv.CurrentRow == null || !dgv.CurrentRow.Selected) return;
            try
            {
                _id = Convert.ToInt32(dgv.CurrentRow.Cells["idVacuna"].Value);
                MostrarFila(dgv.CurrentRow);
            }
            catch (Exception ex)
            {
                Mensajes.Error(Modulo, ex, "cargar");
            }
        }

        private void MostrarFila(DataGridViewRow f)
        {
            txtNombre.Text = f.Cells["nombre"].Value.ToString();
            cboEspecie.Text = f.Cells["especieDestino"].Value.ToString();
            nudIntervalo.Value = Convert.ToDecimal(f.Cells["intervaloDias"].Value);
            txtDescripcion.Text = f.Cells["descripcion"].Value.ToString();
        }

        private void Nuevo()
        {
            _id = 0;
            LimpiarCampos();
            dgv.ClearSelection();
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            cboEspecie.Text = "";
            nudIntervalo.Value = 365;
            txtDescripcion.Clear();
            txtNombre.Focus();
        }

        private bool Validar()
        {
            if (Mensajes.Invalido(Validaciones.Requerido(txtNombre.Text, "Nombre"), txtNombre)) return false;
            if (Mensajes.Invalido(Validaciones.Requerido(cboEspecie.Text, "Especie destino"), cboEspecie)) return false;
            return true;
        }

        private void GuardarRegistro(bool esNuevo)
        {
            Vacuna v = new Vacuna
            {
                IdVacuna = _id,
                Nombre = txtNombre.Text.Trim(),
                EspecieDestino = cboEspecie.Text.Trim(),
                IntervaloDias = (int)nudIntervalo.Value,
                Descripcion = txtDescripcion.Text.Trim()
            };
            if (esNuevo) VacunaDatos.Insertar(v); else VacunaDatos.Actualizar(v);
        }

        private void EliminarRegistro(int id)
        {
            VacunaDatos.Eliminar(id);
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
