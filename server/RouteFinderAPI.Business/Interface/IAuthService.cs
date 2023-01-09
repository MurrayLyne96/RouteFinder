namespace RouteFinderAPI.Services;

public interface IAuthService
{
    Task<UserDto?>  Authenticate(string email, string password);
}