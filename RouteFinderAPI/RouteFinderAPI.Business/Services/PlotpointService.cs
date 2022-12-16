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
    
    public async Task CreatePlotPoint(params PlotpointCreateDto[] model) //TODO: Return boolean
    {
        var plotPointEntites = _mapper.Map<Plotpoint[]>(model);

        await _database.AddAsync(plotPointEntites);

        await _database.SaveChangesAsync();
    }

    public async Task<bool> UpdatePlotPoint(Guid plotPointId, PlotpointCreateDto model)
    {
        var plotPointEntity = await GetSinglePlotpoint(plotPointId);
        
        if (plotPointEntity is null) return false;
        
        _mapper.Map(model, plotPointEntity);
        return await _database.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeletePlotPoint(Guid plotPointId)
    {
        var plotPointEntity = await GetSinglePlotpoint(plotPointId);
        
        if (plotPointEntity is null) return false;
        
        _database.Delete(plotPointEntity);
        return await _database.SaveChangesAsync() > 0;
    }

    private async Task<Plotpoint?> GetSinglePlotpoint(Guid plotPointId) =>
        await _database.Get<Plotpoint>()
            .Where(new PlotpointByIdSpec(plotPointId))
            .SingleOrDefaultAsync();
}