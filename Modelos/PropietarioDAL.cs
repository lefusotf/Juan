using System.Data;

namespace Modelos
{
    public class PropietarioDAL : DALBase
    {
        public DataTable Listar(string filtro)
        {
            return Consultar(
                "SELECT idPropietario, nombre, dui, telefono, correo, direccion FROM propietario " +
                "WHERE nombre LIKE @f OR dui LIKE @f OR telefono LIKE @f ORDER BY nombre",
                Like("@f", filtro));
        }

        public bool ExisteDui(string dui, int idExcluir)
        {
            return Existe("SELECT COUNT(*) FROM propietario WHERE dui=@d AND idPropietario<>@id",
                P("@d", dui), P("@id", idExcluir));
        }

        public void Insertar(string nombre, string dui, string telefono, string correo, string direccion)
        {
            Ejecutar(
                "INSERT INTO propietario (nombre, dui, telefono, correo, direccion) VALUES (@n, @d, @t, @c, @dir)",
                P("@n", nombre), P("@d", dui), P("@t", telefono), P("@c", correo), P("@dir", direccion));
        }

        public void Actualizar(int idPropietario, string nombre, string dui, string telefono, string correo, string direccion)
        {
            Ejecutar(
                "UPDATE propietario SET nombre=@n, dui=@d, telefono=@t, correo=@c, direccion=@dir WHERE idPropietario=@id",
                P("@n", nombre), P("@d", dui), P("@t", telefono), P("@c", correo), P("@dir", direccion),
                P("@id", idPropietario));
        }

        public void Eliminar(int idPropietario)
        {
            int mascotas = (int)Escalar("SELECT COUNT(*) FROM mascota WHERE idPropietario=@id", P("@id", idPropietario));
            if (mascotas > 0)
                throw new AppException("ERR-NEG-001",
                    "No se puede eliminar al propietario porque tiene " + mascotas +
                    " mascota(s) registrada(s). Elimine o cambie de propietario primero a sus mascotas.");

            Ejecutar("DELETE FROM propietario WHERE idPropietario=@id", P("@id", idPropietario));
        }
    }
}
