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

    public async Task<UserDetailDto?> GetUserById(Guid userId) =>
        await _mapper.ProjectTo<UserDetailDto?>(
            _database
                    .Get<User>()
                    .Where(new UserByIdSpec(userId))
                    .OrderBy(x => x.LastModified))
            .SingleOrDefaultAsync();

    public async Task<Guid> CreateUser(UserCreateDto userModel)
    {
        var user = _mapper.Map<User>(userModel);
        user.RoleId = await _database.Get<Role>()
                        .Where(new RoleByRoleNameSpec("USR"))
                        .Select(x => x.Id)
                        .SingleOrDefaultAsync();

        user.Password = BCrypt.Net.BCrypt.HashPassword(userModel.Password);
        await _database.AddAsync(user);
        await _database.SaveChangesAsync();
        return user.Id;
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
        if (!string.IsNullOrWhiteSpace(userModel.Password))
        {
            userEntity.Password = BCrypt.Net.BCrypt.HashPassword(userModel.Password);
        }
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

    private async Task<User?> GetSingleUser(Guid userId) => 
        await _database
            .Get<User>()
            .Where(new UserByIdSpec(userId))
            .SingleOrDefaultAsync();
}