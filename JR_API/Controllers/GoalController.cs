using JR_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GoalController : ControllerBase
    {
        private readonly JrDbContext _context;

        public GoalController()
        {
            _context = new JrDbContext();
        }

        [HttpGet(Name = "GetGoal")]
        public string Get()
        {
            return "Hola";
        }
    }
}
