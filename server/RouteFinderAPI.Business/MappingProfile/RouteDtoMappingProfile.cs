using RouteFinderAPI.Models.API;

namespace RouteFinderAPI.Business.MappingProfile;

public class RouteDtoMappingProfile : AutoMapper.Profile
{
    public RouteDtoMappingProfile()
    {
        CreateMap<MapRoute, RouteDto>();
        CreateMap<MapRoute, RouteDetailDto>()
            .ForMember(src => src.Type, dest => dest.MapFrom(y => y.Type));
        CreateMap<RouteCreateDto, MapRoute>()
            .ForMember(src => src.RouteName, dest => dest.MapFrom(y => y.Name))
            .ForMember(x => x.Created, y => y.MapFrom(z => DateTime.UtcNow))
            .ForMember(x => x.LastModified, y => y.MapFrom(z => DateTime.UtcNow));
        CreateMap<RouteUpdateDto, MapRoute>()
            .ForMember(x => x.RouteName, y => y.MapFrom(z => z.Name))
            .ForMember(x => x.LastModified, y => y.MapFrom(z => DateTime.UtcNow));
    }
}