using JR_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategorieController : ControllerBase
    {
        private readonly JrDbContext _context;

        public CategorieController()
        {
            _context = new JrDbContext();
        }

        [HttpGet(Name = "GetCategorie")]
        public string Get()
        {
            return "Hola";
        }
    }
}
