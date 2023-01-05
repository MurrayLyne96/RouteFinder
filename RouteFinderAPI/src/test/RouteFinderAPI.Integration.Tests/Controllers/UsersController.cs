using RouteFinderAPI.Models.ViewModels;

namespace RouteFinderAPI.Integration.Tests.Controllers;

[Collection("Integration")]
public class UsersControllerTests
{
    private readonly HttpClient _httpClient;
    private readonly ITestOutputHelper _testOutputHelper;

    public UsersControllerTests(ITestOutputHelper testOutputHelper, IntegrationClassFixture integrationClassFixture)
    {
        _testOutputHelper = testOutputHelper;
        _httpClient = integrationClassFixture.Host.CreateClient();
    }

    [Fact]
    public async Task GetAllUsers_WhenUsersPresent_ReturnsOk()
    {
        var response = await _httpClient.GetAsync("/api/Users");
        var value = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<UserViewModel[]>());
    }

    [Fact]
    public async Task GetUser_WhenUserIsFound_ReturnsOk()
    {
        var response = await _httpClient.GetAsync($"api/Users/{DatabaseSeed.UserToTestId.ToString()}");
        var value = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<UserDetailViewModel>());
    }

    [Fact]
    public async Task GetRoutesFromUser_WhenRoutesPresent_ReturnsOk()
    {
        var response = await _httpClient.GetAsync($"api/Users/{DatabaseSeed.UserToTestId.ToString()}/routes");
        var value = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<RouteViewModel[]>());
    }

    [Fact]
    public async Task CreateUser_WhenUserIsCreated_ReturnsOk()
    {

    }
}
