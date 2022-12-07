using RouteFinderAPI.Models.ViewModels;

namespace RouteFinderAPI.Services;

public interface IUserService
{
    public Task<IEnumerable<UserViewModel>> GetAllUsers();
    public Task<UserViewModel> GetUserById(Guid userId);
    public Task CreateUser(UserCreateViewModel user);
    public Task<List<RouteViewModel>> GetRoutesFromUser(Guid userId);
    public Task<bool> UpdateUser(Guid userId, UserUpdateViewModel user);
    public Task<bool> DeleteUser(Guid userId);

}