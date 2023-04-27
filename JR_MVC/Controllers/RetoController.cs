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
            IEnumerable<JR_DB.Reto> retos = await Functions.APIServiceReto.RetoGetList();
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
            retoaleatorio.IdReto = 0;
            retoaleatorio.NombreReto = reto;
            retoaleatorio.IdUser = idUsuario;
            return View(retoaleatorio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ObtenerReto(string reto)
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            //validar calificacion
           /* reto.IdUser = idUsuario;
            reto.DateStart = Convert.ToDateTime(DateTime.Now.Date);
            reto.Status = "En Proceso";

                 if (ModelState.IsValid)
                 {
                     await Functions.APIServiceReto.RetoSet(reto);
                    
                 }*/
            
            return RedirectToAction(nameof(ShowReto));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditReto(int id)
        {
            JR_DB.Reto reto = await Functions.APIServiceReto.GetRetoByID(id);

            return View(reto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditReseña(int id, [Bind("IdReto,NombreReto,Status,DateStart,DateEnd,IdUser")] JR_DB.Reto reto)
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            if (id != reto.IdReto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await Functions.APIServiceReto.RetoEdit(reto, id);
                return RedirectToAction(nameof(ShowReto));

            }
            return View(reto);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteReto(int id)
        {

            JR_DB.Reto reto = await Functions.APIServiceReto.GetRetoByID(id);


            return View(reto);
        }


        [HttpPost, ActionName("DeleteReto")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id != 0)
            {
                await Functions.APIServiceReto.RetoDelete(id);
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