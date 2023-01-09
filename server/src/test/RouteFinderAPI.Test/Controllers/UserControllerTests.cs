using System.Net;
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
        var usermodel = new UserViewModel();
        var userDtos = new UserDto[] { new UserDto() };
        var userViewModels = new UserViewModel[] { usermodel };

        _userService.GetAllUsers().Returns(userDtos);
        _mapper.Map<UserViewModel>(Arg.Any<UserDto>()).Returns(usermodel);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.GetAllUsers();
        
        // Assert
        var result = actionResult.AssertObjectResult<UserViewModel[], OkObjectResult>();

        await _userService.Received(1).GetAllUsers();
        _mapper.Received(1).Map<UserViewModel>(Arg.Any<UserDto>());
    }

    [Fact]
    public async Task GetAllUsers_WhenUsersNotFound_ReturnNoListContent()
    {
        // Arrange
        _userService.GetAllUsers().Returns(Array.Empty<UserDto>());
        _mapper.Map<UserViewModel[]>(Array.Empty<UserDto>()).Returns(Array.Empty<UserViewModel>());

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.GetAllUsers();
        
        // Assert
        actionResult.AssertResult<UserViewModel[], NoContentResult>();

        await _userService.Received(1).GetAllUsers();
    }
    
    [Fact]
    public async Task GetUser_WhenUserFound_ReturnOkContent()
    {
        // Arrange
        var userDto = new UserDetailDto();
        var userViewModel = new UserDetailViewModel();
        var userId = Guid.NewGuid();
        
        _userService.GetUserById(userId).Returns(userDto);
        _mapper.Map<UserDetailViewModel>(userDto).Returns(userViewModel);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.GetUser(userId);
        
        // Assert
        var result = actionResult.AssertObjectResult<UserDetailViewModel, OkObjectResult>();

        result.Should().BeSameAs(userViewModel);

        await _userService.Received(1).GetUserById(userId);
        _mapper.Received(1).Map<UserDetailViewModel>(userDto);
    }
    
    [Fact]
    public async Task GetUser_WhenUserNotFound_ReturnNotFound()
    {
        // Arrange
        var userDto = new UserDetailDto();
        var userViewModel = new UserDetailViewModel();
        var userId = Guid.NewGuid();
        
        _userService.GetUserById(userId).ReturnsNull();
        _mapper.Map<UserDetailViewModel>(userDto).ReturnsNull();

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.GetUser(userId);
        
        // Assert
        actionResult.AssertResult<UserDetailViewModel, NotFoundResult>();

        await _userService.Received(1).GetUserById(userId);
    }

    [Fact]
    public async Task GetRoutesFromUser_WhenRoutesAreFound_ReturnOkResult()
    {
        // Arrange
        var routeDtos = new RouteDto[] { new RouteDto() };
        var routeViewModels = new RouteViewModel[] { new RouteViewModel() };
        var userId = Guid.NewGuid();
        _userService.GetRoutesFromUser(userId).Returns(routeDtos);
        _mapper.Map<RouteViewModel[]>(routeDtos).Returns(routeViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.GetRoutesFromUser(userId);
        
        // Assert
        var result = actionResult.AssertObjectResult<RouteViewModel[], OkObjectResult>();

        result.Should().BeSameAs(routeViewModels);

        await _userService.Received(1).GetRoutesFromUser(userId);
        _mapper.Received(1).Map<RouteViewModel[]>(routeDtos);
    }
    
    [Fact]
    public async Task GetRoutesFromUser_WhenNoRoutesAreFound_ReturnNoContentResult()
    {
        // Arrange
        var routeDtos = new RouteDto[] { new RouteDto() };
        var routeViewModels = new RouteViewModel[] { new RouteViewModel() };
        var userId = Guid.NewGuid();
        _userService.GetRoutesFromUser(Guid.NewGuid()).Returns(routeDtos);
        _mapper.Map<RouteViewModel[]>(routeDtos).Returns(routeViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.GetRoutesFromUser(userId);
        
        // Assert
        actionResult.AssertResult<RouteViewModel[], NoContentResult>();
        
        await _userService.Received(1).GetRoutesFromUser(userId);
    }
    
    [Fact]
    public async Task CreateUser_WhenUserIsCreated_ReturnCreatedResult()
    {
        // Arrange
        var userCreateDto = new UserCreateDto();
        var userViewModel = new UserCreateViewModel();

        _userService.CreateUser(userCreateDto).Returns(Guid.NewGuid());
        _mapper.Map<UserCreateDto>(userViewModel).Returns(userCreateDto);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.CreateUser(userViewModel);
        Guid guidValue;
        var isGuid = Guid.TryParse(actionResult.Value.ToString(), out guidValue);
        
        // Assert
        actionResult.StatusCode.Should().Be((int)HttpStatusCode.Created);

        isGuid.Should().Be(true);

        await _userService.Received(1).CreateUser(userCreateDto);
        _mapper.Received(1).Map<UserCreateDto>(userViewModel);
    }
    
    [Fact]
    public async Task UpdateUser_WhenUserIsUpdated_ReturnNoContentResult()
    {
        // Arrange
        var userUpdateDto = new UserUpdateDto();
        var userViewModel = new UserUpdateViewModel();
        var userId = Guid.NewGuid();
        
        _userService.UpdateUser(userId, userUpdateDto).Returns(true);
        _mapper.Map<UserUpdateDto>(userViewModel).Returns(userUpdateDto);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.UpdateUser(userId, userViewModel);
        
        // Assert
        actionResult.AssertResult<NoContentResult>();

        await _userService.Received(1).UpdateUser(userId, userUpdateDto);
        _mapper.Received(1).Map<UserUpdateDto>(userViewModel);
    }
    
    [Fact]
    public async Task UpdateUser_WhenUserIsNotUpdated_ReturnNoContentResult()
    {
        // Arrange
        var userUpdateDto = new UserUpdateDto();
        var userViewModel = new UserUpdateViewModel();
        var userId = Guid.NewGuid();
        
        _userService.UpdateUser(userId, userUpdateDto).Returns(false);
        _mapper.Map<UserUpdateDto>(userViewModel).Returns(userUpdateDto);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.UpdateUser(userId, userViewModel);
        
        // Assert
        actionResult.AssertResult<NotFoundResult>();

        await _userService.Received(1).UpdateUser(userId, userUpdateDto);
        _mapper.Received(1).Map<UserUpdateDto>(userViewModel);
    }
    
    [Fact]
    public async Task DeleteUser_WhenUserIsDeleted_ReturnNoContentResult()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userService.DeleteUser(userId).Returns(true);
        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.DeleteUser(userId);
        
        // Assert
        actionResult.AssertResult<NoContentResult>();

        await _userService.Received(1).DeleteUser(userId);
    }
    
    [Fact]
    public async Task DeleteUser_WhenUserIsNotDeleted_ReturnNotFoundResult()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userService.DeleteUser(Guid.NewGuid()).Returns(false);
        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.DeleteUser(userId);
        
        // Assert
        actionResult.AssertResult<NotFoundResult>();

        await _userService.Received(1).DeleteUser(userId);
    }
    
    private UsersController RetrieveController()
    {
        return new UsersController(_userService, _mapper);
    }
}