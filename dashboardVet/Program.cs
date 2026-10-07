using System;
using System.Threading;
using System.Windows.Forms;
using Modelos;
using dashboardVet.Helpers;

namespace dashboardVet
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => ManejadorUIErrores.MostrarError(e.ExceptionObject as Exception ?? new Exception());

            while (true)
            {
                using (Login.LoginVet login = new Login.LoginVet())
                {
                    if (login.ShowDialog() != DialogResult.OK) break;
                }

                using (DashBoard.DashBoard dashboard = new DashBoard.DashBoard())
                {
                    Application.Run(dashboard);
                    if (!dashboard.CerrarSesion) break;
                }
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ManejadorUIErrores.MostrarError(e.Exception);
        }
    }
}
