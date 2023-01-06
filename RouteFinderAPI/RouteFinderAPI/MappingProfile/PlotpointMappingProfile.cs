
namespace RouteFinderAPI.Business.MappingProfile;

public class PlotpointMappingProfile : AutoMapper.Profile
{
    public PlotpointMappingProfile()
    {
        CreateMap<PlotpointDto, PlotPointViewModel>();
        CreateMap<PlotPointCreateModel, PlotpointCreateDto>();
        CreateMap<PlotPointCreateModel, PlotpointUpdateDto>();

    }
}