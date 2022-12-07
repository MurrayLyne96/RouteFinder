namespace RouteFinderAPI.Services;

public class RouteService : IRouteService
{
    private readonly IRouteFinderDatabase _database;
    private readonly IMapper _mapper;
    public RouteService(IRouteFinderDatabase database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }
    public async Task<List<RouteViewModel>> GetAllRoutes()
    {
        var routes = await _database.Get<MapRoute>().ToListAsync();
        return _mapper.Map<List<RouteViewModel>>(routes);
    }

    public async Task<RouteViewModel> GetRouteById(Guid routeId)
    {
        var route = await GetSingleRoute(routeId);
        return _mapper.Map<RouteViewModel>(route);
    }

    public async Task CreateRoute(RouteCreateViewModel model)
    {
        var routeEntity = new MapRoute();
        _mapper.Map(model, routeEntity);
        await _database.AddAsync(routeEntity);
        await _database.SaveChangesAsync();
    }

    public async Task<bool> UpdateRouteById(Guid routeId, RouteUpdateViewModel model)
    {
        var routeEntity = await GetSingleRoute(routeId);
        if (routeEntity is null)
        {
            return false;
        }
            
        routeEntity.RouteName = model.Name;
        routeEntity.TypeId = model.TypeId;

        await _database.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteRouteById(Guid routeId)
    {
        var routeEntity = await GetSingleRoute(routeId);
            
        if (routeEntity is null)
        {
            return false;
        }
            
        _database.Delete(routeEntity);
        await _database.SaveChangesAsync();
        return true;
    }

    private async Task<MapRoute> GetSingleRoute(Guid routeId)
    {
        return await _database.Get<MapRoute>().Where(x => x.Id == routeId).SingleOrDefaultAsync();
    }
}