using System;
using System.Windows.Forms;
using Modelos.Utilidades;
using Vista.Dashboard;
using Vista.Login;

namespace Vista
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Última red de seguridad: cualquier excepción no controlada se registra y se informa al usuario
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) => ManejarError(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => ManejarError(e.ExceptionObject as Exception);

            // Bucle de sesiones: al cerrar sesión se vuelve al login
            while (true)
            {
                using (frmLogin login = new frmLogin())
                {
                    if (login.ShowDialog() != DialogResult.OK) break;
                }

                using (frmDashboardPrincipal dashboard = new frmDashboardPrincipal())
                {
                    Application.Run(dashboard);
                    if (!dashboard.CerrarSesion) break;
                }
            }
        }

        private static void ManejarError(Exception ex)
        {
            if (ex == null) return;
            Logger.Error("Aplicación", ex);
            MessageBox.Show("Ocurrió un error inesperado y fue registrado en la bitácora:\n" + ex.Message,
                "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
