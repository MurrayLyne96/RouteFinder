namespace RouteFinderAPI.Services;

public interface IPlotpointService
{
    public Task CreatePlotPoint(Guid routeId, PlotPointCreateModel model);
    public Task UpdatePlotPoint(Guid plotPointId, PlotPointCreateModel model);
    public Task DeletePlotPoint(Guid plotPointId);
}