using System.Data;
using Modelos.Conexion_DB;
using Modelos.Entidades;

namespace Modelos.Datos
{
    public static class ConsultaDatos
    {
        public static DataTable Listar(string filtro)
        {
            return Conexion.Consultar(
                "SELECT idConsulta, idMascota, mascota, propietario, idVeterinario, veterinario, fecha, motivo, " +
                "diagnostico, tratamiento, observaciones FROM vw_ConsultasDetalle " +
                "WHERE mascota LIKE @f OR propietario LIKE @f OR diagnostico LIKE @f OR veterinario LIKE @f " +
                "ORDER BY fecha DESC",
                Conexion.P("@f", "%" + (filtro ?? "") + "%"));
        }

        public static void Insertar(Consulta c)
        {
            Conexion.EjecutarNoQuery(
                "INSERT INTO consulta (idMascota, idVeterinario, fecha, motivo, diagnostico, tratamiento, observaciones) " +
                "VALUES (@m, @v, @f, @mo, @d, @t, @o)",
                Conexion.P("@m", c.IdMascota), Conexion.P("@v", c.IdVeterinario), Conexion.P("@f", c.Fecha),
                Conexion.P("@mo", c.Motivo), Conexion.P("@d", c.Diagnostico), Conexion.P("@t", c.Tratamiento),
                Conexion.P("@o", c.Observaciones));
        }

        public static void Actualizar(Consulta c)
        {
            Conexion.EjecutarNoQuery(
                "UPDATE consulta SET idMascota=@m, idVeterinario=@v, fecha=@f, motivo=@mo, diagnostico=@d, " +
                "tratamiento=@t, observaciones=@o WHERE idConsulta=@id",
                Conexion.P("@m", c.IdMascota), Conexion.P("@v", c.IdVeterinario), Conexion.P("@f", c.Fecha),
                Conexion.P("@mo", c.Motivo), Conexion.P("@d", c.Diagnostico), Conexion.P("@t", c.Tratamiento),
                Conexion.P("@o", c.Observaciones), Conexion.P("@id", c.IdConsulta));
        }

        public static void Eliminar(int id)
        {
            Conexion.EjecutarNoQuery("DELETE FROM consulta WHERE idConsulta=@id", Conexion.P("@id", id));
        }
    }
}
