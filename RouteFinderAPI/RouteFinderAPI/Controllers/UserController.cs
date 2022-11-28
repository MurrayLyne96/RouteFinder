namespace RouteFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("/users")]
        public void GetAllUsers()
        {
            
        }

        [HttpGet]
        [Route("/users/{userId:guid}")]
        public void GetUser(Guid userId)
        {
            
        }

        [HttpPost]
        [Route("/users")]
        [ProducesResponseType(201)]
        public void CreateUser()
        {
            
        }

        [HttpPut]
        [Route("/users/{userId:guid}")]
        [ProducesResponseType(204)]
        public void UpdateUser()
        {
            
        }

        [HttpDelete]
        [Route("/users/{userId:guid}")]
        [ProducesResponseType(204)]
        public void DeleteUser(Guid userId)
        {
            
        }

    }
}
