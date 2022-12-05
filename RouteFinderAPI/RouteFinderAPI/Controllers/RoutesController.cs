using System.Net;
using Microsoft.EntityFrameworkCore;
using RouteFinderAPI.Data.Entities;
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
            var routes = await _database.Get<MapRoute>().ToListAsync();
            return Ok(routes);
        }

        [HttpGet]
        [Route("{routeId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RouteDetailViewModel))]
        public async Task<ActionResult<Route>> GetRouteById(Guid routeId)
        {
            var route = await _database.Get<MapRoute>().Where(x => x.Id == routeId).SingleOrDefaultAsync();
            return Ok(route);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreateRoute(RouteCreateViewModel model)
        {
            var routeEntity = new MapRoute()
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
            var routeEntity = await _database.Get<MapRoute>().Where(x => x.Id == routeId).SingleOrDefaultAsync();
            
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
            var routeEntity = await _database.Get<MapRoute>().Where(x => x.Id == routeId).SingleOrDefaultAsync();
            
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
        public async Task<ActionResult> CreatePlotPoint(Guid routeId, PlotPointCreateModel model)
        {
            var plotPointEntity = new Plotpoint()
            {
                Id = Guid.NewGuid(),
                XCoordinate = model.XCoordinate,
                YCoordinate = model.YCoordinate,
                PointDescription = model.Description,
                PlotOrder = model.PlotOrder,
                Created = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };

            await _database.AddAsync(plotPointEntity);
            return Created(this.Url.ToString(), model);
        }

        [HttpPut]
        [Route("{routeId:guid}/plotpoints/{plotPointId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> UpdatePlotPoint(Guid routeId, Guid plotPointId, PlotPointCreateModel model)
        {
            var plotPointEntity = await _database.Get<Plotpoint>().Where(x => x.Id == plotPointId).SingleOrDefaultAsync();
            
            plotPointEntity.XCoordinate = model.XCoordinate;
            plotPointEntity.YCoordinate = model.YCoordinate;
            plotPointEntity.PointDescription = model.Description;
            plotPointEntity.PlotOrder = model.PlotOrder;
            plotPointEntity.LastModified = DateTime.UtcNow;

            await _database.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        [Route("{routeId:guid}/plotpoints/{plotPointId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> DeletePlotPoint(Guid routeId, Guid plotPointId)
        {
            var plotPointEntity = await _database.Get<Plotpoint>().Where(x => x.Id == plotPointId).SingleOrDefaultAsync();
            _database.Delete(plotPointEntity);
            await _database.SaveChangesAsync();
            return NoContent();
        }
        
    }
}
