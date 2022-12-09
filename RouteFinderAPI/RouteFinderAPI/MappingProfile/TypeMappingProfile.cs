namespace RouteFinderAPI.Business.MappingProfile;

public class TypeMappingProfile : AutoMapper.Profile
{
    public TypeMappingProfile()
    {
        CreateMap<RouteType, TypeViewModel>();
    }
}