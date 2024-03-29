using RouteFinderAPI.Models.ViewModels;

namespace RouteFinderAPI.Services;

public interface IUserService
{
    public Task<UserDto[]> GetAllUsers();
    public Task<UserDetailDto> GetUserById(Guid userId);
    public Task<UserDto> GetLoggedInUserById(Guid userId);
    public Task<Guid> CreateUser(UserCreateDto user);
    public Task<RouteDto[]> GetRoutesFromUser(Guid userId);
    public Task<bool> UpdateUser(Guid userId, UserUpdateDto userModel);
    public Task<bool> DeleteUser(Guid userId);

}