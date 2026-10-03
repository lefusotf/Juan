using System;
using System.Windows.Forms;
using Modelos.Datos;
using Modelos.Entidades;
using Modelos.Seguridad;
using Modelos.Utilidades;
using Vista.Comun;

namespace Vista.Login
{
    /// <summary>Pantalla de inicio de sesión: valida los datos, verifica la contraseña con BCrypt y abre la sesión.</summary>
    public partial class frmLogin : Form
    {
        private const int MaximoIntentos = 3;
        private int _intentos;

        public frmLogin()
        {
            InitializeComponent();
        }

        // Diseño adaptable: en ventanas angostas se oculta el panel de la marca y el formulario ocupa todo el ancho
        private void frmLogin_Resize(object sender, EventArgs e)
        {
            bool angosto = ClientSize.Width < 820;
            pnlMarca.Visible = !angosto;
            tlpRaiz.ColumnStyles[0].Width = angosto ? 0 : 45;
            tlpRaiz.ColumnStyles[1].Width = angosto ? 100 : 55;
        }

        private void chkMostrar_CheckedChanged(object sender, EventArgs e)
        {
            txtContrasena.UseSystemPasswordChar = !chkMostrar.Checked;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (Mensajes.Invalido(Validaciones.Requerido(txtUsuario.Text, "Usuario"), txtUsuario)) return;
            if (Mensajes.Invalido(Validaciones.Requerido(txtContrasena.Text, "Contraseña"), txtContrasena)) return;

            try
            {
                Usuario usuario = UsuarioDatos.Autenticar(txtUsuario.Text.Trim(), txtContrasena.Text);
                if (usuario == null)
                {
                    _intentos++;
                    Logger.Info("Login", "Intento fallido para el usuario '" + txtUsuario.Text.Trim() + "'");
                    txtContrasena.Clear();

                    if (_intentos >= MaximoIntentos)
                    {
                        Mensajes.Advertencia("Superó el máximo de intentos permitidos. El sistema se cerrará.");
                        DialogResult = DialogResult.Cancel;
                        Close();
                        return;
                    }
                    Mensajes.Advertencia("Usuario o contraseña incorrectos, o el usuario está inactivo.\nIntentos restantes: " +
                                         (MaximoIntentos - _intentos));
                    txtContrasena.Focus();
                    return;
                }

                Sesion.Iniciar(usuario);
                Logger.Info("Login", "Inicio de sesión correcto (rol " + usuario.Rol + ")");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Mensajes.Error("Login", ex, "iniciar sesión");
            }
        }
    }
}
