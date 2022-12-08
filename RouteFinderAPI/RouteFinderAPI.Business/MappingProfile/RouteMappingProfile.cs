using RouteFinderAPI.Models.API;

namespace RouteFinderAPI.Business.MappingProfile;

public class RouteMappingProfile : AutoMapper.Profile
{
    public RouteMappingProfile()
    {
        CreateMap<MapRoute, RouteViewModel>().ReverseMap();
        CreateMap<MapRoute, RouteCreateViewModel>().ReverseMap();
        CreateMap<MapRoute, RouteUpdateViewModel>().ReverseMap();
    }
}