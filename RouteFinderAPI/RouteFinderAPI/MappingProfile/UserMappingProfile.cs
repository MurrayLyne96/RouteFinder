namespace RouteFinderAPI.Business.MappingProfile;

public class UserMappingProfile : AutoMapper.Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserViewModel>();
        CreateMap<UserCreateViewModel, User>();
        CreateMap<User, UserDetailViewModel>();
        CreateMap<UserUpdateViewModel, User>();
    }
}