using System.Security.Claims;

namespace RouteFinderAPI.Authentication;

public class AuthorizedAccountProvider : IAuthorizedAccountProvider
{
    private UserDto? _account;
    private readonly IUserService _userService;
    
    public AuthorizedAccountProvider(IUserService userService, IHttpContextAccessor contextAccessor)
    {
        _userService = userService;
    }
    
    public async Task<UserDto> GetLoggedInAccount(string id)
    {
        if (_account is not null) return _account;

        if (string.IsNullOrWhiteSpace(id)) return null;

        _account = await _userService.GetLoggedInUserById(Guid.Parse(id));

        return _account;
    }
}

public interface IAuthorizedAccountProvider
{
    Task<UserDto> GetLoggedInAccount(string id);
}