using RouteFinderAPI.Models.ViewModels.Roles;

namespace RouteFinderAPI.Business.MappingProfile;

public class RoleDtoMappingProfile : AutoMapper.Profile
{
    public RoleDtoMappingProfile()
    {
        CreateMap<RoleDto, RoleViewModel>();
        CreateMap<Role, RoleDto>();
    }
}