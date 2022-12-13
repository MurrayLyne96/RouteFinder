

namespace RouteFinderAPI.DAL.Specifications.MapRoutes;

public class MapRouteByUserIdSpec : Specification<MapRoute>
{
    private readonly Guid _userId;
    public MapRouteByUserIdSpec(Guid userId) => _userId = userId;
    
    public override Expression<Func<MapRoute, bool>> BuildExpression() => x => x.UserId == _userId;
}