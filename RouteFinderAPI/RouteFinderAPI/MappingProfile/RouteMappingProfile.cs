
namespace RouteFinderAPI.Business.MappingProfile;

public class RouteMappingProfile : AutoMapper.Profile
{
    public RouteMappingProfile()
    {
        CreateMap<RouteDto, RouteViewModel>();
        CreateMap<RouteDetailDto, RouteDetailViewModel>()
            .ForMember(src => src.Type, dest => dest.MapFrom(y => y.Type));
        CreateMap<RouteCreateViewModel, RouteCreateDto>()
            .ForMember(src => src.Name, dest => dest.MapFrom(y => y.Name));
        CreateMap<RouteUpdateViewModel, RouteUpdateDto>();
    }
}