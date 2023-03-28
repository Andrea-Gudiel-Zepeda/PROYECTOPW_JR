using Microsoft.AspNetCore.Mvc;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        [HttpGet(Name = "GetStatus")]
        public string Get()
        {
            return "Hola";
        }
    }
}
