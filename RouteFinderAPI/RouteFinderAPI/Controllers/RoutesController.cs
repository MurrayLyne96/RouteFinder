using System.Net;
using RouteFinderAPI.Models.ViewModels;

namespace RouteFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<RouteViewModel>))]
        public ActionResult<List<RouteViewModel>> GetAllRoutes()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{routeId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RouteDetailViewModel))]
        public ActionResult<RouteViewModel> GetRouteById(Guid routeId)
        {
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public ActionResult CreateRoute(RouteCreateModel model)
        {
            return Created(this.Url.ToString(), new RouteViewModel());
        }

        [HttpPut]
        [Route("{routeId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public ActionResult UpdateRouteById(Guid routeId, RouteUpdateModel model)
        {
            return NoContent();
        }

        [HttpDelete]
        [Route("{routeId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public ActionResult DeleteRouteById(Guid routeId)
        {
            return NoContent();
        }

        [HttpPost]
        [Route("{routeId:guid}/plotpoints")]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(PlotPointCreateModel))]
        public ActionResult CreatePlotPoint(Guid routeId)
        {
            return Created(this.Url.ToString(), new PlotPointCreateModel());
        }

        [HttpPut]
        [Route("{routeId:guid}/plotpoints/{plotPointId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public ActionResult UpdatePlotPoint(Guid routeId, Guid plotPointId, RouteUpdateModel model)
        {
            return NoContent();
        }

        [HttpDelete]
        [Route("{routeId:guid}/plotpoints/{plotPointId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public ActionResult DeletePlotPoint(Guid routeId, Guid plotPointId)
        {
            return NoContent();
        }
        
    }
}
