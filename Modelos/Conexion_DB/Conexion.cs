using System;
using System.Data;
using System.Data.SqlClient;

namespace Modelos.Conexion_DB
{
    /// <summary>
    /// Acceso centralizado a SQL Server. Todas las consultas son parametrizadas
    /// (evita inyección SQL) y cada conexión se abre y se libera dentro de un using.
    /// Las excepciones se propagan hacia la Vista, que decide cómo mostrarlas con MessageBox.
    /// </summary>
    public static class Conexion
    {
        // ===== CAMBIE AQUÍ EL NOMBRE DE SU SERVIDOR =====
        // Ejemplos: "(localdb)\\MSSQLLocalDB", ".\\SQLEXPRESS", "DESKTOP-POFECHT\\SQLEXPRESS"
        private static string servidor = "DESKTOP-POFECHT\\SQLEXPRESS";
        private static string baseDeDatos = "Veterinaria";

        private static string ObtenerCadena()
        {
            return $"Data Source={servidor};Initial Catalog={baseDeDatos};Integrated Security=true;";
        }

        public static SqlConnection Conectar()
        {
            SqlConnection conexion = new SqlConnection(ObtenerCadena());
            try
            {
                conexion.Open();
                return conexion;
            }
            catch
            {
                // Liberar memoria si no se pudo abrir y dejar que la Vista maneje el error
                conexion.Dispose();
                throw;
            }
        }

        /// <summary>Crea un parámetro convirtiendo null / cadenas vacías en DBNull.</summary>
        public static SqlParameter P(string nombre, object valor)
        {
            if (valor == null || (valor is string s && string.IsNullOrWhiteSpace(s)))
                return new SqlParameter(nombre, DBNull.Value);
            return new SqlParameter(nombre, valor);
        }

        public static DataTable Consultar(string sql, params SqlParameter[] parametros)
        {
            using (SqlConnection cn = Conectar())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                if (parametros != null) cmd.Parameters.AddRange(parametros);
                DataTable tabla = new DataTable();
                da.Fill(tabla);
                return tabla;
            }
        }

        public static int EjecutarNoQuery(string sql, params SqlParameter[] parametros)
        {
            using (SqlConnection cn = Conectar())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                if (parametros != null) cmd.Parameters.AddRange(parametros);
                return cmd.ExecuteNonQuery();
            }
        }

        public static object Escalar(string sql, params SqlParameter[] parametros)
        {
            using (SqlConnection cn = Conectar())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                if (parametros != null) cmd.Parameters.AddRange(parametros);
                return cmd.ExecuteScalar();
            }
        }
    }
}
