using System;
using System.Data;
using System.Windows.Forms;
using Modelos.Datos;
using Modelos.Entidades;
using Modelos.Seguridad;
using Modelos.Utilidades;
using Vista.Comun;

namespace Vista.Usuarios
{
    public partial class frmUsuarios : Form
    {
        private const string Modulo = "Usuarios";
        private int _id;          // id del registro seleccionado (0 = registro nuevo)
        private bool _cargando;   // evita reaccionar a la selección mientras se llena la tabla

        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            Responsive.Aplicar(tlpCampos, ClientSize.Width);
            ConfigurarEntradas();
            CargarCombos();

            // Control de permisos: sin permiso de gestión solo se puede consultar
            bool puede = Sesion.Tiene(Permisos.UsuariosGestionar);
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
            Entrada.Usuario(txtUsuario);
            Entrada.SinEspacios(txtContrasena);
            Entrada.SinEspacios(txtCorreo);
        }

        private void CargarCombos()
        {
            cboRol.DataSource = RolDatos.Listar();
            cboRol.DisplayMember = "nombre";
            cboRol.ValueMember = "idRol";
        }

        private void CargarDatos()
        {
            try
            {
                _cargando = true;
                dgv.DataSource = UsuarioDatos.Listar(txtBuscar.Text.Trim());
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
            GridUtil.Ocultar(dgv, "idUsuario", "idRol");
            GridUtil.Encabezado(dgv, "nombreCompleto", "Nombre");
            GridUtil.Encabezado(dgv, "nombreUsuario", "Usuario");
            GridUtil.Encabezado(dgv, "correo", "Correo");
            GridUtil.Encabezado(dgv, "rol", "Rol");
            GridUtil.Encabezado(dgv, "estado", "Estado");
            GridUtil.Encabezado(dgv, "fechaCreacion", "Creado");
            GridUtil.Formato(dgv, "fechaCreacion", "dd/MM/yyyy");
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (_cargando || dgv.CurrentRow == null || !dgv.CurrentRow.Selected) return;
            try
            {
                _id = Convert.ToInt32(dgv.CurrentRow.Cells["idUsuario"].Value);
                MostrarFila(dgv.CurrentRow);
            }
            catch (Exception ex)
            {
                Mensajes.Error(Modulo, ex, "cargar");
            }
        }

        private void MostrarFila(DataGridViewRow f)
        {
            txtNombre.Text = f.Cells["nombreCompleto"].Value.ToString();
            txtUsuario.Text = f.Cells["nombreUsuario"].Value.ToString();
            txtContrasena.Clear(); // nunca se muestra la contraseña; vacío = conservar la actual
            txtCorreo.Text = f.Cells["correo"].Value.ToString();
            cboRol.SelectedValue = Convert.ToInt32(f.Cells["idRol"].Value);
            cboEstado.SelectedItem = f.Cells["estado"].Value.ToString();
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
            txtUsuario.Clear();
            txtContrasena.Clear();
            txtCorreo.Clear();
            if (cboRol.Items.Count > 0) cboRol.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;
            txtNombre.Focus();
        }

        private bool Validar()
        {
            if (Mensajes.Invalido(Validaciones.NombreCompleto(txtNombre.Text, "Nombre completo"), txtNombre)) return false;
            if (Mensajes.Invalido(Validaciones.NombreUsuario(txtUsuario.Text), txtUsuario)) return false;
            if (Mensajes.Invalido(Validaciones.Correo(txtCorreo.Text), txtCorreo)) return false;

            if (cboRol.SelectedValue == null)
            {
                Mensajes.Advertencia("Seleccione un rol para el usuario.");
                cboRol.Focus();
                return false;
            }

            bool esNuevo = _id == 0;
            if (esNuevo || txtContrasena.Text.Length > 0)
                if (Mensajes.Invalido(Validaciones.Contrasena(txtContrasena.Text, txtUsuario.Text), txtContrasena)) return false;

            // Evitar que el administrador se bloquee a sí mismo
            if (!esNuevo && _id == Sesion.UsuarioActual.IdUsuario)
            {
                if (cboEstado.SelectedItem.ToString() != "Activo")
                {
                    Mensajes.Advertencia("No puede desactivar su propio usuario.");
                    return false;
                }
                if (Convert.ToInt32(cboRol.SelectedValue) != Sesion.UsuarioActual.IdRol)
                {
                    Mensajes.Advertencia("No puede cambiar su propio rol.");
                    return false;
                }
            }

            // Usuario y correo no pueden repetirse
            try
            {
                if (UsuarioDatos.ExisteNombreUsuario(txtUsuario.Text.Trim(), _id))
                {
                    Mensajes.Invalido("El nombre de usuario ya está en uso. Elija otro.", txtUsuario);
                    return false;
                }
                if (!string.IsNullOrWhiteSpace(txtCorreo.Text) && UsuarioDatos.ExisteCorreo(txtCorreo.Text.Trim(), _id))
                {
                    Mensajes.Invalido("Ya existe otro usuario con ese correo electrónico.", txtCorreo);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Mensajes.Error(Modulo, ex, "guardar");
                return false;
            }
            return true;
        }

        private void GuardarRegistro(bool esNuevo)
        {
            Usuario u = new Usuario
            {
                IdUsuario = _id,
                NombreCompleto = Texto.Capitalizar(txtNombre.Text),
                NombreUsuario = Texto.Limpiar(txtUsuario.Text),
                Contrasena = txtContrasena.Text,
                Correo = Texto.Limpiar(txtCorreo.Text).ToLower(),
                IdRol = Convert.ToInt32(cboRol.SelectedValue),
                Estado = cboEstado.SelectedItem.ToString()
            };
            if (esNuevo) UsuarioDatos.Insertar(u); else UsuarioDatos.Actualizar(u);
        }

        private void EliminarRegistro(int id)
        {
            if (id == Sesion.UsuarioActual.IdUsuario)
                throw new InvalidOperationException("No puede eliminar el usuario con el que inició sesión.");
            UsuarioDatos.Eliminar(id);
        }

        // ---------------- Eventos de la pantalla ----------------

        // Diseño adaptable: una o dos columnas de campos según el ancho disponible
        private void frmUsuarios_Resize(object sender, EventArgs e)
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
