using System.Net;
using Microsoft.EntityFrameworkCore;
using RouteFinderAPI.Models.ViewModels;
using RouteFinderAPI.Data.Contexts;
using RouteFinderAPI.Data.Entities;
using RouteFinderAPI.Data.Interfaces;

namespace RouteFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRouteFinderDatabase _database;
        private UserDetailViewValidator _userDetailViewValidator;
        private UserViewValidator _userViewValidator;
        private UserCreateViewValidator _userCreateViewValidator;
        private UserUpdateViewValidator _userUpdateViewValidator;

        public UsersController(IRouteFinderDatabase database)
        {
            _database = database;
            _userViewValidator = new();
            _userCreateViewValidator = new();
            _userDetailViewValidator = new();
            _userUpdateViewValidator = new();
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<UserViewModel>))]
        public async Task<ActionResult<IList<UserViewModel>>> GetAllUsers()
        {
            var users = await _database.Get<User>().ToListAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("{userId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserDetailViewModel))]
        public async Task<ActionResult<UserViewModel>> GetUser(Guid userId)
        {
            var user = await _database.Get<User>().Where(x => x.Id == userId).SingleOrDefaultAsync();
            return Ok(user);
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
        public async Task<ActionResult<Guid>> CreateUser(UserCreateViewModel user)
        {
            var result = await _userCreateViewValidator.ValidateAsync(user);
            
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            
            var userEntity = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                Created = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };

            await _database.AddAsync(userEntity);
            await _database.SaveChangesAsync();
            
            return Created("test", userEntity);
        }

        [HttpPut]
        [Route("{userId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> UpdateUser(Guid userId, UserUpdateViewModel user)
        {
            var result = await _userUpdateViewValidator.ValidateAsync(user);
            
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var userEntity = await _database.Get<User>().Where(x => x.Id == userId).SingleOrDefaultAsync();
            
            if (userEntity is null)
            {
                return NotFound("User was not found");
            }
            
            userEntity.FirstName = user.FirstName;
            userEntity.LastName = user.LastName;
            userEntity.Email = user.Email;
            userEntity.DateOfBirth = user.DateOfBirth;

            await _database.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        [Route("{userId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> DeleteUser(Guid userId)
        {
            var userEntity = await _database.Get<User>().Where(x => x.Id == userId).SingleOrDefaultAsync();
            if (userEntity is null)
            {
                return NotFound("User was not found");
            }
            
            _database.Delete(userEntity);
            return NoContent();
        }

    }
}
