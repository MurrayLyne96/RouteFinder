using RouteFinderAPI.DAL.Specifications.MapRoutes;

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
    public async Task<RouteDto[]> GetAllRoutes() => 
        await _mapper.ProjectTo<RouteDto>(
            _database.Get<MapRoute>()
                .OrderBy(x => x.LastModified))
            .ToArrayAsync();

    public async Task<RouteDetailDto> GetRouteById(Guid routeId) =>
        await _mapper.ProjectTo<RouteDetailDto>(
            _database.Get<MapRoute>()
                .Where(new RouteByIdSpec(routeId))).SingleOrDefaultAsync();

    public async Task<Guid> CreateRoute(RouteUpdateDto model)
    {
        var routeEntity = new MapRoute();
        _mapper.Map(model, routeEntity);
        routeEntity.Created = DateTime.UtcNow;
        routeEntity.LastModified = DateTime.UtcNow;
        await _database.AddAsync(routeEntity);
        await _database.SaveChangesAsync();
        return routeEntity.Id;
    }

    public async Task<bool> UpdateRouteById(Guid routeId, RouteUpdateDto model)
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
        return await _database.Get<MapRoute>().Where(new RouteByIdSpec(routeId)).SingleOrDefaultAsync();
    }

    private async Task<MapRoute> GetSingleRouteWithType(Guid routeId)
    {
        return await _database.Get<MapRoute>().Where(new RouteByIdSpec(routeId)).Include(x => x.Type).SingleOrDefaultAsync();
    }
}