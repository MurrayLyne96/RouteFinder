using Microsoft.AspNetCore.Authorization;

namespace RouteFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<UserViewModel>))]
        public async Task<ActionResult<UserViewModel[]>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return OkOrNoListContent(_mapper.Map<UserViewModel[]>(users));
        }

        [HttpGet]
        [Route("{userId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserDetailViewModel))]
        public async Task<ActionResult<UserDetailViewModel>> GetUser(Guid userId)
        {
            var user = await _userService.GetUserById(userId);
            return OkOrNoNotFound(_mapper.Map<UserDetailViewModel>(user));
        }
        
        [HttpGet]
        [Route("{userId:guid}/routes")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<RouteViewModel>))]
        public async Task<ActionResult<RouteViewModel[]>> GetRoutesFromUser(Guid userId)
        {
            var routes = await _userService.GetRoutesFromUser(userId);
            return OkOrNoListContent(_mapper.Map<RouteViewModel[]>(routes));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(Guid))]
        [AllowAnonymous]
        public async Task<ObjectResult> CreateUser(UserCreateViewModel user)
        {
            var userCreateDto = _mapper.Map<UserCreateDto>(user);
            var id = await _userService.CreateUser(userCreateDto);
            
            return StatusCode((int)HttpStatusCode.Created, id.ToString());
        }

        [HttpPut]
        [Route("{userId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> UpdateUser(Guid userId, UserUpdateViewModel user)
        {
            var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
            return NoContentOrNoNotFound(await _userService.UpdateUser(userId, userUpdateDto));
        }

        [HttpDelete]
        [Route("{userId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> DeleteUser(Guid userId)
        {
            return NoContentOrNoNotFound(await _userService.DeleteUser(userId));
        }
        
        

    }
}
