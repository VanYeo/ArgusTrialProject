using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/health")]
    public class HealthController : Controller
    {
        [HttpGet]
        public ActionResult GetHealthCheck()
        {
            return Ok("The app is listening on port 5143");
        }
    }
}
