using JR_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics;

namespace JR_MVC.Controllers
{
    public class CalificacionController : Controller
    {
        private readonly ILogger<CalificacionController> _logger;

        public CalificacionController(ILogger<CalificacionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowCalificacion()
        {

            JR_DB.Tokens token = await Functions.APIServiceUser.Login(
            new JR_DB.Tokens
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            IEnumerable<JR_DB.Calificacion> calificaciones = await Functions.APIServiceCalificacion.CalificacionGetList(token.token);
            
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            
            JR_DB.Calificacion calificacionUser = new JR_DB.Calificacion();
            calificacionUser.LimiteInferior = 0;
            calificacionUser.LimiteSuperior = 0;
            bool encontrado = false;

            foreach (var cf in calificaciones)
            {
                if (cf.IdUser == idUsuario)
                {
                    calificacionUser = cf;
                    encontrado = true;
                    break;
                }
            }

            if (encontrado)
            {
                return View(calificacionUser);
            }
            else
            {
                ViewBag.NoCalificacion = "No ha ingresado ninguna calificacion con este usuario";
                return View(calificacionUser);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateCalificacion()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCalificacion([Bind("IdCalificacion, LimiteInferior, LimiteSuperior, IdUser")] JR_DB.Calificacion calificacion)
        {

            JR_DB.Tokens token = await Functions.APIServiceUser.Login(
            new JR_DB.Tokens
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }
            IEnumerable<JR_DB.Calificacion> calificaciones = await Functions.APIServiceCalificacion.CalificacionGetList(token.token);
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            bool encontrado = false;

            foreach (var cf in calificaciones)
            {
                if (cf.IdUser == idUsuario)
                {
                    encontrado = true;
                    break;
                }
            }

            if (encontrado)
            {
                ViewBag.CalificacionCreate = "Ya existe una calificacion creada con este usuario";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    calificacion.IdUser = idUsuario;
                    await Functions.APIServiceCalificacion.CalificacionSet(calificacion, token.token);
                    return RedirectToAction("ShowCalificacion", "Calificacion");
                }
            }

            return View();
        }

        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditCalificacion(int id)
        {

            JR_DB.Tokens token = await Functions.APIServiceUser.Login(
            new JR_DB.Tokens
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            JR_DB.Calificacion calificacion = await Functions.APIServiceCalificacion.GetCalificacionByID(id, token.token);

            return View(calificacion);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditCalificacion(int id, [Bind("IdCalificacion, LimiteInferior, LimiteSuperior, IdUser")] JR_DB.Calificacion calificacion)
        {

            JR_DB.Tokens token = await Functions.APIServiceUser.Login(
            new JR_DB.Tokens
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            if (id != calificacion.IdCalificacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                calificacion.IdCalificacion = id;
                calificacion.IdUser = idUsuario;
                await Functions.APIServiceCalificacion.CalificacionEdit(calificacion, id, token.token);

                return RedirectToAction(nameof(ShowCalificacion));
            }
            return View(calificacion);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteCalificacion(int id)
        {

            JR_DB.Tokens token = await Functions.APIServiceUser.Login(
            new JR_DB.Tokens
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            JR_DB.Calificacion calificacion = await Functions.APIServiceCalificacion.GetCalificacionByID(id, token.token);

            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }


        [HttpPost, ActionName("DeleteGoal")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            JR_DB.Tokens token = await Functions.APIServiceUser.Login(
            new JR_DB.Tokens
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            if (id != 0)
            {
                await Functions.APIServiceCalificacion.CalificacionDelete(id, token.token);
            }


            return RedirectToAction(nameof(ShowCalificacion));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}