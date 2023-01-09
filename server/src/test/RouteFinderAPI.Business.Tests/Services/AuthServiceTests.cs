using RouteFinderAPI.Services.Dto.Users;

namespace RouteFinderAPI.Business.Tests.Services;

public class AuthServiceTests
{
    private readonly IRouteFinderDatabase _database;
    private readonly IMapper _mapper;
    private readonly IFixture _fixture;
    
    public AuthServiceTests()
    {
        _database = Substitute.For<IRouteFinderDatabase>();
        _mapper = Substitute.For<IMapper>();
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
    }
    
    [Fact]
    public async Task Authenticate_WhenUserIsAuthenticated_ReturnUser()
    {
        // Arrange
        var userToAuthenticate = _fixture.Create<User>();
        string password = "password01";
        userToAuthenticate.Password = BCrypt.Net.BCrypt.HashPassword(password);

        
        var userDto = _fixture.Create<UserDto>();
        var users = _fixture.CreateMany<User>(10).ToList();
        users.Add(userToAuthenticate);
        var userQuery = users.AsQueryable().BuildMock();
        
        _database.Get<User>().Returns(userQuery);
        _mapper.Map<UserDto?>(Arg.Any<User>()).Returns(userDto);
        
        // Act
        var service = RetrieveService();
        var result = await service.Authenticate(userToAuthenticate.Email, password);

        // Assert
        result.Should().NotBeNull();
        _mapper.Received(1).Map<UserDto?>(Arg.Any<User>());
        _database.Received(1).Get<User>();

    }
    
    [Fact]
    public async Task Authenticate_WhenUserIsNotAuthenticated_ReturnNull()
    {
        // Arrange
        var userToAuthenticate = _fixture.Create<User>();
        string password = "password01";
        userToAuthenticate.Password = BCrypt.Net.BCrypt.HashPassword("HE_IS_NOT_THE_CORRECT_PASSWORD_HE_IS_A_VERY_NAUGHTY_BOY");

        
        var userDto = _fixture.Create<UserDto>();
        var users = _fixture.CreateMany<User>(10).ToList();
        users.Add(userToAuthenticate);
        var userQuery = users.AsQueryable().BuildMock();
        
        _database.Get<User>().Returns(userQuery);
        _mapper.Map<UserDto?>(Arg.Any<User>()).Returns(userDto);
        
        // Act
        var service = RetrieveService();
        var result = await service.Authenticate(userToAuthenticate.Email, password);

        // Assert
        result.Should().BeNull();
        _database.Received(1).Get<User>();

    }
    
    private AuthService RetrieveService()
    {
        return new AuthService(_database, _mapper);
    }
}