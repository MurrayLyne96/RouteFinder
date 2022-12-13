namespace RouteFinderAPI.DAL.Specifications.MapRoutes;

public class RouteByIdSpec : Specification<MapRoute>
{
    private readonly Guid _routeId;
    public RouteByIdSpec(Guid routeId) => _routeId = routeId;
    public override Expression<Func<MapRoute, bool>> BuildExpression() => x => x.Id == _routeId;
}