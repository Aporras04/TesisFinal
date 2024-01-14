namespace PetVet.Migrations
{
    using PetVet.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PetVet.Data.PetVetContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PetVet.Data.PetVetContext context)
        {
            if (!context.Veterinarias.Any(v => v.Nombre == "Clínica de Mascotas AmigoAnimal"))
            {
                context.Veterinarias.Add(new Veterinaria { VeterinariaID = 100, Nombre = "Clínica de Mascotas AmigoAnimal", Direccion = "Vía al Ilaló S5-198 y Rumihuayco", Telefono = "0984782136" });
            }

            if (!context.Veterinarias.Any(v => v.Nombre == "Clínica Veterinaria Animalia"))
            {
                context.Veterinarias.Add(new Veterinaria { VeterinariaID = 101, Nombre = "Clínica Veterinaria Animalia", Direccion = "Av. Alamos E9-107 y Carlos Alvarado", Telefono = "0965872558" });
            }

            if (!context.Veterinarias.Any(v => v.Nombre == "Hospital Veterinario PetCare"))
            {
                context.Veterinarias.Add(new Veterinaria { VeterinariaID = 102, Nombre = "Hospital Veterinario PetCare", Direccion = "Av. Mariana de Jesús, y 458", Telefono = "0996148523" });
            }

            context.SaveChanges();
        }
    }
}
