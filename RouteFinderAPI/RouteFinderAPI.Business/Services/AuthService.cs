namespace RouteFinderAPI.Services;

public class AuthService : IAuthService
{
    private readonly IRouteFinderDatabase _database;
    private readonly IMapper _mapper;

    public AuthService(IRouteFinderDatabase database, IMapper mapper) => (_database, _mapper) = (database, mapper);
    
    public async Task<UserDto?> Authenticate(string email, string password)
    {
        var user = await _database.Get<User>().Where(new UserByEmailSpec(email)).Include(x => x.Role).SingleOrDefaultAsync();
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password)) return null;

        return _mapper.Map<UserDto?>(user);
    }
}