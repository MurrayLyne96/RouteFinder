using RouteFinderAPI.Models.API;
using RouteFinderAPI.Services.Dto.Plotpoints;

namespace RouteFinderAPI.Business.MappingProfile;

public class PlotpointDtoMappingProfile : AutoMapper.Profile
{
    public PlotpointDtoMappingProfile()
    {
        CreateMap<Plotpoint, PlotpointDto>();
        CreateMap<PlotpointCreateDto, Plotpoint>()
            .ForMember(x => x.Id, y => y.MapFrom(z => Guid.NewGuid()))
            .ForMember(x => x.Created, y => y.MapFrom(z => DateTime.UtcNow))
            .ForMember(x => x.LastModified, y => y.MapFrom(z => DateTime.UtcNow));
    }
}