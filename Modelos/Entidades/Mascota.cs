using System;

namespace Modelos.Entidades
{
    public class Mascota
    {
        public int IdMascota { get; set; }
        public int IdPropietario { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Raza { get; set; }
        public string Sexo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public decimal? Peso { get; set; }
        public string Color { get; set; }
    }
}
