using System.Data;
using Modelos.Conexion_DB;
using Modelos.Entidades;

namespace Modelos.Datos
{
    public static class MascotaDatos
    {
        public static DataTable Listar(string filtro)
        {
            return Conexion.Consultar(
                "SELECT idMascota, idPropietario, propietario, nombre, especie, raza, sexo, fechaNacimiento, peso, color " +
                "FROM vw_MascotasDetalle WHERE nombre LIKE @f OR propietario LIKE @f OR especie LIKE @f OR raza LIKE @f " +
                "ORDER BY nombre",
                Conexion.P("@f", "%" + (filtro ?? "") + "%"));
        }

        /// <summary>Lista corta para combos: idMascota y texto "Nombre (Propietario)".</summary>
        public static DataTable ListarParaCombo()
        {
            return Conexion.Consultar(
                "SELECT idMascota, nombre + ' (' + propietario + ')' AS descripcion FROM vw_MascotasDetalle ORDER BY nombre");
        }

        /// <summary>Indica si el propietario ya tiene otra mascota con ese nombre.</summary>
        public static bool ExisteNombreEnPropietario(int idPropietario, string nombre, int idExcluir)
        {
            return (int)Conexion.Escalar(
                "SELECT COUNT(*) FROM mascota WHERE idPropietario=@p AND nombre=@n AND idMascota<>@id",
                Conexion.P("@p", idPropietario), Conexion.P("@n", nombre), Conexion.P("@id", idExcluir)) > 0;
        }

        /// <summary>Devuelve la especie y la fecha de nacimiento de la mascota (o null si no existe).</summary>
        public static DataRow ObtenerBasico(int idMascota)
        {
            DataTable t = Conexion.Consultar("SELECT especie, fechaNacimiento FROM mascota WHERE idMascota=@id",
                Conexion.P("@id", idMascota));
            return t.Rows.Count == 0 ? null : t.Rows[0];
        }

        public static void Insertar(Mascota m)
        {
            Conexion.EjecutarNoQuery(
                "INSERT INTO mascota (idPropietario, nombre, especie, raza, sexo, fechaNacimiento, peso, color) " +
                "VALUES (@prop, @n, @e, @r, @s, @f, @p, @c)",
                Conexion.P("@prop", m.IdPropietario), Conexion.P("@n", m.Nombre), Conexion.P("@e", m.Especie),
                Conexion.P("@r", m.Raza), Conexion.P("@s", m.Sexo), Conexion.P("@f", m.FechaNacimiento),
                Conexion.P("@p", m.Peso), Conexion.P("@c", m.Color));
        }

        public static void Actualizar(Mascota m)
        {
            Conexion.EjecutarNoQuery(
                "UPDATE mascota SET idPropietario=@prop, nombre=@n, especie=@e, raza=@r, sexo=@s, fechaNacimiento=@f, " +
                "peso=@p, color=@c WHERE idMascota=@id",
                Conexion.P("@prop", m.IdPropietario), Conexion.P("@n", m.Nombre), Conexion.P("@e", m.Especie),
                Conexion.P("@r", m.Raza), Conexion.P("@s", m.Sexo), Conexion.P("@f", m.FechaNacimiento),
                Conexion.P("@p", m.Peso), Conexion.P("@c", m.Color), Conexion.P("@id", m.IdMascota));
        }

        public static void Eliminar(int id)
        {
            DataRow r = Conexion.Consultar(
                "SELECT (SELECT COUNT(*) FROM cita WHERE idMascota=@id) AS citas, " +
                "(SELECT COUNT(*) FROM consulta WHERE idMascota=@id) AS consultas, " +
                "(SELECT COUNT(*) FROM aplicacionVacuna WHERE idMascota=@id) AS vacunas",
                Conexion.P("@id", id)).Rows[0];
            int citas = (int)r["citas"], consultas = (int)r["consultas"], vacunas = (int)r["vacunas"];
            if (citas + consultas + vacunas > 0)
                throw new System.InvalidOperationException("No se puede eliminar la mascota porque tiene historial: " +
                    citas + " cita(s), " + consultas + " consulta(s) y " + vacunas +
                    " vacuna(s) aplicada(s). Elimine primero esos registros.");

            Conexion.EjecutarNoQuery("DELETE FROM mascota WHERE idMascota=@id", Conexion.P("@id", id));
        }
    }
}
