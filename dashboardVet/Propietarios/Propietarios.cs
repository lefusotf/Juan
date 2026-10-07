using System;
using System.Data;
using System.Windows.Forms;
using dashboardVet.Base;
using dashboardVet.Controles;
using dashboardVet.Helpers;
using Modelos;

namespace dashboardVet.Propietarios
{
    public partial class Propietarios : FrmBase
    {
        private readonly PropietarioDAL _propietarioDAL = new PropietarioDAL();
        private const string Modulo = "Propietarios";
        private int _id;
        private bool _cargando;

        public Propietarios()
        {
            InitializeComponent();
        }

        private void Propietarios_Load(object sender, EventArgs e)
        {
            Responsive.Aplicar(tlpCampos, ClientSize.Width);
            ConfigurarEntradas();
            CargarCombos();

            bool puede = Sesion.Tiene(Permisos.PropietariosGestionar);
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
            Entrada.SoloLetras(txtNombre);
            Entrada.Dui(txtDui);
            Entrada.Telefono(txtTelefono);
            Entrada.SinEspacios(txtCorreo);
        }

        private void CargarCombos()
        {
        }

        private void CargarDatos()
        {
            try
            {
                _cargando = true;
                dgv.DataSource = _propietarioDAL.Listar(txtBuscar.Text.Trim());
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
            GridUtil.Ocultar(dgv, "idPropietario");
            GridUtil.Encabezado(dgv, "nombre", "Nombre");
            GridUtil.Encabezado(dgv, "dui", "DUI");
            GridUtil.Encabezado(dgv, "telefono", "Teléfono");
            GridUtil.Encabezado(dgv, "correo", "Correo");
            GridUtil.Encabezado(dgv, "direccion", "Dirección");
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (_cargando || dgv.CurrentRow == null || !dgv.CurrentRow.Selected) return;
            try
            {
                _id = Convert.ToInt32(dgv.CurrentRow.Cells["idPropietario"].Value);
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
            txtDui.Text = f.Cells["dui"].Value.ToString();
            txtTelefono.Text = f.Cells["telefono"].Value.ToString();
            txtCorreo.Text = f.Cells["correo"].Value.ToString();
            txtDireccion.Text = f.Cells["direccion"].Value.ToString();
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
            txtDui.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            txtNombre.Focus();
        }

        private bool Validar()
        {
            if (Mensajes.Invalido(Validaciones.NombreCompleto(txtNombre.Text, "Nombre completo"), txtNombre)) return false;
            if (Mensajes.Invalido(Validaciones.Requerido(txtDui.Text, "DUI") ?? Validaciones.Dui(txtDui.Text), txtDui)) return false;
            if (Mensajes.Invalido(Validaciones.Requerido(txtTelefono.Text, "Teléfono") ?? Validaciones.Telefono(txtTelefono.Text), txtTelefono)) return false;
            if (Mensajes.Invalido(Validaciones.Correo(txtCorreo.Text), txtCorreo)) return false;
            if (Mensajes.Invalido(Validaciones.LongitudMaxima(txtDireccion.Text, 250, "Dirección"), txtDireccion)) return false;

            try
            {
                if (_propietarioDAL.ExisteDui(txtDui.Text.Trim(), _id))
                {
                    Mensajes.Invalido("Ya existe un propietario registrado con ese DUI.", txtDui);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ManejadorUIErrores.MostrarError(ex, Modulo);
                return false;
            }

            if (!Validaciones.DuiDigitoVerificadorValido(txtDui.Text) &&
                !Mensajes.Confirmar("El DUI no supera la verificación del dígito verificador.\n¿Desea guardarlo de todas formas?"))
            {
                txtDui.Focus();
                return false;
            }
            return true;
        }

        private void GuardarRegistro(bool esNuevo)
        {
            string nombre = Texto.Capitalizar(txtNombre.Text);
            string dui = Texto.Limpiar(txtDui.Text);
            string telefono = Texto.Limpiar(txtTelefono.Text);
            string correo = Texto.Limpiar(txtCorreo.Text).ToLower();
            string direccion = Texto.Limpiar(txtDireccion.Text);
            if (esNuevo) _propietarioDAL.Insertar(nombre, dui, telefono, correo, direccion);
            else _propietarioDAL.Actualizar(_id, nombre, dui, telefono, correo, direccion);
        }

        private void EliminarRegistro(int id)
        {
            _propietarioDAL.Eliminar(id);
        }

        private void Propietarios_Resize(object sender, EventArgs e)
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
