using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using RouteFinderAPI.Controllers;
using RouteFinderAPI.Models.API;
using RouteFinderAPI.Models.ViewModels;
using RouteFinderAPI.Services;
using RouteFinderAPI.Services.Dto.Routes;
using RouteFinderAPI.Test.Extensions;

namespace RouteFinderAPI.Test.Controllers;

public class RoutesControllerTests
{
    private readonly IRouteService _routeService;
    private readonly IPlotpointService _plotpointService;
    private readonly IMapper _mapper;
    public RoutesControllerTests()
    {
        _routeService = Substitute.For<IRouteService>();
        _plotpointService = Substitute.For<IPlotpointService>();
        _mapper = Substitute.For<IMapper>();
    }

    [Fact]
    public async Task GetAllRoutes_WhenRoutesFound_MapsAndReturnsOk()
    {
        // Arrange
        var routeDtos = new RouteDto[] { new RouteDto() };
        var routesViewModels = new RouteViewModel[] { new RouteViewModel() };

        _routeService.GetAllRoutes().Returns(routeDtos);
        _mapper.Map<RouteViewModel[]>(routeDtos).Returns(routesViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.GetAllRoutes();
        
        // Assert
        var result = actionResult.AssertObjectResult<RouteViewModel[], OkObjectResult>();

        result.Should().BeSameAs(routesViewModels);

        await _routeService.Received(1).GetAllRoutes();
        _mapper.Received(1).Map<RouteViewModel[]>(routeDtos);
    }
    
    [Fact]
    public async Task GetAllRoutes_WhenNoRoutesFound_ReturnsNoContent()
    {
        // Arrange
        
        _routeService.GetAllRoutes().Returns(Array.Empty<RouteDto>());

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.GetAllRoutes();
        
        // Assert
        actionResult.AssertResult<RouteViewModel[], NoContentResult>();

        await _routeService.Received(1).GetAllRoutes();
    }
    
    [Fact]
    public async Task GetRouteById_WhenRouteFound_ReturnsOk()
    {
        // Arrange
        var routeDto = new RouteDetailDto { Id = Guid.NewGuid() };
        
        _routeService.GetRouteById(routeDto.Id).Returns(routeDto);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.GetRouteById(routeDto.Id);
        
        // Assert
        actionResult.AssertResult<RouteDetailViewModel, OkResult>();

        await _routeService.Received(1).GetAllRoutes();
    }
    
    [Fact]
    public async Task GetRouteById_WhenRouteNotFound_ReturnsNoContent()
    {
        // Arrange
        var routeDto = new RouteDetailDto { Id = Guid.NewGuid() };
        
        _routeService.GetRouteById(routeDto.Id).Returns(routeDto);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.GetRouteById(Guid.NewGuid());
        
        // Assert
        actionResult.AssertResult<RouteDetailViewModel, NoContentResult>();

        await _routeService.Received(1).GetRouteById();
    }
    
    [Fact]
    public async Task CreateRoute_WhenRouteCreated_ReturnsCreated()
    {
        // Arrange
        var routeCreateViewModel = new RouteCreateViewModel();
        var routeCreateDto = new RouteCreateDto();
        _routeService.CreateRoute(routeCreateDto).Returns(Guid.NewGuid());

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.CreateRoute(routeCreateViewModel);
        
        // Assert
        actionResult.AssertResult<Guid, CreatedResult>();

        await _routeService.Received(1).CreateRoute(routeCreateDto);
    }
    
    private RoutesController RetrieveController()
    {
        return new RoutesController(_routeService, _plotpointService, _mapper);
    }
}