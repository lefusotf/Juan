using System.Data.SqlClient;

namespace Modelos
{
    public class Conexion
    {
        private const string Servidor = "DESKTOP-POFECHT\\SQLEXPRESS";
        private const string BaseDeDatos = "Veterinaria";

        private readonly string _cadena =
            "Data Source=" + Servidor + ";Initial Catalog=" + BaseDeDatos + ";Integrated Security=true;";

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_cadena);
        }
    }
}
