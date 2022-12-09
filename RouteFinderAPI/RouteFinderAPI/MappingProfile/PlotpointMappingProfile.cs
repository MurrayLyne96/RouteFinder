using RouteFinderAPI.Models.API;

namespace RouteFinderAPI.Business.MappingProfile;

public class PlotpointMappingProfile : AutoMapper.Profile
{
    public PlotpointMappingProfile()
    {
        CreateMap<Plotpoint, PlotPointViewModel>();
        CreateMap<PlotPointCreateModel, Plotpoint>();
    }
}