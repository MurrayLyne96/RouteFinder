using System.Security.Claims;

namespace RouteFinderAPI.Authentication;

public class AuthorizedAccountProvider : IAuthorizedAccountProvider
{
    private UserDto? _account;
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _contextAccessor;
    
    public AuthorizedAccountProvider(IUserService userService, IHttpContextAccessor contextAccessor)
    {
        _userService = userService;
        _contextAccessor = contextAccessor;
    }
    
    public async Task<UserDto> GetLoggedInAccount()
    {
        if (_account is not null) return _account;

        var identifier = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(identifier)) return null;

        _account = await _userService.GetLoggedInUserById(Guid.Parse(identifier));

        return _account;
    }
}

public interface IAuthorizedAccountProvider
{
    Task<UserDto> GetLoggedInAccount();
}