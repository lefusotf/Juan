using System.Data;
using Modelos.Conexion_DB;

namespace Modelos.Datos
{
    /// <summary>Consultas para la pantalla de inicio (estadísticas y próximas citas).</summary>
    public static class InicioDatos
    {
        public static DataRow Resumen()
        {
            DataTable t = Conexion.Consultar(
                "SELECT " +
                "(SELECT COUNT(*) FROM propietario) AS propietarios, " +
                "(SELECT COUNT(*) FROM mascota) AS mascotas, " +
                "(SELECT COUNT(*) FROM cita WHERE estado = 'Programada' AND fechaHora >= CAST(GETDATE() AS DATE)) AS citas, " +
                "(SELECT COUNT(*) FROM consulta) AS consultas, " +
                "(SELECT COUNT(*) FROM aplicacionVacuna) AS vacunas");
            return t.Rows[0];
        }

        public static DataTable ProximasCitas()
        {
            return Conexion.Consultar(
                "SELECT TOP 8 fechaHora, mascota, propietario, veterinario, motivo FROM vw_CitasDetalle " +
                "WHERE estado = 'Programada' AND fechaHora >= CAST(GETDATE() AS DATE) ORDER BY fechaHora");
        }
    }
}
