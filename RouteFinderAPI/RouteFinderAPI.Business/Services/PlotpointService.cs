using RouteFinderAPI.DAL.Specifications.Plotpoints;
using RouteFinderAPI.Services.Dto.Plotpoints;

namespace RouteFinderAPI.Services;

public class PlotpointService : IPlotpointService
{
    private readonly IRouteFinderDatabase _database;
    private readonly IMapper _mapper;
    
    public PlotpointService(IRouteFinderDatabase database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }
    
    public async Task CreatePlotPoint(params PlotpointCreateDto[] model)
    {
        var plotPointEntites = _mapper.Map<Plotpoint[]>(model);

        await _database.AddAsync(plotPointEntites);

        await _database.SaveChangesAsync();
    }

    public async Task UpdatePlotPoint(Guid plotPointId, PlotpointCreateDto model)
    {
        var plotPointEntity = await GetSinglePlotpoint(plotPointId);

        _mapper.Map(model, plotPointEntity);
        plotPointEntity.LastModified = DateTime.UtcNow;

        await _database.SaveChangesAsync();
    }

    public async Task DeletePlotPoint(Guid plotPointId)
    {
        var plotPointEntity = await GetSinglePlotpoint(plotPointId);
        _database.Delete(plotPointEntity);
        await _database.SaveChangesAsync();
    }

    private async Task<Plotpoint> GetSinglePlotpoint(Guid plotPointId)
    {
        return await _database.Get<Plotpoint>().Where(new PlotpointByIdSpec(plotPointId)).SingleOrDefaultAsync();
    }
}