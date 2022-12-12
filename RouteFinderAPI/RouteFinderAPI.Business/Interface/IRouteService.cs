namespace RouteFinderAPI.Services;

public interface IRouteService
{
    public Task<RouteDto[]> GetAllRoutes();
    public Task<RouteDetailDto> GetRouteById(Guid routeId);
    public Task<Guid> CreateRoute(RouteCreateDto model);
    public Task<bool> UpdateRouteById(Guid routeId, RouteUpdateDto model);
    public Task<bool> DeleteRouteById(Guid routeId);
}