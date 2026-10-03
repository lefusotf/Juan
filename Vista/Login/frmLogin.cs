using System;
using System.Drawing;
using System.Windows.Forms;
using Modelos.Datos;
using Modelos.Entidades;
using Modelos.Seguridad;
using Modelos.Utilidades;
using Vista.Comun;

namespace Vista.Login
{
    /// <summary>Pantalla de inicio de sesión: valida los datos, verifica la contraseña con BCrypt y abre la sesión.</summary>
    public class frmLogin : Form
    {
        private const int MaximoIntentos = 3;

        private readonly TextBox txtUsuario = new TextBox();
        private readonly TextBox txtContrasena = new TextBox();
        private readonly CheckBox chkMostrar = new CheckBox();
        private readonly Button btnIngresar = new Button();
        private readonly Button btnSalir = new Button();
        private int _intentos;

        public frmLogin()
        {
            Text = "Veterinaria - Iniciar sesión";
            Font = Estilo.Fuente;
            BackColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(380, 360);

            Panel banda = new Panel { Dock = DockStyle.Top, Height = 100, BackColor = Estilo.Primario };
            Label titulo = new Label
            {
                Text = "Clínica Veterinaria",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            banda.Controls.Add(titulo);

            Label lblUsuario = new Label { Text = "Usuario", Location = new Point(40, 125), AutoSize = true };
            txtUsuario.Location = new Point(40, 148);
            txtUsuario.Width = 300;
            txtUsuario.MaxLength = 50;

            Label lblClave = new Label { Text = "Contraseña", Location = new Point(40, 190), AutoSize = true };
            txtContrasena.Location = new Point(40, 213);
            txtContrasena.Width = 300;
            txtContrasena.MaxLength = 100;
            txtContrasena.UseSystemPasswordChar = true;

            chkMostrar.Text = "Mostrar contraseña";
            chkMostrar.AutoSize = true;
            chkMostrar.Location = new Point(40, 245);
            chkMostrar.CheckedChanged += (s, e) => txtContrasena.UseSystemPasswordChar = !chkMostrar.Checked;

            btnIngresar.Text = "Ingresar";
            Estilo.Boton(btnIngresar, Estilo.Primario);
            btnIngresar.Location = new Point(40, 290);
            btnIngresar.Width = 145;
            btnIngresar.Click += (s, e) => Ingresar();

            btnSalir.Text = "Salir";
            Estilo.Boton(btnSalir, Estilo.Neutro);
            btnSalir.Location = new Point(195, 290);
            btnSalir.Width = 145;
            btnSalir.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.AddRange(new Control[] { banda, lblUsuario, txtUsuario, lblClave, txtContrasena, chkMostrar, btnIngresar, btnSalir });
            AcceptButton = btnIngresar;
            CancelButton = btnSalir;
        }

        private void Ingresar()
        {
            string error = Validaciones.Requerido(txtUsuario.Text, "Usuario");
            if (error != null) { Mensajes.Advertencia(error); txtUsuario.Focus(); return; }
            error = Validaciones.Requerido(txtContrasena.Text, "Contraseña");
            if (error != null) { Mensajes.Advertencia(error); txtContrasena.Focus(); return; }

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
