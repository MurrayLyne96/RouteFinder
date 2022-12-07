using RouteFinderAPI.Models.API;

namespace RouteFinderAPI.Business.MappingProfile;

public class RouteMappingProfile : AutoMapper.Profile
{
    public RouteMappingProfile()
    {
        CreateMap<MapRoute, RouteViewModel>();
        CreateMap<MapRoute, RouteCreateViewModel>();
        CreateMap<MapRoute, RouteUpdateViewModel>();
    }
}