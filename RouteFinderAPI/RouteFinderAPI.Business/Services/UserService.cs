namespace RouteFinderAPI.Services;

public class UserService : IUserService
{
    private readonly IRouteFinderDatabase _database;
    private readonly IMapper _mapper;

    public UserService(IRouteFinderDatabase database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public async Task<UserDto[]> GetAllUsers() =>
        await _mapper.ProjectTo<UserDto>(
            _database
                    .Get<User>()
                    .OrderBy(x => x.LastModified))
                .ToArrayAsync();

    public async Task<UserDto?> GetUserById(Guid userId) =>
        await _mapper.ProjectTo<UserDto?>(
                _database
                    .Get<User>()
                    .Where(new UserByIdSpec(userId))
                    .OrderBy(x => x.LastModified))
            .SingleOrDefaultAsync();

    public async Task CreateUser(UserCreateDto userModel)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Created = DateTime.UtcNow,
            LastModified = DateTime.UtcNow
        };
        
        _mapper.Map(userModel, user);
        await _database.AddAsync(user);
        await _database.SaveChangesAsync();
    }

    public async Task<RouteDto[]> GetRoutesFromUser(Guid userId) =>
        await _mapper.ProjectTo<RouteDto>(
                _database
                    .Get<MapRoute>()
                    .Where(new MapRouteByUserIdSpec(userId)))
            .ToArrayAsync();
                

    public async Task<bool> UpdateUser(Guid userId, UserUpdateDto userModel)
    {
        var userEntity = await GetSingleUser(userId);
        
        if (userEntity is null)
        {
            return false;
        }
        
        _mapper.Map(userModel, userEntity);

        await _database.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUser(Guid userId)
    {
        var user = await GetSingleUser(userId);
        
        if (user is null)
        {
            return false;
        }
        
        _database.Delete(user);
        await _database.SaveChangesAsync();
        return true;
    }

    private async Task<User> GetSingleUser(Guid userId)
    {
        return await _database.Get<User>().Where(new UserByIdSpec(userId)).SingleOrDefaultAsync();
    }
}