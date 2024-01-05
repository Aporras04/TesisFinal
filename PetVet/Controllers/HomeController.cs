using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetVet.Models;

namespace PetVet.Controllers
{
    public class HomeController : Controller
    {
        public JsonResult GetMyPets()
        {
            var model = new[]
            {
                new Mascota
                {
                    Nombre= "Candy", Raza= "Mestiza", Edad="1 ano", Especie="perro", Sexo="Hembra", Color="Blanco y Cafe", Esterilizado = true, Veterinaria = "Veterinaria 1"
                }

            };
            return Json(model);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult LogIn()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Pets()
        {
            return View();
        }

        public ActionResult NewPet()
        {
            return View();
        }

        public ActionResult PetProfile()
        {
            return View();
        }
    }
}