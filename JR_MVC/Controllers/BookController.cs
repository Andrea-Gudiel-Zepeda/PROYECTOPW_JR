using JR_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Web;


namespace JR_MVC.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }

        //Obtener libro leidos = 1
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Read_List()
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            int idCategorie = 1;
            IEnumerable<JR_DB.Book> books = await Functions.APIServiceBook.GetListByCategorie(idCategorie);
            List<JR_DB.Book> books_ls = new List<JR_DB.Book>();

            foreach(var bk in books)
            {
                if(bk.IdUser == idUsuario)
                {
                    books_ls.Add(bk);
                }
            }
            return View(books_ls);
        }

        //Crear libro
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateBook_Read()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBook_Read([Bind("IdBook,NameBook,AuthorBook,BookPublish,DateBook,Calificacion")] JR_DB.Book book)
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            bool calificacionEncontrada = false;
            //validar calificacion
            IEnumerable<JR_DB.Calificacion> calificaciones = await Functions.APIServiceCalificacion.CalificacionGetList();
            foreach (var cl in calificaciones)
            {
                if (cl.IdUser == idUsuario)
                {
                    if(book.Calificacion >= cl.LimiteInferior && book.Calificacion <= cl.LimiteSuperior)
                    {

                        book.IdCategorie = 1;
                        book.IdUser = idUsuario;
                        if (ModelState.IsValid)
                        {
                            await Functions.APIServiceBook.BookSet(book);
                            return RedirectToAction(nameof(Read_List));
                        }
                    }
                    else
                    {
                        calificacionEncontrada = true;
                        ViewBag.BookCreate = "La calificacion no esta entre el rango creado";
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
        public async Task<IActionResult> EditBookR(int id)
        {
            JR_DB.Book book = await Functions.APIServiceBook.GetBookByID(id);

            return View(book);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditBookR(int id, [Bind("IdBook,NameBook,AuthorBook,BookPublish,DateBook,Calificacion,IdCategorie,IdUser")] JR_DB.Book book)
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            if (id != book.IdBook)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await Functions.APIServiceBook.BookEdit(book, id);
                return RedirectToAction(nameof(Read_List));
                
            }
            return View(book);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteBookR(int id)
        {

            JR_DB.Book book = await Functions.APIServiceBook.GetBookByID(id);


            return View(book);
        }


        [HttpPost, ActionName("DeleteBookR")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmedR(int id)
        {
            if (id != 0)
            {
                await Functions.APIServiceBook.BookDelete(id);
            }


            return RedirectToAction(nameof(Read_List));
        }


        //obtener libros pendientes = 2
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ToDo_List()
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            int idCategorie = 2;
            IEnumerable<JR_DB.Book> books = await Functions.APIServiceBook.GetListByCategorie(idCategorie);
            List<JR_DB.Book> books_ls = new List<JR_DB.Book>();

            foreach (var bk in books)
            {
                if (bk.IdUser == idUsuario)
                {
                    books_ls.Add(bk);
                }
            }
            return View(books_ls);
        }

        //Crear libro pendientes
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateBook_ToDo()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBook_ToDo([Bind("IdBook,NameBook,AuthorBook,BookPublish,DateBook,Calificacion")] JR_DB.Book book)
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
                        book.Calificacion = 0;
                        book.IdCategorie = 2;
                        book.IdUser = idUsuario;
                        if (ModelState.IsValid)
                        {
                            await Functions.APIServiceBook.BookSet(book);
                            return RedirectToAction(nameof(ToDo_List));
                        }
                    
                
            
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditBookP(int id)
        {
            JR_DB.Book book = await Functions.APIServiceBook.GetBookByID(id);

            return View(book);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditBookP(int id, [Bind("IdBook,NameBook,AuthorBook,BookPublish,DateBook,Calificacion,IdCategorie,IdUser")] JR_DB.Book book)
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            if (id != book.IdBook)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await Functions.APIServiceBook.BookEdit(book, id);
                return RedirectToAction(nameof(ToDo_List));

            }
            return View(book);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteBookP(int id)
        {

            JR_DB.Book book = await Functions.APIServiceBook.GetBookByID(id);


            return View(book);
        }


        [HttpPost, ActionName("DeleteBookP")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmedP(int id)
        {
            if (id != 0)
            {
                await Functions.APIServiceBook.BookDelete(id);
            }


            return RedirectToAction(nameof(ToDo_List));
        }

        //Obtener listado libros por comprar
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Buy_List()
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            int idCategorie = 3;
            IEnumerable<JR_DB.Book> books = await Functions.APIServiceBook.GetListByCategorie(idCategorie);
            List<JR_DB.Book> books_ls = new List<JR_DB.Book>();

            foreach (var bk in books)
            {
                if (bk.IdUser == idUsuario)
                {
                    books_ls.Add(bk);
                }
            }
            return View(books_ls);
        }

        //Crear libro
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateBook_Buy()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBook_Buy([Bind("IdBook,NameBook,AuthorBook,BookPublish,DateBook,Calificacion")] JR_DB.Book book)
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            
               book.Calificacion = 0;
                        book.IdCategorie = 3;
                        book.IdUser = idUsuario;
                        if (ModelState.IsValid)
                        {
                            await Functions.APIServiceBook.BookSet(book);
                            return RedirectToAction(nameof(Buy_List));
                        }
                  

            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditBookB(int id)
        {
            JR_DB.Book book = await Functions.APIServiceBook.GetBookByID(id);

            return View(book);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditBookB(int id, [Bind("IdBook,NameBook,AuthorBook,BookPublish,DateBook,Calificacion,IdCategorie,IdUser")] JR_DB.Book book)
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            if (id != book.IdBook)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await Functions.APIServiceBook.BookEdit(book, id);
                return RedirectToAction(nameof(Buy_List));

            }
            return View(book);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteBookB(int id)
        {

            JR_DB.Book book = await Functions.APIServiceBook.GetBookByID(id);


            return View(book);
        }


        [HttpPost, ActionName("DeleteBookB")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmedB(int id)
        {
            if (id != 0)
            {
                await Functions.APIServiceBook.BookDelete(id);
            }


            return RedirectToAction(nameof(Buy_List));
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}