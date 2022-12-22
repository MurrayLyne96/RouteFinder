using NSubstitute.ReturnsExtensions;
using RouteFinderAPI.Services.Dto.Users;

namespace RouteFinderAPI.Business.Tests.Services;

public class UserServiceTests
{
    private readonly IRouteFinderDatabase _database;
    private readonly IMapper _mapper;
    private readonly IFixture _fixture;
    public UserServiceTests()
    {
        _database = Substitute.For<IRouteFinderDatabase>();
        _mapper = Substitute.For<IMapper>();
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
    }

    [Fact]
    public async Task GetAllUsers_WhenUsersFound_ReturnUsers()
    {
        // Arrange
        var users = _fixture.CreateMany<User>(10).
            ToList()
            .AsQueryable()
            .BuildMock();
        
        var userDtos = _fixture.CreateMany<UserDto>(10).
            ToList()
            .AsQueryable()
            .BuildMock();
        
        _database.Get<User>().Returns(users);

        _mapper.ProjectTo<UserDto>(Arg.Any<IQueryable<User>>()).Returns(userDtos);
        var service = RetrieveService();
        
        // Act
        var result = await service.GetAllUsers();
        
        // Assert
        result.Should().BeEquivalentTo(userDtos);
        _mapper.Received(1).ProjectTo<UserDto>(Arg.Is<IQueryable<User>>(x => x.Count() == 10));
    }
    
    [Fact]
    public async Task GetUser_WhenUserFound_ReturnUser()
    {
        // Arrange
        var users = _fixture.CreateMany<User>(10).
            ToList()
            .AsQueryable()
            .BuildMock();
        var userToRetrieve = users.AsQueryable().First();
        
        var userDto = _fixture.Create<UserDetailDto>();
        userDto.Id = userToRetrieve.Id;
        
        var userDtos = _fixture.CreateMany<UserDetailDto>(10).ToList();
        userDtos.Add(userDto);
        var userDtoQuery = userDtos.AsQueryable().BuildMock();
        
        _database.Get<User>().Returns(users);

        _mapper.ProjectTo<UserDetailDto>(Arg.Any<IQueryable<User>>()).Returns(userDtoQuery.Where(x => x.Id == userDto.Id));
        
        var service = RetrieveService();
        
        // Act
        var result = await service.GetUserById(userToRetrieve.Id);
        
        // Assert
        result.Should().BeEquivalentTo(userDto);
        _mapper.Received(1).ProjectTo<UserDetailDto>(Arg.Is<IQueryable<User>>(x => x.Count() == 1));
    }
    
    [Fact]
    public async Task GetUser_WhenUserNotFound_ReturnNull()
    {
        // Arrange
        var users = _fixture.CreateMany<User>(10).
            ToList()
            .AsQueryable()
            .BuildMock();
        
        var userDtos = _fixture.CreateMany<UserDetailDto>(10).ToList();
        var userDtoQuery = userDtos.AsQueryable().BuildMock();
        
        _database.Get<User>().Returns(users);

        _mapper.ProjectTo<UserDetailDto>(Arg.Any<IQueryable<User>>()).Returns(userDtoQuery.Where(x => x.Id == Guid.NewGuid()));
        
        var service = RetrieveService();
        
        // Act
        var result = await service.GetUserById(Guid.NewGuid());
        
        // Assert
        result.Should().BeNull();
        _mapper.Received(1).ProjectTo<UserDetailDto>(Arg.Is<IQueryable<User>>(x => x.Count() == 0));
    }
    
    
    
    private UserService RetrieveService()
    {
        return new UserService(_database, _mapper);
    }
}