using System.Net;
using Microsoft.EntityFrameworkCore;
using RouteFinderAPI.Data.Entities;
using RouteFinderAPI.Data.Interfaces;
using RouteFinderAPI.Models.ViewModels;
using RouteFinderAPI.Services;

namespace RouteFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private IRouteService _routeService;
        private IPlotpointService _plotpointService;
        public RoutesController(IRouteService routeService, IPlotpointService plotpointService)
        {
            _routeService = routeService;
            _plotpointService = plotpointService;
        }
        
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<RouteViewModel>))]
        public async Task<ActionResult<List<RouteViewModel>>> GetAllRoutes()
        {
            var routes = await _routeService.GetAllRoutes();
            return Ok(routes);
        }

        [HttpGet]
        [Route("{routeId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RouteDetailViewModel))]
        public async Task<ActionResult<Route>> GetRouteById(Guid routeId)
        {
            var route = await _routeService.GetRouteById(routeId);
            return Ok(route);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreateRoute(RouteCreateViewModel model)
        {
            await _routeService.CreateRoute(model);
            return Created(this.Url.ToString(), model);
        }

        [HttpPut]
        [Route("{routeId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> UpdateRouteById(Guid routeId, RouteUpdateViewModel model)
        {
            await _routeService.UpdateRouteById(routeId, model);
            return NoContent();
        }

        [HttpDelete]
        [Route("{routeId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> DeleteRouteById(Guid routeId)
        {
            await _routeService.DeleteRouteById(routeId);
            return NoContent();
        }

        [HttpPost]
        [Route("{routeId:guid}/plotpoints")]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(PlotPointCreateModel))]
        public async Task<ActionResult> CreatePlotPoint(Guid routeId, PlotPointCreateModel model)
        {
            await _plotpointService.CreatePlotPoint(routeId, model);
            return Created(this.Url.ToString(), model);
        }

        [HttpPut]
        [Route("{routeId:guid}/plotpoints/{plotPointId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> UpdatePlotPoint(Guid routeId, Guid plotPointId, PlotPointCreateModel model)
        {
            await _plotpointService.UpdatePlotPoint(plotPointId, model);
            return NoContent();
        }

        [HttpDelete]
        [Route("{routeId:guid}/plotpoints/{plotPointId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> DeletePlotPoint(Guid routeId, Guid plotPointId)
        {
            await _plotpointService.DeletePlotPoint(plotPointId);
            return NoContent();
        }
        
    }
}
