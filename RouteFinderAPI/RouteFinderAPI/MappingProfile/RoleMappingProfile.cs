using RouteFinderAPI.Models.ViewModels.Roles;
using RouteFinderAPI.Services.Dto.Roles;

namespace RouteFinderAPI.Business.MappingProfile;

public class RoleDtoMappingProfile : AutoMapper.Profile
{
    public RoleDtoMappingProfile()
    {
        CreateMap<RoleViewModel, RoleDto>();
        CreateMap<RoleDto, RoleViewModel>();
    }
}