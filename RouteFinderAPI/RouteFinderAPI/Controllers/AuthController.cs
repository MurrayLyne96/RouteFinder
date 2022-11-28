using System.Net;

namespace RouteFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("/auth")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TokenModel))]
        public ActionResult<TokenModel> Authenticate(UserAuthModel model)
        {
            return Ok();
        }
    }
}
