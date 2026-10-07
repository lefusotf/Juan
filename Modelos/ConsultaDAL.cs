using System;
using System.Data;

namespace Modelos
{
    public class ConsultaDAL : DALBase
    {
        public DataTable Listar(string filtro)
        {
            return Consultar(
                "SELECT idConsulta, idMascota, mascota, propietario, idVeterinario, veterinario, fecha, motivo, " +
                "diagnostico, tratamiento, observaciones FROM vw_ConsultasDetalle " +
                "WHERE mascota LIKE @f OR propietario LIKE @f OR diagnostico LIKE @f OR veterinario LIKE @f " +
                "ORDER BY fecha DESC",
                Like("@f", filtro));
        }

        public void Insertar(int idMascota, int idVeterinario, DateTime fecha, string motivo, string diagnostico,
            string tratamiento, string observaciones)
        {
            Ejecutar(
                "INSERT INTO consulta (idMascota, idVeterinario, fecha, motivo, diagnostico, tratamiento, observaciones) " +
                "VALUES (@m, @v, @f, @mo, @d, @t, @o)",
                P("@m", idMascota), P("@v", idVeterinario), P("@f", fecha), P("@mo", motivo), P("@d", diagnostico),
                P("@t", tratamiento), P("@o", observaciones));
        }

        public void Actualizar(int idConsulta, int idMascota, int idVeterinario, DateTime fecha, string motivo,
            string diagnostico, string tratamiento, string observaciones)
        {
            Ejecutar(
                "UPDATE consulta SET idMascota=@m, idVeterinario=@v, fecha=@f, motivo=@mo, diagnostico=@d, " +
                "tratamiento=@t, observaciones=@o WHERE idConsulta=@id",
                P("@m", idMascota), P("@v", idVeterinario), P("@f", fecha), P("@mo", motivo), P("@d", diagnostico),
                P("@t", tratamiento), P("@o", observaciones), P("@id", idConsulta));
        }

        public void Eliminar(int idConsulta)
        {
            Ejecutar("DELETE FROM consulta WHERE idConsulta=@id", P("@id", idConsulta));
        }
    }
}
