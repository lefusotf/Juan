using System.Data;
using Modelos.Conexion_DB;
using Modelos.Entidades;

namespace Modelos.Datos
{
    public static class PropietarioDatos
    {
        public static DataTable Listar(string filtro)
        {
            return Conexion.Consultar(
                "SELECT idPropietario, nombre, dui, telefono, correo, direccion FROM propietario " +
                "WHERE nombre LIKE @f OR dui LIKE @f OR telefono LIKE @f ORDER BY nombre",
                Conexion.P("@f", "%" + (filtro ?? "") + "%"));
        }

        public static void Insertar(Propietario p)
        {
            Conexion.EjecutarNoQuery(
                "INSERT INTO propietario (nombre, dui, telefono, correo, direccion) VALUES (@n, @d, @t, @c, @dir)",
                Conexion.P("@n", p.Nombre), Conexion.P("@d", p.Dui), Conexion.P("@t", p.Telefono),
                Conexion.P("@c", p.Correo), Conexion.P("@dir", p.Direccion));
        }

        public static void Actualizar(Propietario p)
        {
            Conexion.EjecutarNoQuery(
                "UPDATE propietario SET nombre=@n, dui=@d, telefono=@t, correo=@c, direccion=@dir WHERE idPropietario=@id",
                Conexion.P("@n", p.Nombre), Conexion.P("@d", p.Dui), Conexion.P("@t", p.Telefono),
                Conexion.P("@c", p.Correo), Conexion.P("@dir", p.Direccion), Conexion.P("@id", p.IdPropietario));
        }

        public static void Eliminar(int id)
        {
            Conexion.EjecutarNoQuery("DELETE FROM propietario WHERE idPropietario=@id", Conexion.P("@id", id));
        }
    }
}
