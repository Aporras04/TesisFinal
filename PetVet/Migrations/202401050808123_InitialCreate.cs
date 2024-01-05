namespace PetVet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 45, unicode: false),
                        Cedula = c.String(maxLength: 10, unicode: false),
                        Mail = c.String(maxLength: 45, unicode: false),
                    })
                .PrimaryKey(t => t.DoctorID);
            
            CreateTable(
                "dbo.Veterinarias",
                c => new
                    {
                        VeterinariaID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 45, unicode: false),
                        Direccion = c.String(maxLength: 45, unicode: false),
                        Telefono = c.String(maxLength: 10, unicode: false),
                        Doctor_DoctorID = c.Int(),
                        Mascota_MascotaID = c.Int(),
                    })
                .PrimaryKey(t => t.VeterinariaID)
                .ForeignKey("dbo.Doctors", t => t.Doctor_DoctorID)
                .ForeignKey("dbo.Mascotas", t => t.Mascota_MascotaID)
                .Index(t => t.Doctor_DoctorID)
                .Index(t => t.Mascota_MascotaID);
            
            CreateTable(
                "dbo.Mascotas",
                c => new
                    {
                        MascotaID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 45, unicode: false),
                        Especie = c.String(maxLength: 45, unicode: false),
                        Raza = c.String(maxLength: 45, unicode: false),
                        Edad = c.String(maxLength: 2, unicode: false),
                        Sexo = c.String(maxLength: 1, unicode: false),
                        Color = c.String(maxLength: 10, unicode: false),
                        Esterilizado = c.Boolean(nullable: false),
                        Usuario_UsuarioID = c.Int(),
                    })
                .PrimaryKey(t => t.MascotaID)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_UsuarioID)
                .Index(t => t.Usuario_UsuarioID);
            
            CreateTable(
                "dbo.Tratamientoes",
                c => new
                    {
                        idTratamiento = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Descripcion = c.String(maxLength: 45, unicode: false),
                        PesoMascota = c.Double(nullable: false),
                        FechaProxima = c.DateTime(nullable: false),
                        Mascota_MascotaID = c.Int(),
                    })
                .PrimaryKey(t => t.idTratamiento)
                .ForeignKey("dbo.Mascotas", t => t.Mascota_MascotaID)
                .Index(t => t.Mascota_MascotaID);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 45, unicode: false),
                        Direccion = c.String(maxLength: 60, unicode: false),
                        Cedula = c.String(maxLength: 10, unicode: false),
                        Telefono = c.String(maxLength: 10, unicode: false),
                        Mail = c.String(maxLength: 45, unicode: false),
                    })
                .PrimaryKey(t => t.UsuarioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Veterinarias", "Mascota_MascotaID", "dbo.Mascotas");
            DropForeignKey("dbo.Mascotas", "Usuario_UsuarioID", "dbo.Usuarios");
            DropForeignKey("dbo.Tratamientoes", "Mascota_MascotaID", "dbo.Mascotas");
            DropForeignKey("dbo.Veterinarias", "Doctor_DoctorID", "dbo.Doctors");
            DropIndex("dbo.Tratamientoes", new[] { "Mascota_MascotaID" });
            DropIndex("dbo.Mascotas", new[] { "Usuario_UsuarioID" });
            DropIndex("dbo.Veterinarias", new[] { "Mascota_MascotaID" });
            DropIndex("dbo.Veterinarias", new[] { "Doctor_DoctorID" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Tratamientoes");
            DropTable("dbo.Mascotas");
            DropTable("dbo.Veterinarias");
            DropTable("dbo.Doctors");
        }
    }
}
