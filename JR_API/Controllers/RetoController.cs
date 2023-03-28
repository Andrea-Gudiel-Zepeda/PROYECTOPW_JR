using JR_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RetoController : ControllerBase
    {
        private readonly JrDbContext _context;

        public RetoController()
        {
            _context = new JrDbContext();
        }

        [HttpGet(Name = "RetoBook")]
        public string Get()
        {
            return "Hola";
        }
    }
}
