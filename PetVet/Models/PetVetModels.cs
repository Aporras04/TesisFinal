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
        [StringLength(45, ErrorMessage ="Maximo 45 caracteres"), MinLength(10, ErrorMessage = "Minimo 10 caracteres")]
        [Required(ErrorMessage = "Campo requerido")]
        public string Nombre { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(60, ErrorMessage = "Maximo 60 caracteres"), MinLength(5, ErrorMessage = "Minimo 5 caracteres")]
        [Required(ErrorMessage = "Campo requerido")]
        public string Direccion { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(10, ErrorMessage = "Maximo 10 caracteres"), MinLength(10, ErrorMessage = "Minimo 10 caracteres"), RegularExpression("([0-9]+)", ErrorMessage = "Ingresar solo numeros")]
        [Required(ErrorMessage = "Campo requerido")]
        public string Cedula { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(10,ErrorMessage ="Maximo 10 caracteres"), MinLength(7, ErrorMessage = "Minimo 7 caracteres"), RegularExpression("([0-9]+)", ErrorMessage = "Ingresar solo numeros")]
        [Required(ErrorMessage = "Campo requerido")]
        public string Telefono { get; set; }

        [Column(TypeName = "VARCHAR")]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Correo no valido")]
        [Required(ErrorMessage = "Campo requerido")]
        public string Mail { get; set; }

        [Column(TypeName = "VARCHAR")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage ="Contrasena invalida")]
        [Required(ErrorMessage = "Campo requerido")]
        public string Password { get; set; }

        public ICollection<Mascota> Mascotas { get; set; }

        
    }

    public class Mascota
    {
        [Key]
        public int MascotaID { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(45, ErrorMessage = "Maximo 45 caracteres"), MinLength(2, ErrorMessage = "Minimo 2 caracteres")]
        [Required(ErrorMessage = "Campo requerido")]
        public string Nombre { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Required(ErrorMessage = "Campo requerido")]
        public string Especie { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(20, ErrorMessage = "Maximo 20 caracteres")]
        [Required(ErrorMessage = "Campo requerido")]
        public string Raza { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(10, ErrorMessage = "Maximo 10 caracteres")]
        [Required(ErrorMessage = "Campo requerido")]
        public string Edad { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(6, ErrorMessage = "Maximo 6 caracteres")]
        [Required(ErrorMessage = "Campo requerido")]
        public string Sexo { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [Required(ErrorMessage = "Campo requerido")]
        public string Color { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Required(ErrorMessage = "Campo requerido")]
        public string Esterilizado { get; set; }
        public int Veterinaria { get; set; }
        public ICollection<Tratamiento> Tratamientos { get; set; }

        public string NombreUsuario { get; set; }
        public string CedulaUsuario { get; set; }

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

        public int Mascota { get; set; }

        public string NombreVeterinario { get; set; }
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

        //public Mascota Mascota { get; set; }

        //public Usuario Doctor { get; set; }
    }

    //public class Doctor
    //{
    //    [Key]
    //    public int DoctorID { get; set; }

    //    [Column(TypeName = "VARCHAR")]
    //    [StringLength(45)]
    //    public string Nombre { get; set; }

    //    [Column(TypeName = "VARCHAR")]
    //    [StringLength(10)]
    //    public string Cedula { get; set; }

    //    [Column(TypeName = "VARCHAR")]
    //    [StringLength(45)]
    //    public string Mail { get; set; }

    //    public ICollection<Veterinaria> Veterinarias { get; set; }
    //}
}