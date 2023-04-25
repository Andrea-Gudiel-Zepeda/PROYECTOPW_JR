using JR_MVC.Models;
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
        public static int IdUser = 0;

        [HttpGet]
        public async Task<IActionResult> ValidacionCredencialesL()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ValidacionCredencialesL(string email, string password)
        {
            IEnumerable<JR_DB.User> usuario = await Functions.APIServiceUser.UserGetList();
            bool encontrado = false;
            foreach (var us in usuario)
            {
                if (us.Email == email)
                {
                    if (us.Password == password)
                    {
                        IdUser = us.IdUser;
                        encontrado = true;
                        break;
                    }
                    else
                    {
                        ViewBag.error = "La contraseña es incorrecta, ingrese de nuevo";
                        return View();
                    }
                }
                else
                {
                    ViewBag.error = "No se encontro el usuario, intente de nuevo para poder continuar";
                    return View();
                }
            }

            if (encontrado)
            {
                return RedirectToAction("CreateBook_Read", "Book");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ValidacionCredencialesP()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ValidacionCredencialesP(string email, string password)
        {
            IEnumerable<JR_DB.User> usuario = await Functions.APIServiceUser.UserGetList();
            bool encontrado = false;
            foreach (var us in usuario)
            {
                if (us.Email == email)
                {
                    if (us.Password == password)
                    {
                        IdUser = us.IdUser;
                        encontrado = true;
                        break;
                    }
                    else
                    {
                        ViewBag.error = "La contraseña es incorrecta, ingrese de nuevo";
                        return View();
                    }
                }
                else
                {
                    ViewBag.error = "No se encontro el usuario, intente de nuevo para poder continuar";
                    return View();
                }
            }

            if (encontrado)
            {
                return RedirectToAction("CreateBook_ToDo", "Book");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ValidacionCredencialesB()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ValidacionCredencialesB(string email, string password)
        {
            IEnumerable<JR_DB.User> usuario = await Functions.APIServiceUser.UserGetList();
            bool encontrado = false;
            foreach (var us in usuario)
            {
                if (us.Email == email)
                {
                    if (us.Password == password)
                    {
                        IdUser = us.IdUser;
                        encontrado = true;
                        break;
                    }
                    else
                    {
                        ViewBag.error = "La contraseña es incorrecta, ingrese de nuevo";
                        return View();
                    }
                }
                else
                {
                    ViewBag.error = "No se encontro el usuario, intente de nuevo para poder continuar";
                    return View();
                }
            }

            if (encontrado)
            {
                return RedirectToAction("CreateBook_Buy", "Book");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Read_List()
        {
            IEnumerable<JR_DB.Book> books = await Functions.APIServiceBook.BookGetList();
            List<JR_DB.Book> books_l = new List<JR_DB.Book>();

            foreach (var bk in books)
            {
                if(bk.IdCategorie == 0)
                {
                    books_l.Add(bk);
                }
            }

            return View(books_l);
        }

        //obtener libros pendientes
        [HttpGet]
        public async Task<IActionResult> ToDo_List()
        {
            IEnumerable<JR_DB.Book> books = await Functions.APIServiceBook.BookGetList();
            List<JR_DB.Book> books_p = new List<JR_DB.Book>();

            foreach (var bk in books)
            {
                if (bk.IdCategorie == 2)
                {
                    books_p.Add(bk);
                }
            }

            return View(books_p);
        }

        //Obtener listado libros por comprar
        [HttpGet]
        public async Task<IActionResult> Buy_List()
        {
            IEnumerable<JR_DB.Book> books = await Functions.APIServiceBook.BookGetList();
            List<JR_DB.Book> books_b = new List<JR_DB.Book>();

            foreach (var bk in books)
            {
                if (bk.IdCategorie == 2)
                {
                    books_b.Add(bk);
                }
            }

            return View(books_b);
        }

        [HttpGet]
        public async Task<IActionResult> CreateBook_Read()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook_Read([Bind("IdBook,NameBook,AuthorBook,BookPublish,DateBook,Calificacion,PictureBook,Imagen")] Book book, IFormCollection collection)
        {
            //validar calificacion
            bool calificacion = await Functions.APIServiceCalificacion.GetCalificacionByID(IdUser, book.Calificacion);
            if (calificacion)
            {
                //validar imagen
                var imagen = collection["Imagen"];
                byte[] bytes = Encoding.UTF8.GetBytes(imagen);

                book.PictureBook = bytes;
                book.IdCategorie = 0;
                book.IdUser = IdUser;
                if (ModelState.IsValid)
                {
                    await Functions.APIServiceBook.BookSet(book);
                    //falta el mensaje y direccionar 
                    ViewBag.BookCreate = "Libro Ingresado correctamente";
                    return RedirectToAction(nameof(CreateBook_Read));
                }
            }
            
            //_jrContext.Books.Add(book);
            //_jrContext.SaveChanges();

            return View();
        }

        public IActionResult CreateBook_Buy()
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}