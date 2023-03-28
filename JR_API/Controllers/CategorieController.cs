using Microsoft.AspNetCore.Mvc;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategorieController : ControllerBase
    {
        [HttpGet(Name = "GetCategorie")]
        public string Get()
        {
            return "Hola";
        }
    }
}
