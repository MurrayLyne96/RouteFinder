using MockQueryable.NSubstitute;
using RouteFinderAPI.Data.Entities;
using RouteFinderAPI.Data.Interfaces;
using RouteFinderAPI.Services;
using RouteFinderAPI.Services.Dto.Routes;
using Xunit;

namespace RouteFinderAPI.Business.Tests.Services;

public class RouteServiceTests
{
    private readonly IRouteFinderDatabase _database;
    private readonly IMapper _mapper;
    public RouteServiceTests()
    {
        _database = Substitute.For<IRouteFinderDatabase>();
        _mapper = Substitute.For<IMapper>();
    }
    
    [Fact]
    public async Task GetAllRoutes_WhenRoutesFound_ReturnAllRoutes()
    {
        // Arrange
        var routes = new List<MapRoute>()
        {
            new MapRoute(),
            new MapRoute()
        }.AsQueryable().BuildMockDbSet();

        var dtoRoutes = new List<RouteDto>()
        {
            new RouteDto(),
            new RouteDto()
        };

        _mapper.Map<List<RouteDto>>(routes).Returns(dtoRoutes);
        
        _database.Get<MapRoute>().Returns(routes);
        var service = RetrieveService();
        
        // Act
        var result = await service.GetAllRoutes();
        // Assert
        result.Should().BeEquivalentTo(dtoRoutes);
        _mapper.Received(1).Map<List<RouteDto>>(routes);
    }

    [Fact]
    public async Task GetRouteById_WhenRouteFound_ReturnSingleRoute()
    {
        // Arrange
        var idToRetrieve = Guid.NewGuid();
        var entityToRetrieve = new MapRoute()
        {
            Id = idToRetrieve
        };
        
        var routes = new List<MapRoute>()
        {
            entityToRetrieve,
            new MapRoute()
        };

        var routeDto = new RouteDetailDto()
        {
            Id = idToRetrieve
        };

        var mock = routes.AsQueryable().BuildMockDbSet();
        
        _database.Get<MapRoute>().Returns(mock);

        _mapper.Map<RouteDetailDto>(entityToRetrieve).Returns(routeDto);
        
        var service = RetrieveService();

        // Act
        var result = await service.GetRouteById(idToRetrieve);
        result.Should().BeEquivalentTo(routeDto);
        
        _mapper.Received(1).Map<RouteDetailDto>(entityToRetrieve);
    }

    private RouteService RetrieveService()
    {
        return new RouteService(_database, _mapper);
    }
}