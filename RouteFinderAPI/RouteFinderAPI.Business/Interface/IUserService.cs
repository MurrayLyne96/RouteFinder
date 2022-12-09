using RouteFinderAPI.Models.ViewModels;

namespace RouteFinderAPI.Services;

public interface IUserService
{
    public Task<UserDto[]> GetAllUsers();
    public Task<UserDto> GetUserById(Guid userId);
    public Task CreateUser(UserCreateDto user);
    public Task<RouteDto[]> GetRoutesFromUser(Guid userId);
    public Task<bool> UpdateUser(Guid userId, UserUpdateDto userModel);
    public Task<bool> DeleteUser(Guid userId);

}