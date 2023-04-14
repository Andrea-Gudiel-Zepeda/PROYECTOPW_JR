using JR_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Diagnostics;
using JR_MVC.Functions;

namespace JR_MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        static int idUser;

        //Muestra la página para iniciar sesión
        [HttpGet]
        public IActionResult SingIn()
        {
            return View();
        }

        //Obtiene los datos para validar e ingresar a la pagina principal
        [HttpPost]
        public async Task<IActionResult> SingIn(string email, string password)
        {
            bool contraseñaIncorrecta = true;
            bool emailIncorrecto = true;

            IEnumerable<JR_DB.User> usuario = await Functions.APIService.UserGetList();

            foreach (var us in usuario)
            {
                if (us.Email == email)
                {
                    if (us.Password == password)
                    {
                        contraseñaIncorrecta = false;
                        emailIncorrecto = false;
                        idUser = us.IdUser;
                        break;
                    }
                    else
                    {
                        contraseñaIncorrecta &= true;
                        emailIncorrecto = false;
                    }
                }
                else
                {
                    emailIncorrecto &= true;
                }
            }

            if (!emailIncorrecto)
            {
                if (!contraseñaIncorrecta)
                {
                    return RedirectToAction("Pagina_Principal", "JR");
                }
                else
                {
                    ViewBag.Credenciales = "La contraseña ingresa es incorrecta";
                }

            }
            else
            {
                ViewBag.Credenciales = "No se encontró ningún usuario registrado con este correo";
            }

            return View();
        }

        //Muestra la página para crear un usuario nuevo
        [HttpGet]
        public IActionResult SingUp()
        {
            return View();
        }

        //Recibe los datos para crear el usuario nuevo y retornar la misma página con mensaje
        [HttpPost]
        public async Task<IActionResult> SingUp([Bind("IdUser,FullName,LastName,Email,NumberPhone,Password")] User usuario)
        {
            if (ModelState.IsValid)
            {
                await Functions.APIService.UserSet(usuario);
                //falta el mensaje y direccionar 
                return RedirectToAction(nameof(SingUp));
            }

            return View(usuario);
        }

        //Muestra primera página para cambiar contraseña
        [HttpGet]
        public IActionResult Recoverpw()
        {
            return View();
        }

        //Recibe el email 
        [HttpPost]
        public async Task<IActionResult> Recoverpw(IFormCollection collection)
        {
            string email = collection["Email"];
            bool emailCorrecto = false;

            IEnumerable<JR_DB.User> usuario = await Functions.APIService.UserGetList();

            foreach (var us in usuario)
            {
                if (us.Email == email)
                {
                   emailCorrecto = true;
                   idUser = us.IdUser;
                   break;
                }
                else
                {
                    emailCorrecto &= false;
                }
            }

            if (emailCorrecto)
            {
                return RedirectToAction(nameof(Recoverpw2));
            }
            else
            {
                ViewBag.Credenciales = "No se encontró ningún usuario registrado con este correo";
            }

            return View();
            
        }

        //Muestra la segunda página para cambiar contraseña
        [HttpGet]
        public IActionResult Recoverpw2()
        {
            return View();
        }

        //Solicita la nueva contraseña
        [HttpPost]
        public IActionResult Recoverpw2(IFormCollection collection)
        {
            string newPassword = collection["Password"];
            int id = idUser;
            //Falta hacer el update y el mensaje
            return RedirectToAction(nameof(SingIn));
        }

        //Muestra el perfil de usuario 
        [HttpGet]
        public IActionResult User_Profile()
        {
            return View();
        }
        //Recibe los nuevos valores para editar la informacion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}