using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetVet.Data;
using PetVet.Models;

namespace PetVet.Controllers
{
    public class HomeController : Controller
    {

        private static PetVetContext db = new PetVetContext();
        public JsonResult GetMyPets()
        {
            var model = new[]
            {
                new Mascota
                {
                    Nombre= "Candy", Raza= "Mestiza", Edad="1", Especie="perro", Sexo="Hembra", Color="Blanco y Cafe", Esterilizado = true
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

        public ActionResult UserProfile()
        {
            return View();
        }

        public ActionResult DocVets()
        {
            return View();
        }

        public ActionResult UsersList()
        {
            return View();
        }
    }
}