using System.Data;

namespace Modelos
{
    public class BitacoraDAL : DALBase
    {
        public DataTable Listar(string nivel, string filtro)
        {
            return Consultar(
                "SELECT TOP 500 idBitacora, fecha, nivel, nombreUsuario, modulo, mensaje FROM bitacora " +
                "WHERE (@nivel = '' OR nivel = @nivel) AND (nombreUsuario LIKE @f OR modulo LIKE @f OR mensaje LIKE @f) " +
                "ORDER BY fecha DESC, idBitacora DESC",
                new System.Data.SqlClient.SqlParameter("@nivel", nivel ?? ""),
                Like("@f", filtro));
        }
    }
}
