using System.Data;
using System.Data.SqlClient;
using Modelos.Conexion_DB;

namespace Modelos.Datos
{
    public static class BitacoraDatos
    {
        public static DataTable Listar(string nivel, string filtro)
        {
            return Conexion.Consultar(
                "SELECT TOP 500 idBitacora, fecha, nivel, nombreUsuario, modulo, mensaje FROM bitacora " +
                "WHERE (@nivel = '' OR nivel = @nivel) AND (nombreUsuario LIKE @f OR modulo LIKE @f OR mensaje LIKE @f) " +
                "ORDER BY fecha DESC, idBitacora DESC",
                new SqlParameter("@nivel", nivel ?? ""),
                Conexion.P("@f", "%" + (filtro ?? "") + "%"));
        }
    }
}
