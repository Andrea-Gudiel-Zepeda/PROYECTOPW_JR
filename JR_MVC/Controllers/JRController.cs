using JR_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics;

namespace JR_MVC.Controllers
{
    public class JRController : Controller
    {
        private readonly ILogger<JRController> _logger;

        public JRController(ILogger<JRController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Pagina_Principal()
        {
            return View();
        }

        public IActionResult error404()
        {
            return View();
        }

        public IActionResult AcercaDe()
        {
            return View();

        }

        public IActionResult Terms_Service()
        {
            return View();
        }

        public IActionResult Privacy_Policy()
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