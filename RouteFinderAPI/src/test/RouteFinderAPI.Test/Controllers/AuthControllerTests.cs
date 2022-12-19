using NSubstitute.ReturnsExtensions;
using RouteFinderAPI.Services.Dto.Users;

namespace RouteFinderAPI.Test.Controllers;

public class AuthControllerTests
{
    private readonly IAuthService _authService;

    public AuthControllerTests()
    {
        _authService = Substitute.For<IAuthService>();
    }

    [Fact]
    public async Task Authenticate_WhenAuthenticated_ReturnsOk()
    {
        // Arrange
        var model = new UserAuthModel();
        var user = new UserDto();
        
        _authService.Authenticate(model.Email, model.Password).Returns(user);

        var controller = RetrieveController();
        
        // Act
        var actionResult = await controller.Authenticate(model);
        
        actionResult.AssertResult<TokenModel, OkResult>();

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
        return new AuthController(_authService);
    }
}