using Microsoft.AspNetCore.Mvc;
using Reading_Journal.Models;
using System.Collections;
using System.Diagnostics;

namespace Reading_Journal.Controllers
{
    public class JRController : Controller
    {
        private readonly ILogger<JRController> _logger;

        public JRController(ILogger<JRController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Pagina_Principal()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Pagina_Principal(IFormCollection collection)
        {
            return View();
        }

        [HttpGet]
        public IActionResult SingIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SingIn(IFormCollection collection)
        {
            ViewBag.Id = "1";
            return View();
        }

        [HttpGet]
        public IActionResult SingUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SingUp(IFormCollection collection)
        {
               
            return View();
        }


        public IActionResult Recoverpw()
        {
            return View();
        }

        public IActionResult User_Profile()
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

        public IActionResult Read_List()
        {
            return View();
        }

        public IActionResult Buy_List()
        {
            return View();
        }

        public IActionResult ToDo_List()
        {
            return View();
        }

        public IActionResult CreateBook_Read()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBook_Read(IFormCollection collection)
        {
            int id = Convert.ToInt32(ViewBag.Id);
            JrDbContext _jrContext = new JrDbContext();
            Models.Book book = new Models.Book
            {
                IdBook = 0,
                NameBook = collection["NombreLibro"],
                AuthorBook = collection["NombreAutor"],
                //BookPublish = collection["publicacion"],
                DateBook = Convert.ToDateTime(collection["FechaLeido"]),
                IdCategorie = 1,
                IdUser = id
            };

            //_jrContext.Books.Add(book);
            //_jrContext.SaveChanges();

            return View();
        }

        public IActionResult CreateBook_Buy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBook(Book book)
        {
            return View();
        }

        public IActionResult CreateBook_ToDo()
        {
            return View();
        }

        public IActionResult UpdateBook()
        {
            return View();
        }

        public IActionResult ZonaReseñas()
        {
            return View();
        }

        public IActionResult CreateReseña()
        {
            return View();
        }

        public IActionResult UpdateReseña()
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