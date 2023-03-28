using Microsoft.AspNetCore.Mvc;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GoalController : ControllerBase
    {
        [HttpGet(Name = "GetGoal")]
        public string Get()
        {
            return "Hola";
        }
    }
}
