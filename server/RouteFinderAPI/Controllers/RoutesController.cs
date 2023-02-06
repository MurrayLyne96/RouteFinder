using Microsoft.AspNetCore.Authorization;

namespace RouteFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : BaseController
    {
        private readonly IRouteService _routeService;
        private readonly IPlotpointService _plotpointService;
        private readonly IMapper _mapper;
        public RoutesController(IRouteService routeService, IPlotpointService plotpointService, IMapper mapper)
        {
            _routeService = routeService;
            _plotpointService = plotpointService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<RouteViewModel>))]
        public async Task<ActionResult<RouteViewModel[]>> GetAllRoutes()
        {
            var routes = await _routeService.GetAllRoutes();
            return OkOrNoListContent(_mapper.Map<RouteViewModel[]>(routes));
        }

        [HttpGet]
        [Route("{routeId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RouteDetailViewModel))]
        public async Task<ActionResult<RouteDetailViewModel>> GetRouteById(Guid routeId)
        {
            var route = await _routeService.GetRouteById(routeId);
            return OkOrNoNotFound(_mapper.Map<RouteDetailViewModel>(route));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreateRoute(RouteCreateViewModel model)
        {
            var routeCreateDto = _mapper.Map<RouteCreateDto>(model);
            var routeId = await _routeService.CreateRoute(routeCreateDto);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        [Route("{routeId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> UpdateRouteById(Guid routeId, RouteUpdateViewModel model)
        {
            var routeUpdateDto = _mapper.Map<RouteUpdateDto>(model);
            var result = await _routeService.UpdateRouteById(routeId, routeUpdateDto);
            return NoContentOrNoNotFound(result);
        }

        [HttpDelete]
        [Route("{routeId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> DeleteRouteById(Guid routeId)
        {
            var result = await _routeService.DeleteRouteById(routeId);
            return NoContentOrNoNotFound(result);
        }

        [HttpPost]
        [Route("{routeId:guid}/plotpoints")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreatePlotPoint(Guid routeId, PlotPointCreateModel model)
        {
            var plotpointCreateDto = _mapper.Map<PlotpointCreateDto>(model);
            plotpointCreateDto.MapRouteId = routeId;
            await _plotpointService.CreatePlotPoint(plotpointCreateDto);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        [Route("{routeId:guid}/plotpoints/{plotPointId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> UpdatePlotPoint(Guid routeId, Guid plotPointId, PlotPointCreateModel model)
        {
            var plotpointCreateDto = _mapper.Map<PlotpointUpdateDto>(model);
            plotpointCreateDto.MapRouteId = routeId;
            var result = await _plotpointService.UpdatePlotPoint(plotPointId, plotpointCreateDto);
            return NoContentOrNoNotFound(result);
        }

        [HttpDelete]
        [Route("{routeId:guid}/plotpoints/{plotPointId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> DeletePlotPoint(Guid routeId, Guid plotPointId)
        {
            var result = await _plotpointService.DeletePlotPoint(plotPointId);
            return NoContentOrNoNotFound(result);
        }
        
    }
}
