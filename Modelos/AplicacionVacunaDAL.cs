using System;
using System.Data;

namespace Modelos
{
    public class AplicacionVacunaDAL : DALBase
    {
        public DataTable Listar(string filtro)
        {
            return Consultar(
                "SELECT idAplicacion, idMascota, mascota, idVacuna, vacuna, idVeterinario, veterinario, " +
                "fechaAplicacion, proximaDosis, observaciones FROM vw_AplicacionesDetalle " +
                "WHERE mascota LIKE @f OR vacuna LIKE @f OR veterinario LIKE @f ORDER BY fechaAplicacion DESC",
                Like("@f", filtro));
        }

        public bool ExisteAplicacion(int idMascota, int idVacuna, DateTime fecha, int idExcluir)
        {
            return Existe(
                "SELECT COUNT(*) FROM aplicacionVacuna WHERE idMascota=@m AND idVacuna=@v AND fechaAplicacion=@f AND idAplicacion<>@id",
                P("@m", idMascota), P("@v", idVacuna), P("@f", fecha.Date), P("@id", idExcluir));
        }

        public void Insertar(int idMascota, int idVacuna, int idVeterinario, DateTime fechaAplicacion,
            DateTime? proximaDosis, string observaciones)
        {
            Ejecutar(
                "INSERT INTO aplicacionVacuna (idMascota, idVacuna, idVeterinario, fechaAplicacion, proximaDosis, observaciones) " +
                "VALUES (@m, @v, @vet, @f, @p, @o)",
                P("@m", idMascota), P("@v", idVacuna), P("@vet", idVeterinario), P("@f", fechaAplicacion),
                P("@p", proximaDosis), P("@o", observaciones));
        }

        public void Actualizar(int idAplicacion, int idMascota, int idVacuna, int idVeterinario,
            DateTime fechaAplicacion, DateTime? proximaDosis, string observaciones)
        {
            Ejecutar(
                "UPDATE aplicacionVacuna SET idMascota=@m, idVacuna=@v, idVeterinario=@vet, fechaAplicacion=@f, " +
                "proximaDosis=@p, observaciones=@o WHERE idAplicacion=@id",
                P("@m", idMascota), P("@v", idVacuna), P("@vet", idVeterinario), P("@f", fechaAplicacion),
                P("@p", proximaDosis), P("@o", observaciones), P("@id", idAplicacion));
        }

        public void Eliminar(int idAplicacion)
        {
            Ejecutar("DELETE FROM aplicacionVacuna WHERE idAplicacion=@id", P("@id", idAplicacion));
        }
    }
}
