using System.Data;

namespace Modelos
{
    public class DashboardDAL : DALBase
    {
        public DataRow Resumen()
        {
            DataTable t = Consultar(
                "SELECT " +
                "(SELECT COUNT(*) FROM propietario) AS propietarios, " +
                "(SELECT COUNT(*) FROM mascota) AS mascotas, " +
                "(SELECT COUNT(*) FROM cita WHERE estado = 'Programada' AND fechaHora >= CAST(GETDATE() AS DATE)) AS citas, " +
                "(SELECT COUNT(*) FROM consulta) AS consultas, " +
                "(SELECT COUNT(*) FROM aplicacionVacuna) AS vacunas");
            return t.Rows[0];
        }

        public DataTable ProximasCitas()
        {
            return Consultar(
                "SELECT TOP 8 fechaHora, mascota, propietario, veterinario, motivo FROM vw_CitasDetalle " +
                "WHERE estado = 'Programada' AND fechaHora >= CAST(GETDATE() AS DATE) ORDER BY fechaHora");
        }
    }
}
