using RouteFinderAPI.Models.API;
using RouteFinderAPI.Services.Dto.Plotpoints;

namespace RouteFinderAPI.Business.MappingProfile;

public class PlotpointDtoMappingProfile : AutoMapper.Profile
{
    public PlotpointDtoMappingProfile()
    {
        CreateMap<Plotpoint, PlotpointDto>();
        CreateMap<PlotpointCreateDto, Plotpoint>();
    }
}