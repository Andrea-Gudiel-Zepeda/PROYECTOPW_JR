using Microsoft.AspNetCore.Mvc;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RetoController : ControllerBase
    {
        [HttpGet(Name = "RetoBook")]
        public string Get()
        {
            return "Hola";
        }
    }
}
