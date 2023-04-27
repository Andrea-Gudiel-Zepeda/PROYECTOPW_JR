using JR_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics;

namespace JR_MVC.Controllers
{
    public class ReseñaController : Controller
    {
        private readonly ILogger<ReseñaController> _logger;

        public ReseñaController(ILogger<ReseñaController> logger)
        {
            _logger = logger;
        }

        //Obtener reseñas
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ZonaReseñas()
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);

            JR_DB.Tokens token = await Functions.APIServiceUser.Login(
            new JR_DB.Tokens
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }
            IEnumerable<JR_DB.Reseña> reseñas = await Functions.APIServiceReseña.ReseñaGetList(token.token);
            List<JR_DB.Reseña> reseñas_ls = new List<JR_DB.Reseña>();

            foreach (var rs in reseñas)
            {
                if (rs.IdUser == idUsuario)
                {
                    reseñas_ls.Add(rs);
                }
            }
            return View(reseñas_ls);
        }

        //Crear libro
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateReseña()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateReseña([Bind("IdReseña,NameBook,AuthorBook,GeneroBook,PagesBook,PuntuacionBook,PuntuacionTrama,PuntuacionPersonajes,DescriptionReseña,FavoritePhrase")] JR_DB.Reseña reseña)
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            bool calificacionEncontrada = false;
            //validar calificacion

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
            foreach (var cl in calificaciones)
            {
                if (cl.IdUser == idUsuario)
                {
                    if (reseña.PuntuacionBook >= cl.LimiteInferior && reseña.PuntuacionBook <= cl.LimiteSuperior)
                    {
                        if (reseña.PuntuacionTrama >= cl.LimiteInferior && reseña.PuntuacionTrama <= cl.LimiteSuperior)
                        {
                            if (reseña.PuntuacionPersonajes >= cl.LimiteInferior && reseña.PuntuacionPersonajes <= cl.LimiteSuperior)
                            {
                                reseña.IdUser = idUsuario;
                                if (ModelState.IsValid)
                                {
                                    await Functions.APIServiceReseña.ReseñaSet(reseña,token.token);
                                    return RedirectToAction(nameof(ZonaReseñas));
                                }
                            }
                            else
                            {
                                calificacionEncontrada = true;
                                ViewBag.BookCreate = "La calificacion de los personajes no esta entre el rango creado";
                            }
                        }
                        else
                        {
                            calificacionEncontrada = true;
                            ViewBag.BookCreate = "La calificacion de la trama no esta entre el rango creado";
                        }


                    }
                    else
                    {
                        calificacionEncontrada = true;
                        ViewBag.BookCreate = "La calificacion del libro no esta entre el rango creado";
                        return View();
                    }
                }
            }

            if (!calificacionEncontrada)
            {
                ViewBag.BookCreate = "No existe un rango de calificacion configurada";
                return View();
            }

            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditReseña(int id)
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

            JR_DB.Reseña reseña = await Functions.APIServiceReseña.GetReseñaByID(id, token.token);

            return View(reseña);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditReseña(int id, [Bind("IdReseña,NameBook,AuthorBook,GeneroBook,PagesBook,PuntuacionBook,PuntuacionTrama,PuntuacionPersonajes,DescriptionReseña,FavoritePhrase")] JR_DB.Reseña reseña)
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            if (id != reseña.IdReseña)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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

                await Functions.APIServiceReseña.ReseñaEdit(reseña, id, token.token);
                return RedirectToAction(nameof(ZonaReseñas));

            }
            return View(reseña);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteReseña(int id)
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

            JR_DB.Reseña reseña = await Functions.APIServiceReseña.GetReseñaByID(id,token.token);


            return View(reseña);
        }


        [HttpPost, ActionName("DeleteReseña")]
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
                await Functions.APIServiceReseña.ReseñaDelete(id, token.token);
            }


            return RedirectToAction(nameof(ZonaReseñas));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}