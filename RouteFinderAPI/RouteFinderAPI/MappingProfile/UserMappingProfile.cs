using RouteFinderAPI.Data.Entities;

namespace RouteFinderAPI.Business.MappingProfile;

public class UserMappingProfile : AutoMapper.Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserDto, UserViewModel>();
        CreateMap<UserDto, UserViewModel>();
        CreateMap<UserCreateViewModel, UserCreateDto>();
        CreateMap<UserDetailDto, UserDetailViewModel>();
        CreateMap<UserUpdateViewModel, UserUpdateDto>();
    }
}