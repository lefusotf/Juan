using System;
using System.Windows.Forms;
using dashboardVet.Base;
using dashboardVet.Controles;
using dashboardVet.Helpers;
using Modelos;

namespace dashboardVet.Login
{
    public partial class LoginVet : FrmBase
    {
        private readonly LoginDAL _loginDAL = new LoginDAL();
        private const int MaximoIntentos = 3;
        private int _intentos;

        public LoginVet()
        {
            InitializeComponent();
        }

        private void LoginVet_Resize(object sender, EventArgs e)
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
                UsuarioSesion usuario = _loginDAL.Autenticar(txtUsuario.Text.Trim(), txtContrasena.Text);
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
                ManejadorUIErrores.MostrarError(ex, "Login");
            }
        }
    }
}
