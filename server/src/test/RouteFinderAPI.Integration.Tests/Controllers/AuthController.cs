using RouteFinderAPI.Models.API;

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
        var model = new UserAuthModel {
            Email = "testuser@test.com",
            Password = "Testuserpassword"
        };

        var response = await _httpClient.PostAsJsonAsync("/api/auth", model);

        var value = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<TokenModel>());
    }

    [Fact]
    public async Task Authenticate_WhenNotAuthenticated_ReturnsForbidden()
    {
        var model = new UserAuthModel {
            Email = "testuser@test.com",
            Password = "Testpassword"
        };

        var response = await _httpClient.PostAsJsonAsync("/api/auth", model);

        var value = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}