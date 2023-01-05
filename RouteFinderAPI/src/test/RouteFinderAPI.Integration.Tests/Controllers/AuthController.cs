namespace RouteFinderAPI.Integration.Tests.Controllers;

[Collection("Integration")]
public class AuthControllerTests
{
    private readonly HttpClient _httpClient;
    private readonly ITestOutputHelper _testOutputHelper;

    public AuthControllerTests(ITestOutputHelper testOutputHelper, IntegrationClassFixture integrationClassFixture)
    {
        _testOutputHelper = testOutputHelper;
        _httpClient = integrationClassFixture.Host.CreateClient();
    }

    [Fact]
    public async Task Authenticate_WhenAuthenticated_ReturnsOk()
    {
        
    }
}