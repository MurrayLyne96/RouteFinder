using RouteFinderAPI.Services.Dto.Types;

namespace RouteFinderAPI.Business.MappingProfile;

public class TypeMappingDtoProfile : AutoMapper.Profile
{
    public TypeMappingDtoProfile()
    {
        CreateMap<RouteType, TypeDto>();
    }
}