namespace RouteFinderAPI.Business.MappingProfile;

public class UserMappingProfile : AutoMapper.Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserViewModel>();
        CreateMap<User, UserCreateViewModel>();
        CreateMap<User, UserUpdateViewModel>();
    }
}