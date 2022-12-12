namespace RouteFinderAPI.Business.MappingProfile;

public class UserDtoMappingProfile : AutoMapper.Profile
{
    public UserDtoMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserCreateDto, User>()
            .ForMember(x => x.Created, y => y.MapFrom(z => DateTime.UtcNow))
            .ForMember(x => x.LastModified, y => y.MapFrom(z => DateTime.UtcNow));
        CreateMap<User, UserDetailDto>();
        CreateMap<UserUpdateDto, User>()
            .ForMember(x => x.LastModified, y => y.MapFrom(z => DateTime.UtcNow));
    }
}