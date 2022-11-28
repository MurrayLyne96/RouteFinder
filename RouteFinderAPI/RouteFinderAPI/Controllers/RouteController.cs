namespace RouteFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        [HttpGet]
        [Route("/routes")]
        public void GetAllRoutes()
        {

        }

        [HttpGet]
        [Route("/routes/{routeId:guid}")]
        [ProducesResponseType(200)]
        public void GetRouteById(Guid routeId)
        {

        }

        [HttpPost]
        [Route("/routes")]
        [ProducesResponseType(202)]
        public void CreateRoute()
        {

        }

        [HttpPut]
        [Route("/routes/{routeId:guid}")]
        [ProducesResponseType(204)]
        public void UpdateRouteById(Guid routeId)
        {

        }

        [HttpDelete]
        [Route("/routes/{routeId:guid}")]
        [ProducesResponseType(204)]
        public void DeleteRouteById(Guid routeId)
        {

        }

        [HttpPost]
        [Route("/routes/{routeId:guid}/plotpoints")]
        [ProducesResponseType(202)]
        public void CreatePlotPoint(Guid routeId)
        {

        }

        [HttpPut]
        [Route("/routes/{routeId:guid}/plotpoints/{plotPointId:guid}")]
        [ProducesResponseType(204)]
        public void UpdatePlotpoint(Guid routeId, Guid plotPointId)
        {

        }

        [HttpDelete]
        [Route("/routes/{routeId:guid}/plotpoints/{plotPointId:guid}")]
        [ProducesResponseType(204)]
        public void DeletePlotPoint(Guid routeId, Guid plotPointId)
        {
            
        }
        
    }
}
