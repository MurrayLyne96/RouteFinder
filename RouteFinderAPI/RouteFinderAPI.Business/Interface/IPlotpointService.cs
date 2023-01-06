using RouteFinderAPI.Services.Dto.Plotpoints;

namespace RouteFinderAPI.Services;

public interface IPlotpointService
{
    public Task<bool> CreatePlotPoint(params PlotpointCreateDto[] model);
    public Task<bool> UpdatePlotPoint(Guid plotPointId, PlotpointUpdateDto model);
    public Task<bool> DeletePlotPoint(Guid plotPointId);
}