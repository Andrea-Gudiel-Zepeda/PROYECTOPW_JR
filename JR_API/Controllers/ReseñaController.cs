using Microsoft.AspNetCore.Mvc;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReseñaController : ControllerBase
    {
        [HttpGet(Name = "GetResela")]
        public string Get()
        {
            return "Hola";
        }
    }
}
