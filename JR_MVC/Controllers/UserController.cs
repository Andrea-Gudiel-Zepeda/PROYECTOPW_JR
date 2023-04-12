using AspNetCore;
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

        [HttpGet]
        public IActionResult SingIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SingIn([Bind("IdUser,FullName,LastName,Email,NumberPhone,Password")] User usuario)
        {
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
        public IActionResult SingUp([Bind("IdUser,FullName,LastName,Email,NumberPhone,Password")] User usuario)
        {
           
            return RedirectToAction(nameof(SingUp));
        }

        public IActionResult Recoverpw()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}