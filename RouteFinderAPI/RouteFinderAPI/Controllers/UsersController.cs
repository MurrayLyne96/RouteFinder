using System.Net;
using Microsoft.EntityFrameworkCore;
using RouteFinderAPI.Models.ViewModels;
using RouteFinderAPI.Data.Contexts;
using RouteFinderAPI.Data.Entities;
using RouteFinderAPI.Data.Interfaces;
using RouteFinderAPI.Services;

namespace RouteFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<UserViewModel>))]
        public async Task<ActionResult<IList<UserViewModel>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("{userId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserDetailViewModel))]
        public async Task<ActionResult<UserViewModel>> GetUser(Guid userId)
        {
            var user = await _userService.GetUserById(userId);
            return Ok(user);
        }
        
        [HttpGet]
        [Route("{userId:guid}/routes")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<RouteViewModel>))]
        public async Task<ActionResult<List<RouteViewModel>>> GetRoutesFromUser(Guid userId)
        {
            var routes = await _userService.GetRoutesFromUser(userId);
            return Ok(routes);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(Guid))]
        public async Task<ActionResult<Guid>> CreateUser(UserCreateViewModel user)
        {
            await _userService.CreateUser(user);
            
            return Created(this.Url.ToString(), user);
        }

        [HttpPut]
        [Route("{userId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> UpdateUser(Guid userId, UserUpdateViewModel user)
        {
            var result = await _userService.UpdateUser(userId, user);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("{userId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> DeleteUser(Guid userId)
        {
            var result = await _userService.DeleteUser(userId);
            
            if (!result)
            {
                return NotFound();
            }
            
            return NoContent();
        }

    }
}
