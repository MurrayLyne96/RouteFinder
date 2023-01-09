using AutoFixture;
using Microsoft.EntityFrameworkCore;
using MockQueryable.NSubstitute;
using NSubstitute.ReturnsExtensions;
using RouteFinderAPI.DAL.Specifications.MapRoutes;
using RouteFinderAPI.DAL.Specifications.Users;
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
    private readonly IFixture _fixture;
    public RouteServiceTests()
    {
        _database = Substitute.For<IRouteFinderDatabase>();
        _mapper = Substitute.For<IMapper>();
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
    }
    
    [Fact]
    public async Task GetAllRoutes_WhenRoutesFound_ReturnAllRoutes()
    {
        // Arrange
        var routes = new List<MapRoute>
        {
            new MapRoute(),
            new MapRoute()
        }.AsQueryable();

        var dtoRoutes = new List<RouteDto>
        {
            new RouteDto(),
            new RouteDto()
        }.AsQueryable().BuildMock();

        _mapper.ProjectTo<RouteDto>(Arg.Any<IQueryable<MapRoute>>()).Returns(dtoRoutes);
        
        _database.Get<MapRoute>().Returns(routes);
        var service = RetrieveService();
        
        // Act
        var result = await service.GetAllRoutes();
        
        // Assert
        result.Should().BeEquivalentTo(dtoRoutes);
        _mapper.Received(1).ProjectTo<RouteDto>(Arg.Is<IQueryable<MapRoute>>(x => x.Count() == 2));
    }

    [Fact]
    public async Task GetRouteById_WhenRouteFound_ReturnSingleRoute()
    {
        // Arrange
        var idToRetrieve = Guid.NewGuid();
        var entityToRetrieve = new MapRoute
        {
            Id = idToRetrieve
        };

        var routes = new List<MapRoute>
        {
            entityToRetrieve,
            new MapRoute()
        }.AsQueryable();

        var routeDto = new RouteDetailDto
        {
            Id = idToRetrieve
        };
        
        var routeDtos = new List<RouteDetailDto>
        {
            routeDto,
            new RouteDetailDto()
        }.AsQueryable().BuildMock();

        _database.Get<MapRoute>().Returns(routes);

        _mapper.ProjectTo<RouteDetailDto>(Arg.Any<IQueryable<MapRoute>>()).Returns(routeDtos.Where(x => x.Id == idToRetrieve));
        
        var service = RetrieveService();

        // Act
        var result = await service.GetRouteById(idToRetrieve);
        
        // Assert
        result.Should().BeEquivalentTo(routeDto);
        
        _mapper.Received(1).ProjectTo<RouteDetailDto>(Arg.Any<IQueryable<MapRoute>>());
    }

    [Fact]
    public async Task CreateRoute_WhenRouteCreated_ReturnsSingleRoute()
    {
        // Arrange 
        var newRoute = _fixture.Build<MapRoute>().Create();
        var routeCreateDto = _fixture.Create<RouteCreateDto>();
        var fakeRoutesList = new List<MapRoute>().AsQueryable().BuildMock();

        _database.SaveChangesAsync().Returns(1);
        _mapper.Map(Arg.Any<RouteCreateDto>(), Arg.Any<MapRoute>()).Returns(newRoute);
        _database.Get<MapRoute>().Returns(fakeRoutesList);
        
        // Act
        var service = RetrieveService();
        var result = await service.CreateRoute(routeCreateDto);
        
        // Assert
        result.Should().Be(newRoute.Id);
        _mapper.Received(1).Map(Arg.Any<RouteCreateDto>(), Arg.Any<MapRoute>());
        await _database.Received(1).SaveChangesAsync();
        await _database.Received(1).AddAsync(newRoute);
    }

    [Fact]
    public async Task UpdateRoute_WhenRouteUpdated_ReturnsTrue()
    {
        // Arrange
        var id = Guid.NewGuid();
        var routeToUpdate = _fixture.Create<MapRoute>();
        routeToUpdate.Id = id;
        var routes = _fixture.CreateMany<MapRoute>(10).ToList();
        
        routes.Add(routeToUpdate);

        var routesQuery = routes.AsQueryable().BuildMock();

        _database.Get<MapRoute>().Returns(routesQuery);
        
        var routeUpdateDto = _fixture.Create<RouteUpdateDto>();
        var service = RetrieveService();
        
        // Act
        var result = await service.UpdateRouteById(id, routeUpdateDto);
        
        // Assert
        result.Should().Be(true);
        _mapper.Received(1).Map(Arg.Any<RouteUpdateDto>(), Arg.Any<MapRoute>());
        await _database.Received(1).SaveChangesAsync();
    }
    
    [Fact]
    public async Task UpdateRoute_WhenRouteNotUpdated_ReturnsFalse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var routes = _fixture.CreateMany<MapRoute>(10).ToList();

        var routesQuery = routes.AsQueryable().BuildMock();

        _database.Get<MapRoute>().Returns(routesQuery);
        
        var routeUpdateDto = _fixture.Create<RouteUpdateDto>();
        var service = RetrieveService();
        
        // Act
        var result = await service.UpdateRouteById(id, routeUpdateDto);
        
        // Assert
        result.Should().Be(false);
    }
    
    [Fact]
    public async Task DeleteRoute_WhenRouteDeleted_ReturnsTrue()
    {
        // Arrange
        var id = Guid.NewGuid();
        var routeToUpdate = _fixture.Create<MapRoute>();
        routeToUpdate.Id = id;
        var routes = _fixture.CreateMany<MapRoute>(10).ToList();
        
        routes.Add(routeToUpdate);

        var routesQuery = routes.AsQueryable().BuildMock();

        _database.Get<MapRoute>().Returns(routesQuery);
        
        var routeUpdateDto = _fixture.Create<RouteUpdateDto>();
        var service = RetrieveService();
        
        // Act
        var result = await service.DeleteRouteById(id);
        
        // Assert
        result.Should().Be(true);
        await _database.Received(1).SaveChangesAsync();
        _database.Received(1).Delete(Arg.Any<MapRoute>());
    }
    
    [Fact]
    public async Task DeleteRoute_WhenRouteNotDeleted_ReturnsFalse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var routes = _fixture.CreateMany<MapRoute>(10).ToList();

        var routesQuery = routes.AsQueryable().BuildMock();

        _database.Get<MapRoute>().Returns(routesQuery);
        
        var routeUpdateDto = _fixture.Create<RouteUpdateDto>();
        var service = RetrieveService();
        
        // Act
        var result = await service.DeleteRouteById(id);
        
        // Assert
        result.Should().Be(false);
    }

    private RouteService RetrieveService()
    {
        return new RouteService(_database, _mapper);
    }
}