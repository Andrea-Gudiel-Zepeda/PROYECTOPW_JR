using JR_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReseñaController : ControllerBase
    {
        private readonly JrDbContext _context;

        public ReseñaController()
        {
            _context = new JrDbContext();
        }

        [HttpGet(Name = "GetResela")]
        public string Get()
        {
            return "Hola";
        }
    }
}
