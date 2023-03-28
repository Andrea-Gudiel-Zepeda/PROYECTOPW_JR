using JR_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly JrDbContext _context;

        public StatusController()
        {
            _context = new JrDbContext();
        }

        //OBTENER STATUS
        [HttpGet(Name = "GetStatus")]
        public string Get()
        {
            return "Hola";
        }

        //CREAR STATUS

        //EDITAR STATUS

        //ELIMINAR STATUS 
    }
}
