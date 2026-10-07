using System;
using System.Data;

namespace Modelos
{
    public class CitaDAL : DALBase
    {
        public DataTable Listar(string filtro)
        {
            return Consultar(
                "SELECT idCita, idMascota, mascota, propietario, idVeterinario, veterinario, fechaHora, motivo, estado " +
                "FROM vw_CitasDetalle WHERE mascota LIKE @f OR propietario LIKE @f OR veterinario LIKE @f OR estado LIKE @f " +
                "ORDER BY fechaHora DESC",
                Like("@f", filtro));
        }

        public bool VeterinarioOcupado(int idVeterinario, DateTime fechaHora, int idCitaExcluir)
        {
            return Existe(
                "SELECT COUNT(*) FROM cita WHERE idVeterinario=@v AND ABS(DATEDIFF(MINUTE, fechaHora, @f)) < 30 " +
                "AND estado='Programada' AND idCita<>@id",
                P("@v", idVeterinario), P("@f", fechaHora), P("@id", idCitaExcluir));
        }

        public bool MascotaTieneCitaEseDia(int idMascota, DateTime fechaHora, int idCitaExcluir)
        {
            return Existe(
                "SELECT COUNT(*) FROM cita WHERE idMascota=@m AND estado='Programada' " +
                "AND CAST(fechaHora AS DATE)=CAST(@f AS DATE) AND idCita<>@id",
                P("@m", idMascota), P("@f", fechaHora), P("@id", idCitaExcluir));
        }

        public void Insertar(int idMascota, int idVeterinario, DateTime fechaHora, string motivo, string estado,
            int idUsuarioRegistro)
        {
            Ejecutar(
                "INSERT INTO cita (idMascota, idVeterinario, fechaHora, motivo, estado, idUsuarioRegistro) " +
                "VALUES (@m, @v, @f, @mo, @e, @u)",
                P("@m", idMascota), P("@v", idVeterinario), P("@f", fechaHora), P("@mo", motivo), P("@e", estado),
                P("@u", idUsuarioRegistro));
        }

        public void Actualizar(int idCita, int idMascota, int idVeterinario, DateTime fechaHora, string motivo,
            string estado)
        {
            Ejecutar(
                "UPDATE cita SET idMascota=@m, idVeterinario=@v, fechaHora=@f, motivo=@mo, estado=@e WHERE idCita=@id",
                P("@m", idMascota), P("@v", idVeterinario), P("@f", fechaHora), P("@mo", motivo), P("@e", estado),
                P("@id", idCita));
        }

        public void Eliminar(int idCita)
        {
            Ejecutar("DELETE FROM cita WHERE idCita=@id", P("@id", idCita));
        }
    }
}
