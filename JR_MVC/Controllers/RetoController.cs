using Google.Protobuf.WellKnownTypes;
using JR_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics;

namespace JR_MVC.Controllers
{
    public class RetoController : Controller
    {
        private readonly ILogger<RetoController> _logger;

        public RetoController(ILogger<RetoController> logger)
        {
            _logger = logger;
        }

        //Obtener 
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowReto()
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
            IEnumerable<JR_DB.Reto> retos = await Functions.APIServiceReto.RetoGetList(token.token);
            List<JR_DB.Reto> retos_ls = new List<JR_DB.Reto>();

            foreach (var rt in retos)
            {
                if (rt.IdUser == idUsuario)
                {
                    retos_ls.Add(rt);
                }
            }
            return View(retos_ls);
        }


        [HttpGet]
        [Authorize]
        //Obtener reto aleatorio
        public async Task<IActionResult> ObtenerReto()
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            RetosAleatorios metodoretos = new RetosAleatorios();
            string reto = metodoretos.TuReto();
            JR_DB.Reto retoaleatorio = new JR_DB.Reto();
            retoaleatorio.NombreReto = reto;
            retoaleatorio.IdUser = idUsuario;
            retoaleatorio.DateStart = DateTime.Today.Date;
            retoaleatorio.Status = "En Proceso";
            JR_DB.Tokens token = await Functions.APIServiceUser.Login(
                new JR_DB.Tokens
                {
                    token = "asdkhfalskdjfhas"
                });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
                  {
                      await Functions.APIServiceReto.RetoSet(retoaleatorio,token.token);

                  }
            return View(retoaleatorio);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditReto(int id)
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
            JR_DB.Reto reto = await Functions.APIServiceReto.GetRetoByID(id, token.token);

            return View(reto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditReto(int id, [Bind("IdReto,NombreReto,Status,DateStart,DateEnd,IdUser")] JR_DB.Reto reto)
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            if (id != reto.IdReto)
            {
                return NotFound();
            }

            JR_DB.Tokens token = await Functions.APIServiceUser.Login(
                new JR_DB.Tokens
                {
                    token = "asdkhfalskdjfhas"
                });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await Functions.APIServiceReto.RetoEdit(reto, id, token.token);
                return RedirectToAction(nameof(ShowReto));

            }
            return View(reto);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteReto(int id)
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
            JR_DB.Reto reto = await Functions.APIServiceReto.GetRetoByID(id, token.token);


            return View(reto);
        }


        [HttpPost, ActionName("DeleteReto")]
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
                await Functions.APIServiceReto.RetoDelete(id, token.token);
            }


            return RedirectToAction(nameof(ShowReto));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}