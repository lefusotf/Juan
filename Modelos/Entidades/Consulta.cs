using System;

namespace Modelos.Entidades
{
    public class Consulta
    {
        public int IdConsulta { get; set; }
        public int IdMascota { get; set; }
        public int IdVeterinario { get; set; }
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public string Observaciones { get; set; }
    }
}
