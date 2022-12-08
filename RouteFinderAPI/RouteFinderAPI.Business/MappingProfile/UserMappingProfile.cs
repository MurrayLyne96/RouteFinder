namespace RouteFinderAPI.Business.MappingProfile;

public class UserMappingProfile : AutoMapper.Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserViewModel>().ReverseMap();
        CreateMap<User, UserCreateViewModel>().ReverseMap();
        CreateMap<User, UserDetailViewModel>().ReverseMap();
        CreateMap<User, UserUpdateViewModel>().ReverseMap();
    }
}