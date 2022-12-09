using RouteFinderAPI.Models.API;

namespace RouteFinderAPI.Business.MappingProfile;

public class RouteMappingProfile : AutoMapper.Profile
{
    public RouteMappingProfile()
    {
        CreateMap<MapRoute, RouteViewModel>();
        CreateMap<MapRoute, RouteDetailViewModel>()
            .ForMember(src => src.Type, dest => dest.MapFrom(y => y.Type));
        CreateMap<RouteCreateViewModel, MapRoute>()
            .ForMember(src => src.RouteName, dest => dest.MapFrom(y => y.Name));
        CreateMap<RouteUpdateViewModel, MapRoute>();
    }
}