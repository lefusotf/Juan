using System.Data;

namespace Modelos
{
    public class RolDAL : DALBase
    {
        public DataTable Listar()
        {
            return Consultar("SELECT idRol, nombre, descripcion FROM rol ORDER BY nombre");
        }
    }
}
