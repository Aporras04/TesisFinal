using System;
using PetVet.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Data.Entity;

namespace PetVet.Data
{
    public class PetVetContext : DbContext
    {
        public PetVetContext() : base("PetVetDB")
        {
            //base.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new PetVetDbContextInitializer());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PetVetContext, Migrations.Configuration>());

            
        }

        // DbSet para cada entidad en tu modelo
        public DbSet<Doctor> Doctores { get; set; }
        public DbSet<Veterinaria> Veterinarias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Tratamiento> Tratamientos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().ToTable("Doctor");
            modelBuilder.Entity<Veterinaria>().ToTable("Veterinaria");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Mascota>().ToTable("Mascota");
            modelBuilder.Entity<Tratamiento>().ToTable("Tratamiento");
            base.OnModelCreating(modelBuilder);
        }
    }

    public class PetVetDbContextInitializer : CreateDatabaseIfNotExists<PetVetContext>
    {
        protected override void Seed(PetVetContext context)
        {
            // Puedes realizar operaciones de inicialización si es necesario
            base.Seed(context);
        }
    }
}