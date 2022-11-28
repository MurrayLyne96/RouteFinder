namespace RouteFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("/auth")]
        [ProducesResponseType(200)]
        public void Authenticate()
        {
            
        }
    }
}
