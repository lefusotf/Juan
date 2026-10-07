using System;
using System.Data;

namespace Modelos
{
    public class MascotaDAL : DALBase
    {
        public DataTable Listar(string filtro)
        {
            return Consultar(
                "SELECT idMascota, idPropietario, propietario, nombre, especie, raza, sexo, fechaNacimiento, peso, color " +
                "FROM vw_MascotasDetalle WHERE nombre LIKE @f OR propietario LIKE @f OR especie LIKE @f OR raza LIKE @f " +
                "ORDER BY nombre",
                Like("@f", filtro));
        }

        public DataTable ListarParaCombo()
        {
            return Consultar(
                "SELECT idMascota, nombre + ' (' + propietario + ')' AS descripcion FROM vw_MascotasDetalle ORDER BY nombre");
        }

        public bool ExisteNombreEnPropietario(int idPropietario, string nombre, int idExcluir)
        {
            return Existe(
                "SELECT COUNT(*) FROM mascota WHERE idPropietario=@p AND nombre=@n AND idMascota<>@id",
                P("@p", idPropietario), P("@n", nombre), P("@id", idExcluir));
        }

        public DataRow ObtenerBasico(int idMascota)
        {
            DataTable t = Consultar("SELECT especie, fechaNacimiento FROM mascota WHERE idMascota=@id",
                P("@id", idMascota));
            return t.Rows.Count == 0 ? null : t.Rows[0];
        }

        public void Insertar(int idPropietario, string nombre, string especie, string raza, string sexo,
            DateTime? fechaNacimiento, decimal? peso, string color)
        {
            Ejecutar(
                "INSERT INTO mascota (idPropietario, nombre, especie, raza, sexo, fechaNacimiento, peso, color) " +
                "VALUES (@prop, @n, @e, @r, @s, @f, @p, @c)",
                P("@prop", idPropietario), P("@n", nombre), P("@e", especie), P("@r", raza), P("@s", sexo),
                P("@f", fechaNacimiento), P("@p", peso), P("@c", color));
        }

        public void Actualizar(int idMascota, int idPropietario, string nombre, string especie, string raza, string sexo,
            DateTime? fechaNacimiento, decimal? peso, string color)
        {
            Ejecutar(
                "UPDATE mascota SET idPropietario=@prop, nombre=@n, especie=@e, raza=@r, sexo=@s, fechaNacimiento=@f, " +
                "peso=@p, color=@c WHERE idMascota=@id",
                P("@prop", idPropietario), P("@n", nombre), P("@e", especie), P("@r", raza), P("@s", sexo),
                P("@f", fechaNacimiento), P("@p", peso), P("@c", color), P("@id", idMascota));
        }

        public void Eliminar(int idMascota)
        {
            DataRow r = Consultar(
                "SELECT (SELECT COUNT(*) FROM cita WHERE idMascota=@id) AS citas, " +
                "(SELECT COUNT(*) FROM consulta WHERE idMascota=@id) AS consultas, " +
                "(SELECT COUNT(*) FROM aplicacionVacuna WHERE idMascota=@id) AS vacunas",
                P("@id", idMascota)).Rows[0];
            int citas = (int)r["citas"], consultas = (int)r["consultas"], vacunas = (int)r["vacunas"];
            if (citas + consultas + vacunas > 0)
                throw new AppException("ERR-NEG-001",
                    "No se puede eliminar la mascota porque tiene historial: " + citas + " cita(s), " + consultas +
                    " consulta(s) y " + vacunas + " vacuna(s) aplicada(s). Elimine primero esos registros.");

            Ejecutar("DELETE FROM mascota WHERE idMascota=@id", P("@id", idMascota));
        }
    }
}
