using System.Data;

namespace Modelos
{
    public class VacunaDAL : DALBase
    {
        public DataTable Listar(string filtro)
        {
            return Consultar(
                "SELECT idVacuna, nombre, descripcion, especieDestino, intervaloDias FROM vacuna " +
                "WHERE nombre LIKE @f OR especieDestino LIKE @f ORDER BY nombre",
                Like("@f", filtro));
        }

        public DataTable ListarParaCombo()
        {
            return Consultar("SELECT idVacuna, nombre, intervaloDias FROM vacuna ORDER BY nombre");
        }

        public bool ExisteNombre(string nombre, int idExcluir)
        {
            return Existe("SELECT COUNT(*) FROM vacuna WHERE nombre=@n AND idVacuna<>@id",
                P("@n", nombre), P("@id", idExcluir));
        }

        public string ObtenerEspecieDestino(int idVacuna)
        {
            object r = Escalar("SELECT especieDestino FROM vacuna WHERE idVacuna=@id", P("@id", idVacuna));
            return r == null ? "" : r.ToString();
        }

        public void Insertar(string nombre, string descripcion, string especieDestino, int intervaloDias)
        {
            Ejecutar(
                "INSERT INTO vacuna (nombre, descripcion, especieDestino, intervaloDias) VALUES (@n, @d, @e, @i)",
                P("@n", nombre), P("@d", descripcion), P("@e", especieDestino), P("@i", intervaloDias));
        }

        public void Actualizar(int idVacuna, string nombre, string descripcion, string especieDestino, int intervaloDias)
        {
            Ejecutar(
                "UPDATE vacuna SET nombre=@n, descripcion=@d, especieDestino=@e, intervaloDias=@i WHERE idVacuna=@id",
                P("@n", nombre), P("@d", descripcion), P("@e", especieDestino), P("@i", intervaloDias),
                P("@id", idVacuna));
        }

        public void Eliminar(int idVacuna)
        {
            int usos = (int)Escalar("SELECT COUNT(*) FROM aplicacionVacuna WHERE idVacuna=@id", P("@id", idVacuna));
            if (usos > 0)
                throw new AppException("ERR-NEG-001",
                    "No se puede eliminar la vacuna porque ya fue aplicada " + usos +
                    " vez/veces. Elimine primero esas aplicaciones.");

            Ejecutar("DELETE FROM vacuna WHERE idVacuna=@id", P("@id", idVacuna));
        }
    }
}
