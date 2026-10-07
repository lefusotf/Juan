using System;
using System.Data;
using System.Windows.Forms;
using dashboardVet.Base;
using dashboardVet.Controles;
using dashboardVet.Helpers;
using Modelos;

namespace dashboardVet.CatalogoVacunas
{
    public partial class CatalogoVacunas : FrmBase
    {
        private readonly VacunaDAL _vacunaDAL = new VacunaDAL();
        private const string Modulo = "Vacunas";
        private int _id;
        private bool _cargando;

        public CatalogoVacunas()
        {
            InitializeComponent();
        }

        private void CatalogoVacunas_Load(object sender, EventArgs e)
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
            Entrada.LetrasYNumeros(txtNombre);
            Entrada.SoloLetras(cboEspecie);
        }

        private void CargarCombos()
        {
        }

        private void CargarDatos()
        {
            try
            {
                _cargando = true;
                dgv.DataSource = _vacunaDAL.Listar(txtBuscar.Text.Trim());
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
                ManejadorUIErrores.MostrarError(ex, Modulo);
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
            if (Mensajes.Invalido(Validaciones.LetrasYNumeros(txtNombre.Text, "Nombre"), txtNombre)) return false;
            if (Mensajes.Invalido(Validaciones.Nombre(cboEspecie.Text, "Especie destino"), cboEspecie)) return false;
            if (Mensajes.Invalido(Validaciones.LongitudMaxima(txtDescripcion.Text, 250, "Descripción"), txtDescripcion)) return false;

            try
            {
                if (_vacunaDAL.ExisteNombre(Texto.Limpiar(txtNombre.Text), _id))
                {
                    Mensajes.Invalido("Ya existe una vacuna con ese nombre en el catálogo.", txtNombre);
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
            string nombre = Texto.Limpiar(txtNombre.Text);
            string especie = Texto.Limpiar(cboEspecie.Text);
            int intervalo = (int)nudIntervalo.Value;
            string descripcion = Texto.PrimeraMayuscula(txtDescripcion.Text);
            if (esNuevo) _vacunaDAL.Insertar(nombre, descripcion, especie, intervalo);
            else _vacunaDAL.Actualizar(_id, nombre, descripcion, especie, intervalo);
        }

        private void EliminarRegistro(int id)
        {
            _vacunaDAL.Eliminar(id);
        }

        private void CatalogoVacunas_Resize(object sender, EventArgs e)
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
