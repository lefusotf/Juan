using System;
using System.IO;
using System.Text;
using Modelos.Conexion_DB;
using Modelos.Seguridad;

namespace Modelos.Utilidades
{
    /// <summary>
    /// Logging de actividades y errores: escribe en un archivo diario (carpeta Logs)
    /// y en la tabla bitacora. Nunca lanza excepciones para no ocultar el error original.
    /// </summary>
    public static class Logger
    {
        private static readonly object Candado = new object();

        public static void Info(string modulo, string mensaje)
        {
            Escribir("INFO", modulo, mensaje, null);
        }

        public static void Error(string modulo, Exception ex)
        {
            Escribir("ERROR", modulo, ex.Message, ex.ToString());
        }

        private static void Escribir(string nivel, string modulo, string mensaje, string detalle)
        {
            string usuario = Sesion.HaySesion ? Sesion.UsuarioActual.NombreUsuario : "(sin sesión)";
            EscribirArchivo(nivel, usuario, modulo, mensaje, detalle);
            EscribirBaseDatos(nivel, usuario, modulo, mensaje, detalle);
        }

        private static void EscribirArchivo(string nivel, string usuario, string modulo, string mensaje, string detalle)
        {
            try
            {
                string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                Directory.CreateDirectory(carpeta);
                string archivo = Path.Combine(carpeta, "veterinaria_" + DateTime.Now.ToString("yyyyMMdd") + ".log");

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0:yyyy-MM-dd HH:mm:ss} [{1}] [{2}] [{3}] {4}", DateTime.Now, nivel, usuario, modulo, mensaje);
                if (!string.IsNullOrEmpty(detalle)) sb.AppendLine().Append(detalle);
                sb.AppendLine();

                lock (Candado)
                {
                    File.AppendAllText(archivo, sb.ToString(), Encoding.UTF8);
                }
            }
            catch (Exception)
            {
                // El logging jamás debe romper la aplicación
            }
        }

        private static void EscribirBaseDatos(string nivel, string usuario, string modulo, string mensaje, string detalle)
        {
            try
            {
                if (mensaje != null && mensaje.Length > 500) mensaje = mensaje.Substring(0, 500);
                Conexion.EjecutarNoQuery(
                    "INSERT INTO bitacora (nivel, nombreUsuario, modulo, mensaje, detalle) " +
                    "VALUES (@nivel, @usuario, @modulo, @mensaje, @detalle)",
                    Conexion.P("@nivel", nivel),
                    Conexion.P("@usuario", usuario),
                    Conexion.P("@modulo", modulo),
                    Conexion.P("@mensaje", mensaje),
                    Conexion.P("@detalle", detalle));
            }
            catch (Exception)
            {
                // Si la BD no está disponible, queda el registro en archivo
            }
        }
    }
}
