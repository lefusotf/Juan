using System.Data;
using Modelos.Conexion_DB;
using Modelos.Entidades;

namespace Modelos.Datos
{
    public static class CitaDatos
    {
        public static DataTable Listar(string filtro)
        {
            return Conexion.Consultar(
                "SELECT idCita, idMascota, mascota, propietario, idVeterinario, veterinario, fechaHora, motivo, estado " +
                "FROM vw_CitasDetalle WHERE mascota LIKE @f OR propietario LIKE @f OR veterinario LIKE @f OR estado LIKE @f " +
                "ORDER BY fechaHora DESC",
                Conexion.P("@f", "%" + (filtro ?? "") + "%"));
        }

        /// <summary>Indica si el veterinario ya tiene otra cita programada a esa misma fecha y hora.</summary>
        public static bool VeterinarioOcupado(int idVeterinario, System.DateTime fechaHora, int idCitaExcluir)
        {
            object r = Conexion.Escalar(
                "SELECT COUNT(*) FROM cita WHERE idVeterinario=@v AND ABS(DATEDIFF(MINUTE, fechaHora, @f)) < 30 " +
                "AND estado='Programada' AND idCita<>@id",
                Conexion.P("@v", idVeterinario), Conexion.P("@f", fechaHora), Conexion.P("@id", idCitaExcluir));
            return (int)r > 0;
        }

        /// <summary>Indica si la mascota ya tiene otra cita programada ese mismo día.</summary>
        public static bool MascotaTieneCitaEseDia(int idMascota, System.DateTime fechaHora, int idCitaExcluir)
        {
            object r = Conexion.Escalar(
                "SELECT COUNT(*) FROM cita WHERE idMascota=@m AND estado='Programada' " +
                "AND CAST(fechaHora AS DATE)=CAST(@f AS DATE) AND idCita<>@id",
                Conexion.P("@m", idMascota), Conexion.P("@f", fechaHora), Conexion.P("@id", idCitaExcluir));
            return (int)r > 0;
        }

        public static void Insertar(Cita c)
        {
            Conexion.EjecutarNoQuery(
                "INSERT INTO cita (idMascota, idVeterinario, fechaHora, motivo, estado, idUsuarioRegistro) " +
                "VALUES (@m, @v, @f, @mo, @e, @u)",
                Conexion.P("@m", c.IdMascota), Conexion.P("@v", c.IdVeterinario), Conexion.P("@f", c.FechaHora),
                Conexion.P("@mo", c.Motivo), Conexion.P("@e", c.Estado), Conexion.P("@u", c.IdUsuarioRegistro));
        }

        public static void Actualizar(Cita c)
        {
            Conexion.EjecutarNoQuery(
                "UPDATE cita SET idMascota=@m, idVeterinario=@v, fechaHora=@f, motivo=@mo, estado=@e WHERE idCita=@id",
                Conexion.P("@m", c.IdMascota), Conexion.P("@v", c.IdVeterinario), Conexion.P("@f", c.FechaHora),
                Conexion.P("@mo", c.Motivo), Conexion.P("@e", c.Estado), Conexion.P("@id", c.IdCita));
        }

        public static void Eliminar(int id)
        {
            Conexion.EjecutarNoQuery("DELETE FROM cita WHERE idCita=@id", Conexion.P("@id", id));
        }
    }
}
