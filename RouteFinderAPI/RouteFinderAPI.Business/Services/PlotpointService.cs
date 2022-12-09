using RouteFinderAPI.DAL.Specifications.Plotpoints;

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
    
    public async Task CreatePlotPoint(Guid routeId, PlotPointCreateModel model)
    {
        var plotPointEntity = new Plotpoint()
        {
            Id = Guid.NewGuid(),
            Created = DateTime.UtcNow,
            LastModified = DateTime.UtcNow
        };

        _mapper.Map(model, plotPointEntity);

        await _database.AddAsync(plotPointEntity);
    }

    public async Task UpdatePlotPoint(Guid plotPointId, PlotPointCreateModel model)
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