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

    public async Task<RouteDetailDto?> GetRouteById(Guid routeId) =>
        await _mapper.ProjectTo<RouteDetailDto?>(
            _database.Get<MapRoute>()
                .Where(new RouteByIdSpec(routeId))
                .Include(x => x.Type)
                .Include(x => x.Plotpoints)).SingleOrDefaultAsync();

    public async Task<Guid> CreateRoute(RouteCreateDto model)
    {
        var routeEntity = new MapRoute();
        _mapper.Map(model, routeEntity);

        if (string.IsNullOrEmpty(model.Name))
        {
            throw new Exception();
        }

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

        _mapper.Map(model, routeEntity);

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

    private async Task<MapRoute?> GetSingleRoute(Guid routeId) =>
        await _database
            .Get<MapRoute>()
            .Where(new RouteByIdSpec(routeId))
            .SingleOrDefaultAsync();
}