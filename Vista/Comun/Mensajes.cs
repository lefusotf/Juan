using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Modelos.Utilidades;

namespace Vista.Comun
{
    /// <summary>Centraliza los MessageBox y la traducción de excepciones a mensajes entendibles.</summary>
    public static class Mensajes
    {
        private const string Titulo = "Sistema Veterinaria";

        public static void Info(string mensaje)
        {
            MessageBox.Show(mensaje, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Advertencia(string mensaje)
        {
            MessageBox.Show(mensaje, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>Muestra la advertencia y enfoca el control si hay error de validación. Devuelve true si hubo error.</summary>
        public static bool Invalido(string error, Control foco)
        {
            if (error == null) return false;
            Advertencia(error);
            if (foco != null) foco.Focus();
            return true;
        }

        public static bool Confirmar(string pregunta)
        {
            return MessageBox.Show(pregunta, Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        /// <summary>
        /// Registra la excepción en la bitácora y muestra un MessageBox con un texto claro.
        /// accion: "cargar", "guardar" o "eliminar" (se usa para afinar el mensaje de integridad referencial).
        /// </summary>
        public static void Error(string modulo, Exception ex, string accion)
        {
            Logger.Error(modulo, ex);

            string mensaje;
            SqlException sql = ex as SqlException;

            if (sql != null)
            {
                switch (sql.Number)
                {
                    case 2627:
                    case 2601:
                        mensaje = "Ya existe un registro con esos datos únicos (por ejemplo usuario, DUI o nombre de vacuna).";
                        break;
                    case 547:
                        mensaje = accion == "eliminar"
                            ? "No se puede eliminar el registro porque tiene información relacionada en otras tablas."
                            : "Los datos no cumplen las reglas de integridad de la base de datos. Verifique los valores ingresados.";
                        break;
                    case 2:
                    case 53:
                    case -1:
                    case -2:
                        mensaje = "No se pudo conectar con el servidor de base de datos. Verifique que SQL Server esté iniciado.";
                        break;
                    case 4060:
                        mensaje = "La base de datos 'Veterinaria' no existe o no tiene permisos. Ejecute el script BaseDatos\\Veterinaria.sql.";
                        break;
                    case 18456:
                        mensaje = "No se pudo iniciar sesión en SQL Server con las credenciales configuradas.";
                        break;
                    default:
                        mensaje = "Error de base de datos (código " + sql.Number + "): " + sql.Message;
                        break;
                }
            }
            else if (ex is InvalidOperationException)
            {
                mensaje = ex.Message; // reglas de negocio lanzadas desde el modelo
            }
            else
            {
                mensaje = "Ocurrió un error inesperado: " + ex.Message;
            }

            MessageBox.Show(mensaje, Titulo + " - Error al " + accion, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
