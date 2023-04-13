using JR_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Diagnostics;

namespace JR_MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        //Muestra la página para iniciar sesión
        [HttpGet]
        public IActionResult SingIn()
        {
            return View();
        }

        //Obtiene los datos para validar e ingresar a la pagina principal
        [HttpPost]
        public IActionResult SingIn([Bind("IdUser,FullName,LastName,Email,NumberPhone,Password")] User usuario)
        {
            return RedirectToAction("Pagina_Principal", "JR");
        }

        //Muestra la página para crear un usuario nuevo
        [HttpGet]
        public IActionResult SingUp()
        {
            return View();
        }

        //Recibe los datos para crear el usuario nuevo y retornar la misma página con mensaje
        [HttpPost]
        public IActionResult SingUp([Bind("IdUser,FullName,LastName,Email,NumberPhone,Password")] User usuario)
        {
           
            return RedirectToAction(nameof(SingUp));
        }

        //Muestra primera página para cambiar contraseña
        [HttpGet]
        public IActionResult Recoverpw()
        {
            return View();
        }

        //Recibe el email 
        [HttpPost]
        public IActionResult Recoverpw(IFormCollection collection)
        {
            string email = collection["Email"];
            return RedirectToAction(nameof(Recoverpw2));
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
            string email = collection["Password"];

            ViewBag.NewRpw = "La contraseña ha sido modificada exitosamente";
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