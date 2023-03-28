using JR_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly JrDbContext _context;

        public BookController()
        {
            _context = new JrDbContext();
        }

        //OBTENER LIBROS POR USUARIO 
        [HttpGet(Name = "GetBook")]
        public string Get()
        {
            return "Hola Libros";
        }

        //CREAR LIBRO

        //EDITAR LIBRO 

        //ELIMINAR LIBRO
    }
}
