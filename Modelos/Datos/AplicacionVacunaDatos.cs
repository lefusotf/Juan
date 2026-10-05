using System.Data;
using Modelos.Conexion_DB;
using Modelos.Entidades;

namespace Modelos.Datos
{
    public static class AplicacionVacunaDatos
    {
        public static DataTable Listar(string filtro)
        {
            return Conexion.Consultar(
                "SELECT idAplicacion, idMascota, mascota, idVacuna, vacuna, idVeterinario, veterinario, " +
                "fechaAplicacion, proximaDosis, observaciones FROM vw_AplicacionesDetalle " +
                "WHERE mascota LIKE @f OR vacuna LIKE @f OR veterinario LIKE @f ORDER BY fechaAplicacion DESC",
                Conexion.P("@f", "%" + (filtro ?? "") + "%"));
        }

        /// <summary>Indica si ya se registró la misma vacuna a la misma mascota en la misma fecha.</summary>
        public static bool ExisteAplicacion(int idMascota, int idVacuna, System.DateTime fecha, int idExcluir)
        {
            return (int)Conexion.Escalar(
                "SELECT COUNT(*) FROM aplicacionVacuna WHERE idMascota=@m AND idVacuna=@v AND fechaAplicacion=@f AND idAplicacion<>@id",
                Conexion.P("@m", idMascota), Conexion.P("@v", idVacuna), Conexion.P("@f", fecha.Date), Conexion.P("@id", idExcluir)) > 0;
        }

        public static void Insertar(AplicacionVacuna a)
        {
            Conexion.EjecutarNoQuery(
                "INSERT INTO aplicacionVacuna (idMascota, idVacuna, idVeterinario, fechaAplicacion, proximaDosis, observaciones) " +
                "VALUES (@m, @v, @vet, @f, @p, @o)",
                Conexion.P("@m", a.IdMascota), Conexion.P("@v", a.IdVacuna), Conexion.P("@vet", a.IdVeterinario),
                Conexion.P("@f", a.FechaAplicacion), Conexion.P("@p", a.ProximaDosis), Conexion.P("@o", a.Observaciones));
        }

        public static void Actualizar(AplicacionVacuna a)
        {
            Conexion.EjecutarNoQuery(
                "UPDATE aplicacionVacuna SET idMascota=@m, idVacuna=@v, idVeterinario=@vet, fechaAplicacion=@f, " +
                "proximaDosis=@p, observaciones=@o WHERE idAplicacion=@id",
                Conexion.P("@m", a.IdMascota), Conexion.P("@v", a.IdVacuna), Conexion.P("@vet", a.IdVeterinario),
                Conexion.P("@f", a.FechaAplicacion), Conexion.P("@p", a.ProximaDosis), Conexion.P("@o", a.Observaciones),
                Conexion.P("@id", a.IdAplicacion));
        }

        public static void Eliminar(int id)
        {
            Conexion.EjecutarNoQuery("DELETE FROM aplicacionVacuna WHERE idAplicacion=@id", Conexion.P("@id", id));
        }
    }
}
