using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PetVet.Data;
using PetVet.Models;

namespace PetVet.Controllers
{
    public class HomeController : Controller
    {

        private static PetVetContext db = new PetVetContext();
        public JsonResult GetMyPets()
        {
            //var model = new[]
            //{
            //    new Mascota
            //    {
            //        Nombre= "Candy", Raza= "Mestiza", Edad="1", Especie="perro", Sexo="Hembra", Color="Blanco y Cafe", Esterilizado = true
            //    }

            //};

            Usuario user = new Usuario();
            List<Mascota> pets = new List<Mascota>();
            int id = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    user = db.Usuarios.Include("Mascotas").Where(i => i.UsuarioID == id).FirstOrDefault();
                    pets = (List<Mascota>)user.Mascotas;
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return Json(pets);
        }

        public JsonResult GetVeterinarias()
        {

            List<Veterinaria> vets = new List<Veterinaria>();
            string error = "error";
            try
            {
                vets = db.Veterinarias.ToList();

                return Json(vets);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes.");


            }
            return Json(error);
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

        [HttpPost]
        public ActionResult SignIn(Usuario user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Usuarios.Add(user);
                    db.SaveChanges();
                }
                    
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes.");
                return View(user);
            }

            return RedirectToAction("LogIn");
        }

        public ActionResult LogIn()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string email, string psswd)
        {
            Usuario user = new Usuario();
            try
            {
                
                user = db.Usuarios.Where(i => i.Mail == email && i.Password == psswd).FirstOrDefault();

                if (user != null)
                {
                    Session["UserInfo"] = JsonConvert.SerializeObject(user);
                    if (user.Mail.Contains("vet"))
                    {
                        var response = new { redirectUrl = Url.Action("DocVets") };
                        Session["UserType"] = "Vet";
                        return Json(response);
                    }
                    else
                    {
                        var response = new { redirectUrl = Url.Action("Pets") };
                        Session["UserType"] = "User";
                        return Json(response);
                    }
                   
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes.");
                
            }
            return View(user);
            
        }

        public ActionResult Logout()
        {
            if (Session["UserInfo"] != null || Session["UserType"] != null)
            {
                Session["UserInfo"] = null;
                Session["UserType"] = null;
                
            }

            return RedirectToAction("LogIn");

        }

        public ActionResult Pets()
        {
            List<Mascota> mascotas = new List<Mascota>();
            if (Session["UserInfo"] != null)
            {
                var user = JsonConvert.DeserializeObject<Usuario>(Session["UserInfo"].ToString());
                mascotas = db.Mascotas.Where(i => i.Usuario == user.UsuarioID).ToList();

            }
            
            return View(mascotas);
            
        }

        public ActionResult NewPet()
        {
            List<SelectListItem> files = new List<SelectListItem>();

            List<Veterinaria> vets = new List<Veterinaria>();
           
            vets = db.Veterinarias.ToList();               
          

            foreach (var vet in vets)
            {
                files.Add(new SelectListItem
                {
                    Text = vet.Nombre,
                    Value = vet.VeterinariaID.ToString()
                });
            }
            ViewBag.Vets = files;

            return View();
        }

        [HttpPost]
        public ActionResult NewPet(Mascota pet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["UserInfo"] != null)
                    {
                        var user = JsonConvert.DeserializeObject<Usuario>(Session["UserInfo"].ToString());
                        pet.Usuario = user.UsuarioID;
                    }


                    db.Mascotas.Add(pet);
                    db.SaveChanges();
                }
                else
                {
                    List<SelectListItem> files = new List<SelectListItem>();

                    List<Veterinaria> vets = new List<Veterinaria>();

                    vets = db.Veterinarias.ToList();


                    foreach (var vet in vets)
                    {
                        files.Add(new SelectListItem
                        {
                            Text = vet.Nombre,
                            Value = vet.VeterinariaID.ToString()
                        });
                    }
                    ViewBag.Vets = files;
                    return View(pet);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }

            return RedirectToAction("Pets");
        }

        public ActionResult PetProfile(int id)
        {
            Mascota pet = new Mascota();
            try
            {
                if (ModelState.IsValid)
                {
                    pet = db.Mascotas.Where(i => i.MascotaID == id).FirstOrDefault();                    
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }

            return View(pet);
            
        }

        public ActionResult UserProfile()
        {
            Usuario user = new Usuario();
            
            try
            {
                if(Session["UserInfo"] != null)
                {
                    user = JsonConvert.DeserializeObject<Usuario>(Session["UserInfo"].ToString());
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult UserProfile(Usuario user)
        {
            try
            {
                var userdb = db.Usuarios.Find(user.UsuarioID);
                if (userdb != null)
                {
                    userdb.Nombre = user.Nombre;
                    userdb.Cedula = user.Cedula;
                    userdb.Direccion = user.Direccion;
                    userdb.Telefono = user.Telefono;
                    db.SaveChanges();
                    Session["UserInfo"] = JsonConvert.SerializeObject(userdb);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return View(user);

        }

        public ActionResult DocVets()
        {
            return View();
        }

        public ActionResult UsersList()
        {
            List<Usuario> users = new List<Usuario>();
            
            int id = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    users = db.Usuarios.ToList();                    
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(users);
        }

        public ActionResult NewMed()
        {
            return View();
        }
    }
}