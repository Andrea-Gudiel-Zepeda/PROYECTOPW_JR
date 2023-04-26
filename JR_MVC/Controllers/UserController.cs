using JR_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Diagnostics;
using JR_MVC.Functions;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> SingIn(string email, string password)
        {
            
            IEnumerable<JR_DB.User> usuario = await Functions.APIServiceUser.UserGetList();

            foreach (var us in usuario)
            {
                if (us.Email == email)
                {
                    if (us.Password == password)
                    {
                        ViewBag.idUser = us.IdUser;

                        var claims = new List<Claim>();
                        claims.Add(new Claim("idUser", Convert.ToString(us.IdUser)));
                        //claims.Add(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(us.IdUser)));
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        await HttpContext.SignInAsync(claimsPrincipal);
                        return RedirectToAction("Pagina_Principal", "JR");

                    }
                    else
                    {
                        ViewBag.Credenciales = "La contraseña ingresada es incorrecta";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Credenciales = "No se encontró ningún usuario registrado con este correo";
                    return View();
                }
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
        public async Task<IActionResult> SingUp([Bind("IdUser,FullName,LastName,Email,NumberPhone,Password")] JR_DB.User usuario)
        {
            IEnumerable<JR_DB.User> usuarioslist = await Functions.APIServiceUser.UserGetList();

            foreach (var us in usuarioslist)
            {
                if (us.Email == usuario.Email)
                {
                    ViewBag.Credenciales = "Ya existe un usuario creado con esos datos";
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        await Functions.APIServiceUser.UserSet(usuario);
                        return RedirectToAction(nameof(SingUp));
                    }

                }
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
            string newPassword = collection["Password"];
            bool emailCorrecto = false;
            JR_DB.User NewUser = new JR_DB.User();

            IEnumerable<JR_DB.User> usuario = await Functions.APIServiceUser.UserGetList();

            foreach (var us in usuario)
            {
                if (us.Email == email)
                {
                    emailCorrecto = true;
                    NewUser = us;
                    break;
                }
                else
                {
                    emailCorrecto &= false;
                }
            }

            if (emailCorrecto)
            {
                //cambiar la contraseña
                NewUser.Password = newPassword;
                await Functions.APIServiceUser.UserEdit(NewUser, NewUser.IdUser);
                return RedirectToAction(nameof(SingIn));
            }
            else
            {
                ViewBag.Credenciales = "No se encontró ningún usuario registrado con este correo";
            }

            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(SingIn));
        } 

        //Muestra el perfil de usuario 
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> User_Profile()
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            JR_DB.User user = await Functions.APIServiceUser.GetUserByID(idUsuario);
            
            return View(user);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditUser(int id)
        {
            JR_DB.User user = await Functions.APIServiceUser.GetUserByID(id);

            return View(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditUser(int id, [Bind("IdUser,FullName,LastName,Email,NumberPhone,Password")] JR_DB.User usuario)
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            if (id != usuario.IdUser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await Functions.APIServiceUser.UserEdit(usuario, id);

                return RedirectToAction(nameof(User_Profile));
            }
            return View(usuario);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {

            JR_DB.User usuario = await Functions.APIServiceUser.GetUserByID(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }


        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id != 0)
            {
                await Functions.APIServiceUser.UserDelete(id);
            }


            return RedirectToAction(nameof(SingIn));
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}