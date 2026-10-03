using System;
using System.Data;
using System.Windows.Forms;
using Modelos.Datos;
using Modelos.Entidades;
using Modelos.Seguridad;
using Vista.Comun;

namespace Vista.Usuarios
{
    /// <summary>CRUD de usuarios (personal) con asignación de rol. Las contraseñas se guardan con BCrypt.</summary>
    public class frmUsuarios : FormCrudBase
    {
        private readonly TextBox txtNombre = new TextBox { MaxLength = 150 };
        private readonly TextBox txtUsuario = new TextBox { MaxLength = 30 };
        private readonly TextBox txtContrasena = new TextBox { MaxLength = 100, UseSystemPasswordChar = true };
        private readonly TextBox txtCorreo = new TextBox { MaxLength = 150 };
        private readonly ComboBox cboRol = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly ComboBox cboEstado = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };

        protected override string TituloFormulario { get { return "Gestión de usuarios"; } }
        protected override string Modulo { get { return "Usuarios"; } }
        protected override string ColumnaId { get { return "idUsuario"; } }
        protected override bool PuedeGestionar { get { return Sesion.Tiene(Permisos.UsuariosGestionar); } }

        protected override void ConstruirCampos()
        {
            cboEstado.Items.AddRange(new object[] { "Activo", "Inactivo" });

            AgregarCampo("Nombre completo *", txtNombre);
            AgregarCampo("Usuario *", txtUsuario);
            AgregarCampo("Contraseña *", txtContrasena);
            AgregarCampo("Correo", txtCorreo);
            AgregarCampo("Rol *", cboRol);
            AgregarCampo("Estado", cboEstado);

            try
            {
                cboRol.DataSource = RolDatos.Listar();
                cboRol.DisplayMember = "nombre";
                cboRol.ValueMember = "idRol";
            }
            catch (Exception ex)
            {
                Mensajes.Error(Modulo, ex, "cargar");
            }
        }

        protected override DataTable ObtenerDatos(string filtro)
        {
            return UsuarioDatos.Listar(filtro);
        }

        protected override void ConfigurarColumnas()
        {
            OcultarColumnas("idUsuario", "idRol");
            Encabezado("nombreCompleto", "Nombre");
            Encabezado("nombreUsuario", "Usuario");
            Encabezado("correo", "Correo");
            Encabezado("rol", "Rol");
            Encabezado("estado", "Estado");
            Encabezado("fechaCreacion", "Creado");
            if (dgv.Columns.Contains("fechaCreacion")) dgv.Columns["fechaCreacion"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        protected override void MostrarFila(DataGridViewRow f)
        {
            txtNombre.Text = f.Cells["nombreCompleto"].Value.ToString();
            txtUsuario.Text = f.Cells["nombreUsuario"].Value.ToString();
            txtContrasena.Clear(); // nunca se muestra la contraseña; vacío = conservar la actual
            txtCorreo.Text = f.Cells["correo"].Value.ToString();
            cboRol.SelectedValue = Convert.ToInt32(f.Cells["idRol"].Value);
            cboEstado.SelectedItem = f.Cells["estado"].Value.ToString();
        }

        protected override void LimpiarCampos()
        {
            txtNombre.Clear(); txtUsuario.Clear(); txtContrasena.Clear(); txtCorreo.Clear();
            if (cboRol.Items.Count > 0) cboRol.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;
            txtNombre.Focus();
        }

        protected override bool Validar()
        {
            if (Falla(Validaciones.Requerido(txtNombre.Text, "Nombre completo"), txtNombre)) return false;
            if (Falla(Validaciones.NombreUsuario(txtUsuario.Text), txtUsuario)) return false;
            if (Falla(Validaciones.Correo(txtCorreo.Text), txtCorreo)) return false;

            if (cboRol.SelectedValue == null)
            {
                Mensajes.Advertencia("Seleccione un rol para el usuario.");
                cboRol.Focus();
                return false;
            }

            bool esNuevo = IdSeleccionado == 0;
            if (esNuevo || txtContrasena.Text.Length > 0)
                if (Falla(Validaciones.Contrasena(txtContrasena.Text), txtContrasena)) return false;

            // Evitar que el administrador se bloquee a sí mismo
            if (!esNuevo && IdSeleccionado == Sesion.UsuarioActual.IdUsuario)
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
            return true;
        }

        protected override void GuardarRegistro(bool esNuevo)
        {
            Usuario u = new Usuario
            {
                IdUsuario = IdSeleccionado,
                NombreCompleto = txtNombre.Text.Trim(),
                NombreUsuario = txtUsuario.Text.Trim(),
                Contrasena = txtContrasena.Text,
                Correo = txtCorreo.Text.Trim(),
                IdRol = Convert.ToInt32(cboRol.SelectedValue),
                Estado = cboEstado.SelectedItem.ToString()
            };
            if (esNuevo) UsuarioDatos.Insertar(u); else UsuarioDatos.Actualizar(u);
        }

        protected override void EliminarRegistro(int id)
        {
            if (id == Sesion.UsuarioActual.IdUsuario)
                throw new InvalidOperationException("No puede eliminar el usuario con el que inició sesión.");
            UsuarioDatos.Eliminar(id);
        }
    }
}
