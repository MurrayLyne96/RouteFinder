namespace RouteFinderAPI.DAL.Specifications.Plotpoints;

public class PlotpointByIdSpec : Specification<Plotpoint>
{
    private readonly Guid _plotpointId;
    public PlotpointByIdSpec(Guid plotpointId) => _plotpointId = plotpointId;

    public override Expression<Func<Plotpoint, bool>> BuildExpression() => x => x.Id == _plotpointId;
}