using System.Net;
using RouteFinderAPI.Models.ViewModels;

namespace RouteFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<UserViewModel>))]
        public ActionResult<IList<UserViewModel>> GetAllUsers()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{userId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserDetailViewModel))]
        public ActionResult<UserViewModel> GetUser(Guid userId)
        {
            return Ok();
        }
        
        [HttpGet]
        [Route("{userId:guid}/routes")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<RouteViewModel>))]
        public ActionResult<List<RouteViewModel>> GetRoutesFromUser(Guid userId)
        {
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(Guid))]
        public ActionResult<Guid> CreateUser()
        {
            var test = new object();
            return Created("test", test);
        }

        [HttpPut]
        [Route("{userId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public ActionResult UpdateUser(Guid userId)
        {
            return NoContent();
        }

        [HttpDelete]
        [Route("{userId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public ActionResult DeleteUser(Guid userId)
        {
            return NoContent();
        }

    }
}
