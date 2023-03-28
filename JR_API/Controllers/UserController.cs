using JR_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly JrDbContext _context;

        public UserController()
        {
            _context = new JrDbContext();
        }

        //OBTENER INFORMACION DE USUARIO
        [HttpGet(Name = "GetUser")]
        public string Get()
        {
            return "Hola";
        }

        //CREAR USUARIO 

        //EDITAR USUARIO 

        //ELIMINAR USUARIO 

    }
}
