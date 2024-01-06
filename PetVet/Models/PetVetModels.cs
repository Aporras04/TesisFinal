using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetVet.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(45)]
        [Required]
        public string Nombre { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(60)]
        [Required]
        public string Direccion { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(10), MinLength(10), RegularExpression("([0-9]+)")]
        [Required]
        public string Cedula { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(10,ErrorMessage ="hola"), MinLength(10, ErrorMessage = "hola2"), RegularExpression("([0-9]+)", ErrorMessage = "hola3")]
        [Required]
        public string Telefono { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(45)]
        [Required]
        public string Mail { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(45)]
        [Required]
        public string Password { get; set; }

        public ICollection<Mascota> Mascotas { get; set; }
    }

    public class Mascota
    {
        [Key]
        public int MascotaID { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(45)]
        public string Nombre { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(45)]
        public string Especie { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(45)]
        public string Raza { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(2)]
        public string Edad { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(1)]
        public string Sexo { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        public string Color { get; set; }

        public bool Esterilizado { get; set; }
        public ICollection<Veterinaria> Veterinarias { get; set; }
        public ICollection<Tratamiento> Tratamientos { get; set; }
        public Usuario Usuario { get; set; }
    }

    public class Tratamiento
    {
        [Key]
        public int idTratamiento { get; set; }

        public DateTime Fecha { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(45)]
        public string Descripcion { get; set; }

        [Column(TypeName = "FLOAT")]
        public float PesoMascota { get; set; }

        public DateTime FechaProxima { get; set; }

        public Mascota Mascota { get; set; }

    }

    public class Veterinaria
    {
        [Key]
        public int VeterinariaID { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(45)]
        public string Nombre { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(45)]
        public string Direccion { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        public string Telefono { get; set; }

        public Mascota Mascota { get; set; }

        public Doctor Doctor { get; set; }
    }

    public class Doctor
    {
        [Key]
        public int DoctorID { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(45)]
        public string Nombre { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        public string Cedula { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(45)]
        public string Mail { get; set; }

        public ICollection<Veterinaria> Veterinarias { get; set; }
    }
}