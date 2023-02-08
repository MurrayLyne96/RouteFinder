using NSubstitute.ReturnsExtensions;
using RouteFinderAPI.Authentication;
using RouteFinderAPI.Services.Dto.Roles;
using RouteFinderAPI.Services.Dto.Users;

namespace RouteFinderAPI.Test.Controllers;

public class AuthControllerTests
{
    private readonly IAuthService _authService;
    private readonly IAuthorizedAccountProvider _authProvider;

    public AuthControllerTests()
    {
        _authService = Substitute.For<IAuthService>();
        _authProvider = Substitute.For<IAuthorizedAccountProvider>();
    }

    [Fact]
    public async Task Authenticate_WhenAuthenticated_ReturnsOk()
    {
        // Arrange
        var model = new UserAuthModel();
        model.Email = "test@test.com";
        model.Password = "password";
        var user = new UserDto();
        user.Email = "test@test.com";
        user.Id = Guid.NewGuid();
        user.Role = new RoleDto();
        user.Role.RoleName = "USR";

        _authService.Authenticate(model.Email, model.Password).Returns(user);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.Authenticate(model);
         actionResult.AssertObjectResult<TokenModel, OkObjectResult>();

        await _authService.Received(1).Authenticate(model.Email, model.Password);
    }
    
    [Fact]
    public async Task Authenticate_WhenNotAuthenticated_ReturnsUnauthorized()
    {
        // Arrange
        var model = new UserAuthModel();
        var user = new UserDto();

        _authService.Authenticate(model.Email, model.Password).ReturnsNull();

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.Authenticate(model);
        
        actionResult.AssertResult<TokenModel, UnauthorizedResult>();

        await _authService.Received(1).Authenticate(model.Email, model.Password);
    }
    
    private AuthController RetrieveController()
    {
        return new AuthController(_authService, _authProvider);
    }
}