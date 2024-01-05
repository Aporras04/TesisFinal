using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetVet.Models
{
    public class Mascota
    {
        public string Nombre { get; set; }
        public string Raza { get; set; }
        public string Edad { get; set; }
        public string Especie { get; set; }
        public string Sexo { get; set; }
        public string Color { get; set; }
        public bool Esterilizado { get; set; }
        public string Veterinaria { get; set; }
    }
}