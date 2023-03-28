using Microsoft.AspNetCore.Mvc;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalificacionController : ControllerBase
    {
        [HttpGet(Name = "GetCalificacion")]
        public string Get()
        {
            return "Hola";
        }
    }
}
