using System;

namespace Modelos.Entidades
{
    public class AplicacionVacuna
    {
        public int IdAplicacion { get; set; }
        public int IdMascota { get; set; }
        public int IdVacuna { get; set; }
        public int IdVeterinario { get; set; }
        public DateTime FechaAplicacion { get; set; }
        public DateTime? ProximaDosis { get; set; }
        public string Observaciones { get; set; }
    }
}
