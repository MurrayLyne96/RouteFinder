using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RouteFinderAPI.Data.Entities;
using RouteFinderAPI.Data.Interfaces;
using RouteFinderAPI.Models.ViewModels;

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

    public async Task<IEnumerable<UserViewModel>> GetAllUsers()
    {
        var users = await _database.Get<User>().ToListAsync();
        return _mapper.Map<List<UserViewModel>>(users);
    }

    public async Task<UserViewModel> GetUserById(Guid userId)
    {
        var user = await GetSingleUser(userId);
        return _mapper.Map<UserViewModel>(user);
    }

    public async Task CreateUser(UserCreateViewModel userModel)
    {
        var user = new User();
        _mapper.Map(userModel, user);
        user.Id = Guid.NewGuid();
        await _database.SaveChangesAsync();
    }

    public async Task<List<RouteViewModel>> GetRoutesFromUser(Guid userId)
    {
        var routes = await _database.Get<MapRoute>().Where(x => x.UserId == userId).ToListAsync();
        return _mapper.Map<List<RouteViewModel>>(routes);
    }

    public async Task<bool> UpdateUser(Guid userId, UserUpdateViewModel user)
    {
        var userEntity = await _database.Get<User>().Where(x => x.Id == userId).SingleOrDefaultAsync();
            
        if (userEntity is null)
        {
            return false;
        }
        
        _mapper.Map(user, userEntity);
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
        return await _database.Get<User>().Where(x => x.Id == userId).SingleOrDefaultAsync();
    }
}