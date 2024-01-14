using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                else
                {
                    return View(user);
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
                
                user = db.Usuarios.Include(u => u.Mascotas).FirstOrDefault(i => i.Mail == email && i.Password == psswd);

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
                //mascotas = db.Mascotas.Where(i => i.Usuario == user.UsuarioID).ToList();
                mascotas = (List<Mascota>)user.Mascotas;
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
                        var userInfo = JsonConvert.DeserializeObject<Usuario>(Session["UserInfo"].ToString());

                        Usuario usuario = db.Usuarios.Include(u => u.Mascotas).FirstOrDefault(u => u.UsuarioID == userInfo.UsuarioID);


                        if (usuario != null)
                        {
                            pet.CedulaUsuario = userInfo.Cedula;
                            pet.NombreUsuario = userInfo.Nombre;
                            usuario.Mascotas.Add(pet);
                            db.SaveChanges();
                            Session["UserInfo"] = JsonConvert.SerializeObject(usuario);
                        }
                       
                        //pet.Usuario = user.UsuarioID;                        
                    }
                    //db.Mascotas.Add(pet);
                    //db.SaveChanges();
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

                    Session["PetInfo"] = JsonConvert.SerializeObject(pet);
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
            List<Veterinaria> veterinarias = new List<Veterinaria>();
            try
            {
                if (Session["UserType"] != null && Session["UserType"].ToString() == "Vet")
                {
                    veterinarias = db.Veterinarias.ToList();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return View(veterinarias);
        }

        public ActionResult UsersList(int id)
        {
            List<Mascota> pets = new List<Mascota>();
            Veterinaria vet =  new Veterinaria();
            try
            {
                vet = db.Veterinarias.Where(i => i.VeterinariaID == id).FirstOrDefault();
                ViewBag.VetName = vet.Nombre;
                pets = db.Mascotas.Where(i => i.Veterinaria == id).ToList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(pets);
        }

        public ActionResult NewMed()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult NewMed(Tratamiento tratamiento)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["PetInfo"] != null)
                    {
                        //var userInfo = JsonConvert.DeserializeObject<Usuario>(Session["UserInfo"].ToString());
                        var petInfo = JsonConvert.DeserializeObject<Mascota>(Session["PetInfo"].ToString());

                        //Usuario usuario = db.Usuarios.Include(u => u.Mascotas).FirstOrDefault(u => u.UsuarioID == userInfo.UsuarioID);
                        //Usuario usuario = db.Usuarios.Include(u => u.Mascotas.Select(m => m.Tratamientos)) .FirstOrDefault(u => u.UsuarioID == userInfo.UsuarioID);

                        Mascota mascota = db.Mascotas.Include(u => u.Tratamientos).FirstOrDefault(i => i.MascotaID == petInfo.MascotaID);
                        if (mascota != null)
                        {
                            tratamiento.Mascota = petInfo.MascotaID;
                            mascota.Tratamientos.Add(tratamiento);
                            db.SaveChanges();                            
                        }
                        //tratamiento.Mascota = petInfo.MascotaID;
                        //usuario.Mascotas.Where(i => i.MascotaID == tratamiento.Mascota).FirstOrDefault().Tratamientos.Add(tratamiento);
                        //db.SaveChanges();
                        //Session["UserInfo"] = JsonConvert.SerializeObject(usuario);

                        return RedirectToAction("PetProfile", new { id = mascota.MascotaID });

                        //pet.Usuario = user.UsuarioID;                        
                    }
                    //db.Mascotas.Add(pet);
                    //db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View(tratamiento);
        }
    }
}