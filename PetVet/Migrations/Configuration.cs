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
            if (!context.Veterinarias.Any(v => v.VeterinariaID == 16))
            {
                context.Veterinarias.Add(new Veterinaria { VeterinariaID = 100, Nombre = "Clínica de Mascotas AmigoAnimal", Direccion = "Calle A" });
            }

            if (!context.Veterinarias.Any(v => v.VeterinariaID == 17))
            {
                context.Veterinarias.Add(new Veterinaria { VeterinariaID = 101, Nombre = "Clínica Veterinaria Animalia", Direccion = "Calle B" });
            }

            if (!context.Veterinarias.Any(v => v.VeterinariaID == 18))
            {
                context.Veterinarias.Add(new Veterinaria { VeterinariaID = 102, Nombre = "Hospital Veterinario PetCare", Direccion = "Calle C" });
            }

            context.SaveChanges();
        }
    }
}
