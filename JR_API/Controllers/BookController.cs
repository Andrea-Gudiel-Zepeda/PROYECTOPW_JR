using Microsoft.AspNetCore.Mvc;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        [HttpGet(Name = "GetBook")]
        public string Get()
        {
            return "Hola Libros";
        }
    }
}
