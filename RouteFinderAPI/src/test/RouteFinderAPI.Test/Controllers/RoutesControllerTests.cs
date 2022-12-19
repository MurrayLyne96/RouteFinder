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

        await _routeService.Received(1).GetRouteById(Guid.NewGuid());
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
    
    [Fact]
    public async Task UpdateRoute_WhenRouteUpdated_ReturnsNoContent()
    {
        // Arrange
        var routeUpdateViewModel = new RouteUpdateViewModel();
        var routeUpdateDto = new RouteUpdateDto();
        var id = Guid.NewGuid();
        _routeService.UpdateRouteById(id, routeUpdateDto).Returns(true);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.UpdateRouteById(id, routeUpdateViewModel);
        
        // Assert
        actionResult.AssertResult<NoContentResult>();

        await _routeService.Received(1).UpdateRouteById(id, routeUpdateDto);
    }
    
    [Fact]
    public async Task UpdateRoute_WhenRouteNotUpdated_ReturnsNotFound()
    {
        // Arrange
        var routeUpdateViewModel = new RouteUpdateViewModel();
        var routeUpdateDto = new RouteUpdateDto();
        var id = Guid.NewGuid();
        _routeService.UpdateRouteById(id, routeUpdateDto).Returns(false);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.UpdateRouteById(id, routeUpdateViewModel);
        
        // Assert
        actionResult.AssertResult<NotFoundResult>();

        await _routeService.Received(1).UpdateRouteById(id, routeUpdateDto);
    }
    
    [Fact]
    public async Task DeleteRoute_WhenRouteDeleted_ReturnsNoContent()
    {
        var id = Guid.NewGuid();
        _routeService.DeleteRouteById(id).Returns(true);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.DeleteRouteById(id);
        
        // Assert
        actionResult.AssertResult<NoContentResult>();

        await _routeService.Received(1).DeleteRouteById(id);
    }
    
    [Fact]
    public async Task DeleteRoute_WhenRouteNotDeleted_ReturnsNotFound()
    {
        // arrange
        var id = Guid.NewGuid();
        _routeService.DeleteRouteById(id).Returns(true);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.DeleteRouteById(id);
        
        // Assert
        actionResult.AssertResult<NotFoundResult>();

        await _routeService.Received(1).DeleteRouteById(id);
    }
    
    [Fact]
    public async Task CreatePlotpoint_WhenPlotpointCreated_ReturnsNoContent()
    {
        // arrange
        var plotPointUpdateViewModel = new PlotPointCreateModel();
        var plotPointCreateDto = new PlotpointCreateDto();
        var routeId = Guid.NewGuid();
        _plotpointService.CreatePlotPoint(plotPointCreateDto);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.CreatePlotPoint(routeId, plotPointUpdateViewModel);
        
        // Assert
        actionResult.AssertResult<CreatedResult>();

        await _plotpointService.Received(1).CreatePlotPoint(plotPointCreateDto);
    }
    
    [Fact]
    public async Task UpdatePlotPoint_WhenPlotPointUpdated_ReturnsNoContent()
    {
        // Arrange
        var plotPointUpdateViewModel = new PlotPointCreateModel();
        var plotPointUpdateDto = new PlotpointCreateDto();
        var routeId = Guid.NewGuid();
        _plotpointService.UpdatePlotPoint(routeId, plotPointUpdateDto).Returns(true);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.UpdatePlotPoint(routeId, Guid.NewGuid(),  plotPointUpdateViewModel);
        
        // Assert
        actionResult.AssertResult<NoContentResult>();

        await _plotpointService.Received(1).UpdatePlotPoint(Guid.NewGuid(), plotPointUpdateDto);
    }
    
    [Fact]
    public async Task UpdatePlotPoint_WhenPlotPointNotUpdated_ReturnsNotFound()
    {
        // Arrange
        var plotPointUpdateViewModel = new PlotPointCreateModel();
        var plotPointUpdateDto = new PlotpointCreateDto();
        var routeId = Guid.NewGuid();
        _plotpointService.UpdatePlotPoint(routeId, plotPointUpdateDto).Returns(false);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.UpdatePlotPoint(routeId, Guid.NewGuid(),  plotPointUpdateViewModel);
        
        // Assert
        actionResult.AssertResult<NotFoundResult>();

        await _plotpointService.Received(1).UpdatePlotPoint(Guid.NewGuid(), plotPointUpdateDto);
    }

    [Fact]
    public async Task DeletePlotPoint_WhenPlotPointDeleted_ReturnsNoContent()
    {
        // Arrange
        var routeId = Guid.NewGuid();
        var plotPointId = Guid.NewGuid();
        _plotpointService.DeletePlotPoint(plotPointId).Returns(true);
        var controller = RetrieveController();

        // Act
        var actionResult = await controller.DeletePlotPoint(routeId, plotPointId);
        
        // Assert
        actionResult.AssertResult<NoContentResult>();

        await _plotpointService.Received(1).DeletePlotPoint(plotPointId);
    }
    
    [Fact]
    public async Task DeletePlotPoint_WhenPlotPointNotDeleted_ReturnsNotFound()
    {
        // Arrange
        var routeId = Guid.NewGuid();
        var plotPointId = Guid.NewGuid();
        _plotpointService.DeletePlotPoint(plotPointId).Returns(false);
        var controller = RetrieveController();

        // Act
        var actionResult = await controller.DeletePlotPoint(routeId, plotPointId);
        
        // Assert
        actionResult.AssertResult<NotFoundResult>();

        await _plotpointService.Received(1).DeletePlotPoint(plotPointId);
    }

    private RoutesController RetrieveController()
    {
        return new RoutesController(_routeService, _plotpointService, _mapper);
    }
}