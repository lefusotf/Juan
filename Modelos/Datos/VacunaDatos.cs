using System.Data;
using Modelos.Conexion_DB;
using Modelos.Entidades;

namespace Modelos.Datos
{
    public static class VacunaDatos
    {
        public static DataTable Listar(string filtro)
        {
            return Conexion.Consultar(
                "SELECT idVacuna, nombre, descripcion, especieDestino, intervaloDias FROM vacuna " +
                "WHERE nombre LIKE @f OR especieDestino LIKE @f ORDER BY nombre",
                Conexion.P("@f", "%" + (filtro ?? "") + "%"));
        }

        public static DataTable ListarParaCombo()
        {
            return Conexion.Consultar("SELECT idVacuna, nombre, intervaloDias FROM vacuna ORDER BY nombre");
        }

        /// <summary>Indica si ya existe otra vacuna con ese nombre.</summary>
        public static bool ExisteNombre(string nombre, int idExcluir)
        {
            return (int)Conexion.Escalar("SELECT COUNT(*) FROM vacuna WHERE nombre=@n AND idVacuna<>@id",
                Conexion.P("@n", nombre), Conexion.P("@id", idExcluir)) > 0;
        }

        /// <summary>Especie a la que está destinada la vacuna.</summary>
        public static string ObtenerEspecieDestino(int idVacuna)
        {
            object r = Conexion.Escalar("SELECT especieDestino FROM vacuna WHERE idVacuna=@id", Conexion.P("@id", idVacuna));
            return r == null ? "" : r.ToString();
        }

        public static void Insertar(Vacuna v)
        {
            Conexion.EjecutarNoQuery(
                "INSERT INTO vacuna (nombre, descripcion, especieDestino, intervaloDias) VALUES (@n, @d, @e, @i)",
                Conexion.P("@n", v.Nombre), Conexion.P("@d", v.Descripcion),
                Conexion.P("@e", v.EspecieDestino), Conexion.P("@i", v.IntervaloDias));
        }

        public static void Actualizar(Vacuna v)
        {
            Conexion.EjecutarNoQuery(
                "UPDATE vacuna SET nombre=@n, descripcion=@d, especieDestino=@e, intervaloDias=@i WHERE idVacuna=@id",
                Conexion.P("@n", v.Nombre), Conexion.P("@d", v.Descripcion), Conexion.P("@e", v.EspecieDestino),
                Conexion.P("@i", v.IntervaloDias), Conexion.P("@id", v.IdVacuna));
        }

        public static void Eliminar(int id)
        {
            int usos = (int)Conexion.Escalar("SELECT COUNT(*) FROM aplicacionVacuna WHERE idVacuna=@id", Conexion.P("@id", id));
            if (usos > 0)
                throw new System.InvalidOperationException("No se puede eliminar la vacuna porque ya fue aplicada " + usos +
                    " vez/veces. Elimine primero esas aplicaciones.");

            Conexion.EjecutarNoQuery("DELETE FROM vacuna WHERE idVacuna=@id", Conexion.P("@id", id));
        }
    }
}
