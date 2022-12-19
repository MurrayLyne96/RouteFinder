using NSubstitute.ReturnsExtensions;
using RouteFinderAPI.Services.Dto.Users;

namespace RouteFinderAPI.Test.Controllers;

public class UserControllerTests
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserControllerTests()
    {
        _userService = Substitute.For<IUserService>();
        _mapper = Substitute.For<IMapper>();
    }

    [Fact]
    public async Task GetAllUsers_WhenUsersFound_ReturnsOkResult()
    {
        // Arrange
        var userDtos = new UserDto[] { new UserDto() };
        var userViewModels = new UserViewModel[] { new UserViewModel() };

        _userService.GetAllUsers().Returns(userDtos);
        _mapper.Map<UserViewModel[]>(userDtos).Returns(userViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.GetAllUsers();
        
        // Assert
        var result = actionResult.AssertObjectResult<UserViewModel[], OkObjectResult>();

        result.Should().BeSameAs(userViewModels);

        await _userService.Received(1).GetAllUsers();
        _mapper.Received(1).Map<UserViewModel[]>(userDtos);
    }

    [Fact]
    public async Task GetAllUsers_WhenUsersNotFound_ReturnNoListContent()
    {
        // Arrange
        var userDtos = new UserDto[0];
        var userViewModels = new UserViewModel[0];

        _userService.GetAllUsers().Returns(userDtos);
        _mapper.Map<UserViewModel[]>(userDtos).Returns(userViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.GetAllUsers();
        
        // Assert
        var result = actionResult.AssertObjectResult<UserViewModel[], NoContentResult>();

        result.Should().BeSameAs(userViewModels);

        await _userService.Received(1).GetAllUsers();
        _mapper.Received(1).Map<UserViewModel[]>(userDtos);
    }
    
    [Fact]
    public async Task GetUser_WhenUserFound_ReturnOkContent()
    {
        // Arrange
        var userDto = new UserDetailDto();
        var userViewModel = new UserDetailViewModel();

        _userService.GetUserById(Guid.NewGuid()).Returns(userDto);
        _mapper.Map<UserDetailViewModel>(userDto).Returns(userViewModel);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.GetUser(Guid.NewGuid());
        
        // Assert
        var result = actionResult.AssertObjectResult<UserDetailViewModel, OkObjectResult>();

        result.Should().BeSameAs(userViewModel);

        await _userService.Received(1).GetUserById(Guid.NewGuid());
        _mapper.Received(1).Map<UserViewModel[]>(userDto);
    }
    
    [Fact]
    public async Task GetUser_WhenUserNotFound_ReturnNotFound()
    {
        // Arrange
        var userDto = new UserDetailDto();
        var userViewModel = new UserDetailViewModel();

        _userService.GetUserById(Guid.NewGuid()).ReturnsNull();
        _mapper.Map<UserDetailViewModel>(userDto).ReturnsNull();

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.GetUser(Guid.NewGuid());
        
        // Assert
        var result = actionResult.AssertObjectResult<UserDetailViewModel, NotFoundResult>();

        await _userService.Received(1).GetUserById(Guid.NewGuid());
        _mapper.Received(1).Map<UserViewModel[]>(userDto);
    }

    [Fact]
    public async Task GetRoutesFromUser_WhenRoutesAreFound_ReturnOkResult()
    {
        // Arrange
        var routeDtos = new RouteDto[] { new RouteDto() };
        var routeViewModels = new RouteViewModel[] { new RouteViewModel() };

        _userService.GetRoutesFromUser(Guid.NewGuid()).Returns(routeDtos);
        _mapper.Map<RouteViewModel[]>(routeDtos).Returns(routeViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.GetRoutesFromUser(Guid.NewGuid());
        
        // Assert
        var result = actionResult.AssertObjectResult<RouteViewModel[], OkObjectResult>();

        result.Should().BeSameAs(routeViewModels);

        await _userService.Received(1).GetRoutesFromUser(Guid.NewGuid());
        _mapper.Received(1).Map<RouteViewModel[]>(routeDtos);
    }
    
    [Fact]
    public async Task GetRoutesFromUser_WhenNoRoutesAreFound_ReturnNoContentResult()
    {
        // Arrange
        var routeDtos = new RouteDto[] { new RouteDto() };
        var routeViewModels = new RouteViewModel[] { new RouteViewModel() };

        _userService.GetRoutesFromUser(Guid.NewGuid()).Returns(routeDtos);
        _mapper.Map<RouteViewModel[]>(routeDtos).Returns(routeViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.GetRoutesFromUser(Guid.NewGuid());
        
        // Assert
        var result = actionResult.AssertObjectResult<RouteViewModel[], OkObjectResult>();

        result.Should().BeSameAs(routeViewModels);

        await _userService.Received(1).GetRoutesFromUser(Guid.NewGuid());
        _mapper.Received(1).Map<RouteViewModel[]>(routeDtos);
    }
    
    [Fact]
    public async Task CreateUser_WhenUserIsCreated_ReturnCreatedResult()
    {
        // Arrange
        var userCreateDto = new UserCreateDto();
        var userViewModel = new UserCreateViewModel();

        _userService.CreateUser(userCreateDto).ReturnsNull();
        _mapper.Map<UserCreateDto>(userViewModel).Returns(userCreateDto);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.CreateUser(userViewModel);
        
        // Assert
        actionResult.AssertObjectResult<Guid, CreatedResult>();

        await _userService.Received(1).CreateUser(userCreateDto);
        _mapper.Received(1).Map<UserCreateDto>(userViewModel);
    }
    
    [Fact]
    public async Task UpdateUser_WhenUserIsUpdated_ReturnNoContentResult()
    {
        // Arrange
        var userUpdateDto = new UserUpdateDto();
        var userViewModel = new UserUpdateViewModel();

        _userService.UpdateUser(Guid.NewGuid(), userUpdateDto).Returns(true);
        _mapper.Map<UserUpdateDto>(userViewModel).Returns(userUpdateDto);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.UpdateUser(Guid.NewGuid(), userViewModel);
        
        // Assert
        actionResult.AssertResult<NoContentResult>();

        await _userService.Received(1).UpdateUser(Guid.NewGuid(), userUpdateDto);
        _mapper.Received(1).Map<UserUpdateDto>(userViewModel);
    }
    
    [Fact]
    public async Task UpdateUser_WhenUserIsNotUpdated_ReturnNoContentResult()
    {
        // Arrange
        var userUpdateDto = new UserUpdateDto();
        var userViewModel = new UserUpdateViewModel();

        _userService.UpdateUser(Guid.NewGuid(), userUpdateDto).Returns(false);
        _mapper.Map<UserUpdateDto>(userViewModel).Returns(userUpdateDto);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.UpdateUser(Guid.NewGuid(), userViewModel);
        
        // Assert
        actionResult.AssertResult<NotFoundResult>();

        await _userService.Received(1).UpdateUser(Guid.NewGuid(), userUpdateDto);
        _mapper.Received(1).Map<UserUpdateDto>(userViewModel);
    }
    
    private UsersController RetrieveController()
    {
        return new UsersController(_userService, _mapper);
    }
}