namespace Modelos.Entidades
{
    public class Vacuna
    {
        public int IdVacuna { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string EspecieDestino { get; set; }
        public int IntervaloDias { get; set; }
    }
}
