namespace RouteFinderAPI.Business.MappingProfile;

public class UserDtoMappingProfile : AutoMapper.Profile
{
    public UserDtoMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserCreateDto, User>();
        CreateMap<User, UserDetailDto>();
        CreateMap<UserUpdateDto, User>();
    }
}