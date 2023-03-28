using JR_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalificacionController : ControllerBase
    {
        private readonly JrDbContext _context;

        public CalificacionController()
        {
            _context = new JrDbContext();
        }

        [HttpGet(Name = "GetCalificacion")]
        public string Get()
        {
            return "Hola";
        }
    }
}
