using System;
using System.Data;
using System.Windows.Forms;
using Modelos.Datos;
using Modelos.Entidades;
using Modelos.Seguridad;
using Modelos.Utilidades;
using Vista.Comun;

namespace Vista.Propietarios
{
    public partial class frmPropietarios : Form
    {
        private const string Modulo = "Propietarios";
        private int _id;          // id del registro seleccionado (0 = registro nuevo)
        private bool _cargando;   // evita reaccionar a la selección mientras se llena la tabla

        public frmPropietarios()
        {
            InitializeComponent();
        }

        private void frmPropietarios_Load(object sender, EventArgs e)
        {
            Responsive.Aplicar(tlpCampos, ClientSize.Width);
            ConfigurarEntradas();
            CargarCombos();

            // Control de permisos: sin permiso de gestión solo se puede consultar
            bool puede = Sesion.Tiene(Permisos.PropietariosGestionar);
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
            txtBuscar.MaxLength = 60;
            Entrada.SoloLetras(txtNombre);
            Entrada.Dui(txtDui);
            Entrada.Telefono(txtTelefono);
            Entrada.SinEspacios(txtCorreo);
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
                dgv.DataSource = PropietarioDatos.Listar(txtBuscar.Text.Trim());
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
                Mensajes.Error(Modulo, ex, "cargar");
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

            // No se permiten dos propietarios con el mismo DUI
            try
            {
                if (PropietarioDatos.ExisteDui(txtDui.Text.Trim(), _id))
                {
                    Mensajes.Invalido("Ya existe un propietario registrado con ese DUI.", txtDui);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Mensajes.Error(Modulo, ex, "guardar");
                return false;
            }

            // El dígito verificador solo genera una advertencia: el usuario decide si continúa
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
            Propietario p = new Propietario
            {
                IdPropietario = _id,
                Nombre = Texto.Capitalizar(txtNombre.Text),
                Dui = Texto.Limpiar(txtDui.Text),
                Telefono = Texto.Limpiar(txtTelefono.Text),
                Correo = Texto.Limpiar(txtCorreo.Text).ToLower(),
                Direccion = Texto.Limpiar(txtDireccion.Text)
            };
            if (esNuevo) PropietarioDatos.Insertar(p); else PropietarioDatos.Actualizar(p);
        }

        private void EliminarRegistro(int id)
        {
            PropietarioDatos.Eliminar(id);
        }

        // ---------------- Eventos de la pantalla ----------------

        // Diseño adaptable: una o dos columnas de campos según el ancho disponible
        private void frmPropietarios_Resize(object sender, EventArgs e)
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
