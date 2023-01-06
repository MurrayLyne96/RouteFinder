using System.Net;
using NSubstitute.ReturnsExtensions;

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
        _mapper.Map<RouteViewModel[]>(Array.Empty<RouteDto>()).Returns(Array.Empty<RouteViewModel>());
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
        var routeViewModel = new RouteDetailViewModel();
        _routeService.GetRouteById(routeDto.Id).Returns(routeDto);
        _mapper.Map<RouteDetailViewModel>(routeDto).Returns(routeViewModel);
        
        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.GetRouteById(routeDto.Id);
        
        // Assert
        actionResult.AssertObjectResult<RouteDetailViewModel, OkObjectResult>();

        await _routeService.Received(1).GetRouteById(routeDto.Id);
    }
    
    [Fact]
    public async Task GetRouteById_WhenRouteNotFound_ReturnsNotFound()
    {
        // Arrange
        var routeDto = new RouteDetailDto { Id = Guid.NewGuid() };
        var routeViewModel = new RouteDetailViewModel();
        _mapper.Map<RouteDetailViewModel>(routeDto).Returns(routeViewModel);
        _routeService.GetRouteById(routeDto.Id).ReturnsNull();

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.GetRouteById(routeDto.Id);
        
        // Assert
        actionResult.AssertResult<RouteDetailViewModel, NotFoundResult>();

        await _routeService.Received(1).GetRouteById(routeDto.Id);
    }
    
    [Fact]
    public async Task CreateRoute_WhenRouteCreated_ReturnsCreated()
    {
        // Arrange
        var routeCreateViewModel = new RouteCreateViewModel();
        var routeCreateDto = new RouteCreateDto();
        var routeId = Guid.NewGuid();
        _routeService.CreateRoute(routeCreateDto).Returns(routeId);
        _mapper.Map<RouteCreateDto>(routeCreateViewModel).Returns(routeCreateDto);
        
        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.CreateRoute(routeCreateViewModel);
        
        // Assert
        actionResult.AssertResult<StatusCodeResult>(HttpStatusCode.Created);

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
        _mapper.Map<RouteUpdateDto>(routeUpdateViewModel).Returns(routeUpdateDto);
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
        _mapper.Map<RouteUpdateDto>(routeUpdateViewModel).Returns(routeUpdateDto);
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
        _routeService.DeleteRouteById(id).Returns(false);

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
        
        _plotpointService.CreatePlotPoint(plotPointCreateDto).Returns(true);
        _mapper.Map<PlotpointCreateDto>(plotPointUpdateViewModel).Returns(plotPointCreateDto);
        
        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.CreatePlotPoint(routeId, plotPointUpdateViewModel);
        
        // Assert
        actionResult.AssertResult<StatusCodeResult>(HttpStatusCode.Created);

        await _plotpointService.Received(1).CreatePlotPoint(plotPointCreateDto);
    }
    
    [Fact]
    public async Task UpdatePlotPoint_WhenPlotPointUpdated_ReturnsNoContent()
    {
        // Arrange
        var plotPointUpdateViewModel = new PlotPointCreateModel();
        var plotPointUpdateDto = new PlotpointUpdateDto();
        var routeId = Guid.NewGuid();
        var plotpointId = Guid.NewGuid();
        _plotpointService.UpdatePlotPoint(plotpointId, plotPointUpdateDto).Returns(true);
        _mapper.Map<PlotpointUpdateDto>(plotPointUpdateViewModel).Returns(plotPointUpdateDto);
        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.UpdatePlotPoint(routeId, plotpointId,  plotPointUpdateViewModel);
        
        // Assert
        actionResult.AssertResult<NoContentResult>();

        await _plotpointService.Received(1).UpdatePlotPoint(plotpointId, plotPointUpdateDto);
    }
    
    [Fact]
    public async Task UpdatePlotPoint_WhenPlotPointNotUpdated_ReturnsNotFound()
    {
        // Arrange
        var plotPointUpdateViewModel = new PlotPointCreateModel();
        var plotPointUpdateDto = new PlotpointUpdateDto();
        var routeId = Guid.NewGuid();
        var plotPointId = Guid.NewGuid();
        _plotpointService.UpdatePlotPoint(plotPointId, plotPointUpdateDto).Returns(false);
        _mapper.Map<PlotpointUpdateDto>(plotPointUpdateViewModel).Returns(plotPointUpdateDto);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.UpdatePlotPoint(routeId, plotPointId,  plotPointUpdateViewModel);
        
        // Assert
        actionResult.AssertResult<NotFoundResult>();

        await _plotpointService.Received(1).UpdatePlotPoint(plotPointId, plotPointUpdateDto);
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