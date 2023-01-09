namespace RouteFinderAPI.DAL.Specifications.MapRoutes;

public class GetPlotpointsByRouteId : Specification<Plotpoint>
{
    private readonly Guid _routeId;
    public GetPlotpointsByRouteId(Guid routeId) => _routeId = routeId;
    
    public override Expression<Func<Plotpoint, bool>> BuildExpression() => x => x.MapRouteId == _routeId;
}