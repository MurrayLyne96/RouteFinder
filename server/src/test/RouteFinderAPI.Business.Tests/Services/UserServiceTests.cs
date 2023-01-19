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

    [Fact]
    public async Task CreateUser_WhenUserIsCreated_ReturnGuid() {
        // Arrange
        var guid = Guid.NewGuid();
        var roleGuid = Guid.NewGuid();
        var user = _fixture.Build<User>().With(x => x.Id, guid).Without(x => x.Password).Create();
        var userDto = _fixture.Create<UserCreateDto>();
        var role = _fixture.Build<Role>().With(x => x.Id, roleGuid).Create();
        
        var roles = _fixture.CreateMany<Role>(10).ToList();
        roles.Add(role);
        var rolesQuery = roles.AsQueryable().BuildMock();
        
        userDto.Password = "testpassword";

        _mapper.Map<User>(Arg.Any<UserCreateDto>()).Returns(user);
        var service = RetrieveService();

        _database.Get<Role>().Returns(rolesQuery);
        _database.SaveChangesAsync().Returns(1);

        // Act
        var result = await service.CreateUser(userDto);
        
        // Assert
        result.Should().Be(guid);
        _mapper.Received(1).Map<User>(Arg.Any<UserCreateDto>());
        await _database.Received(1).AddAsync(Arg.Any<User>());
        await _database.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task GetRoutesFromUser_WhenRoutesFound_ReturnRoutes()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var routes = _fixture.Build<MapRoute>()
        .With(x => x.UserId, userId)
        .CreateMany<MapRoute>(10)
        .ToList()
        .AsQueryable()
        .BuildMock();

        var routeDtos = _fixture
        .Build<RouteDto>()
        .With(x => x.UserId, userId)
        .CreateMany<RouteDto>(10)
        .ToList()
        .AsQueryable()
        .BuildMock();
        
        _database.Get<MapRoute>().Returns(routes);

        _mapper.ProjectTo<RouteDto>(Arg.Any<IQueryable<MapRoute>>()).Returns(routeDtos);
        var service = RetrieveService();

        // Act
        var result = await service.GetRoutesFromUser(userId);

        // Assert
        result.Should().BeEquivalentTo(routeDtos);
        _mapper.Received(1).ProjectTo<RouteDto>(Arg.Is<IQueryable<MapRoute>>(x => x.Count() == 10));
    }

    [Fact]
    public async Task UpdateUser_WhenUserIsUpdated_ReturnTrue()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = _fixture.Build<User>().With(x => x.Id, userId).Create();

        var users = _fixture.CreateMany<User>(10).ToList();
        users.Add(user);
        var usersQuery = users.AsQueryable().BuildMock();

        var userUpdatedDto = _fixture.Create<UserUpdateDto>();

        _database.Get<User>().Returns(usersQuery);

        var service = RetrieveService();

        // Act
        var result = await service.UpdateUser(userId, userUpdatedDto);

        // Assert
        result.Should().Be(true);
        _mapper.Received(1).Map(Arg.Any<UserUpdateDto>(), Arg.Any<User>());
        await _database.Received(1).SaveChangesAsync();
    }

    public async Task UpdateUser_WhenUserIsUpdatedWithNoPassword_ReturnTrue()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = _fixture.Build<User>().With(x => x.Id, userId).Create();

        var users = _fixture.CreateMany<User>(10).ToList();
        users.Add(user);
        var usersQuery = users.AsQueryable().BuildMock();

        var userUpdatedDto = _fixture.Build<UserUpdateDto>().Without(x => x.Password).Create();

        _database.Get<User>().Returns(usersQuery);

        var service = RetrieveService();

        // Act
        var result = await service.UpdateUser(userId, userUpdatedDto);

        // Assert
        result.Should().Be(true);
        _mapper.Received(1).Map(Arg.Any<UserUpdateDto>(), Arg.Any<User>());
        await _database.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task UpdateUser_WhenUserIsNotUpdated_ReturnFalse()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var users = _fixture.CreateMany<User>(10).ToList();
        var usersQuery = users.AsQueryable().BuildMock();

        var userUpdatedDto = _fixture.Create<UserUpdateDto>();

        _database.Get<User>().Returns(usersQuery);

        var service = RetrieveService();

        // Act
        var result = await service.UpdateUser(userId, userUpdatedDto);

        // Assert
        result.Should().Be(false);
    }

    [Fact]
    public async Task DeleteUser_WhenUserIsDeleted_ReturnTrue()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = _fixture.Build<User>().With(x => x.Id, userId).Create();

        var users = _fixture.CreateMany<User>(10).ToList();
        users.Add(user);
        var usersQuery = users.AsQueryable().BuildMock();

        _database.Get<User>().Returns(usersQuery);

        var service = RetrieveService();

        // Act
        var result = await service.DeleteUser(userId);

        // Assert
        result.Should().Be(true);
        _database.Received(1).Delete(Arg.Any<User>());
        await _database.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task DeleteUser_WhenUserIsNotDeleted_ReturnFalse()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var users = _fixture.CreateMany<User>(10).ToList();
        var usersQuery = users.AsQueryable().BuildMock();

        _database.Get<User>().Returns(usersQuery);

        var service = RetrieveService();

        // Act
        var result = await service.DeleteUser(userId);

        // Assert
        result.Should().Be(false);
    }
    
    
    
    private UserService RetrieveService()
    {
        return new UserService(_database, _mapper);
    }
}