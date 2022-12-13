using RouteFinderAPI.Services.Dto.Plotpoints;

namespace RouteFinderAPI.Services;

public interface IPlotpointService
{
    public Task CreatePlotPoint(params PlotpointCreateDto[] model);
    public Task<bool> UpdatePlotPoint(Guid plotPointId, PlotpointCreateDto model);
    public Task<bool> DeletePlotPoint(Guid plotPointId);
}