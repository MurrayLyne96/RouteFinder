using NSubstitute.ReturnsExtensions;
using RouteFinderAPI.Services.Dto.Plotpoints;
using RouteFinderAPI.Services.Dto.Users;

namespace RouteFinderAPI.Business.Tests.Services;

public class PlotpontServiceTests
{
    private readonly IRouteFinderDatabase _database;
    private readonly IMapper _mapper;
    private readonly IFixture _fixture;
    public PlotpontServiceTests()
    {
        _database = Substitute.For<IRouteFinderDatabase>();
        _mapper = Substitute.For<IMapper>();
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
    }

    [Fact]
    public async Task CreatePlotPoint_WhenPlotPointCreated_ReturnsTrue()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var plotpoint = _fixture.Build<Plotpoint>().With(x => x.Id, guid).Create();
        var plotpointCreateDto = _fixture.Create<PlotpointCreateDto>();


        _mapper.Map<Plotpoint>(Arg.Any<PlotpointCreateDto>()).Returns(plotpoint);
        var service = RetrieveService();

        // Act
        var result = await service.CreatePlotPoint(plotpointCreateDto);
        
        // Assert
        result.Should().Be(true);
        _mapper.Received(1).Map<Plotpoint[]>(Arg.Any<PlotpointCreateDto[]>());
        await _database.Received(1).AddAsync(Arg.Any<Plotpoint[]>());
        await _database.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task UpdatePlotPoint_WhenPlotPointUpdated_ReturnsTrue()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var plotpoint = _fixture.Build<Plotpoint>().With(x => x.Id, guid).Create();
        var plotpointCreateDto = _fixture.Create<PlotpointUpdateDto>();

        var plotpoints = _fixture.CreateMany<Plotpoint>(10).ToList();
        plotpoints.Add(plotpoint);
        var plotPointQuery = plotpoints.AsQueryable().BuildMock();

        _database.Get<Plotpoint>().Returns(plotPointQuery);
        _mapper.Map<Plotpoint>(Arg.Any<PlotpointUpdateDto>()).Returns(plotpoint);
        _database.SaveChangesAsync().Returns(1);

        var service = RetrieveService();

        // Act
        var result = await service.UpdatePlotPoint(guid, plotpointCreateDto);

        // Assert
        result.Should().Be(true);
        _mapper.Received(1).Map(Arg.Any<PlotpointUpdateDto>(), Arg.Any<Plotpoint>());
        await _database.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task UpdatePlotPoint_WhenPlotPointNotUpdated_ReturnsFalse()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var plotpointCreateDto = _fixture.Create<PlotpointUpdateDto>();

        var plotpoints = _fixture.CreateMany<Plotpoint>(10).ToList();
        var plotPointQuery = plotpoints.AsQueryable().BuildMock();

        _database.Get<Plotpoint>().Returns(plotPointQuery);

        var service = RetrieveService();

        // Act
        var result = await service.UpdatePlotPoint(guid, plotpointCreateDto);
        
        // Assert
        result.Should().Be(false);
    }

    [Fact]
    public async Task DeletedPlotPoint_WhenPlotPointDeleted_ReturnsTrue()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var plotpoint = _fixture.Build<Plotpoint>().With(x => x.Id, guid).Create();
        var plotpointCreateDto = _fixture.Create<PlotpointCreateDto>();

        var plotpoints = _fixture.CreateMany<Plotpoint>(10).ToList();
        plotpoints.Add(plotpoint);
        var plotPointQuery = plotpoints.AsQueryable().BuildMock();

        _database.Get<Plotpoint>().Returns(plotPointQuery);
        _database.SaveChangesAsync().Returns(1);
        var service = RetrieveService();

        // Act
        var result = await service.DeletePlotPoint(guid);
        
        // Assert
        result.Should().Be(true);
        _database.Received(1).Delete(Arg.Any<Plotpoint>());
        await _database.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task DeletedPlotPoint_WhenPlotPointNotDeleted_ReturnsFalse()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var plotpointCreateDto = _fixture.Create<PlotpointCreateDto>();

        var plotpoints = _fixture.CreateMany<Plotpoint>(10).ToList();
        var plotPointQuery = plotpoints.AsQueryable().BuildMock();

        _database.Get<Plotpoint>().Returns(plotPointQuery);
        var service = RetrieveService();

        _database.SaveChangesAsync().Returns(0);


        // Act
        var result = await service.DeletePlotPoint(guid);
        
        // Assert
        result.Should().Be(false);
    }

    private PlotpointService RetrieveService()
    {
        return new PlotpointService(_database, _mapper);
    }
}