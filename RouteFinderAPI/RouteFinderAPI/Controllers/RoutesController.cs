using System.Net;
using Microsoft.EntityFrameworkCore;
using RouteFinderAPI.Data.Interfaces;
using RouteFinderAPI.Models.ViewModels;

namespace RouteFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private IRouteFinderDatabase _database;
        public RoutesController(IRouteFinderDatabase database) => _database = database;
        
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<RouteViewModel>))]
        public async Task<ActionResult<List<RouteViewModel>>> GetAllRoutes()
        {
            var routes = await _database.Get<Route>().ToListAsync();
            return Ok(routes);
        }

        [HttpGet]
        [Route("{routeId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RouteDetailViewModel))]
        public async Task<ActionResult<Route>> GetRouteById(Guid routeId)
        {
            var route = await _database.Get<Data.Entities.Route>().Where(x => x.Id == routeId).SingleOrDefaultAsync();
            return Ok(route);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreateRoute(RouteCreateViewModel model)
        {
            var routeEntity = new Data.Entities.Route()
            {
                Id = Guid.NewGuid(),
                RouteName = model.Name,
                Created = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                TypeId = model.TypeId,
                UserId = model.UserId
            };
            await _database.AddAsync(routeEntity);
            return Created(this.Url.ToString(), routeEntity);
        }

        [HttpPut]
        [Route("{routeId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> UpdateRouteById(Guid routeId, RouteUpdateViewModel model)
        {
            var routeEntity = await _database.Get<Data.Entities.Route>().Where(x => x.Id == routeId).SingleOrDefaultAsync();
            
            if (routeEntity is null)
            {
                return NotFound();
            }
            
            routeEntity.RouteName = model.Name;
            routeEntity.TypeId = model.TypeId;

            await _database.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        [Route("{routeId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> DeleteRouteById(Guid routeId)
        {
            var routeEntity = await _database.Get<Data.Entities.Route>().Where(x => x.Id == routeId).SingleOrDefaultAsync();
            
            if (routeEntity is null)
            {
                return NotFound();
            }
            
            _database.Delete(routeEntity);
            _database.SaveChangesAsync();
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
        public ActionResult UpdatePlotPoint(Guid routeId, Guid plotPointId, PlotPointCreateModel model)
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
