namespace RouteFinderAPI.Services;

public interface IRouteService
{
    public Task<List<RouteViewModel>> GetAllRoutes();
    public Task<RouteViewModel> GetRouteById(Guid routeId);
    public Task CreateRoute(RouteCreateViewModel model);
    public Task<bool> UpdateRouteById(Guid routeId, RouteUpdateViewModel model);
    public Task<bool> DeleteRouteById(Guid routeId);
}