using RouteFinderAPI.Integration.Tests.Models;

namespace RouteFinderAPI.Integration.Tests.Controllers;

[Collection("Integration")]
public class RoutesControllerTests
{
    private readonly HttpClient _httpClient;
    private readonly ITestOutputHelper _testOutputHelper;

    public RoutesControllerTests(ITestOutputHelper testOutputHelper, IntegrationClassFixture integrationClassFixture)
    {
        _testOutputHelper = testOutputHelper;
        _httpClient = integrationClassFixture.Host.CreateClient();
    }

    [Fact]
    public async Task GetAllRoutes_WhenRoutesPresent_ReturnsOk()
    {
        var response = await _httpClient.GetAsync("/api/Routes");
        var value = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<RouteViewModel[]>());
    }

    [Fact]
    public async Task GetRoutesById_WhenRoutePresent_ReturnsOk()
    {
        var response = await _httpClient.GetAsync($"/api/Routes/{DatabaseSeed.RouteToTestId}");
        var value = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<RouteDetailViewModel>());
    }

    [Fact]
    public async Task CreateRoute_WhenRouteIsCreated_ReturnsCreated()
    {
        var routeModel = new RouteCreateViewModel {
            Name = "New Route",
            TypeId = 1,
            UserId = DatabaseSeed.UserToTestId,
            PlotPoints = new List<PlotPointCreateModel>{
                new() {
                    XCoordinate = 12,
                    YCoordinate = 14,
                    Description = "start point",
                    PlotOrder = 0,
                },
            }
        };

        var response = await _httpClient.PostAsJsonAsync("/api/Routes", routeModel);
        var value = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task CreateRoute_WhenRouteIsInvalid_ReturnsBadRequest()
    {
        var routeModel = new RouteCreateViewModel {
            Name = "",
            TypeId = 1,
            UserId = Guid.Empty,
            PlotPoints = new List<PlotPointCreateModel>{
                new() {
                    XCoordinate = 14,
                    YCoordinate = 14,
                    Description = "",
                    PlotOrder = 1,
                },
                new() {
                    XCoordinate = 14,
                    YCoordinate = 20,
                    Description = "",
                    PlotOrder = 1,
                },
            }
        };

        var response = await _httpClient.PostAsJsonAsync("/api/Routes", routeModel);
        var value = await response.Content.ReadAsStringAsync();
        var result = value.VerifyDeSerialize<ValidationModel>();

        result.Errors.CheckIfErrorPresent("Name", "'Name' must not be empty.");
        result.Errors.CheckIfErrorPresent("UserId", "'User Id' must not be empty.");
        result.Errors.CheckIfErrorPresent("PlotPoints", "Please ensure that all plotpoint order values are unqiue", "Please ensure all plotpoints are in order");
        result.Errors.CheckIfErrorPresent("PlotPoints[0].Description", "'Description' must not be empty.");
        result.Errors.CheckIfErrorPresent("PlotPoints[1].Description", "'Description' must not be empty.");
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateRoute_WhenRouteIsUpdated_ReturnsNoContent()
    {
        var routeModel = new RouteCreateViewModel {
            Name = "Updated Route",
            TypeId = 1,
        };

        var response = await _httpClient.PutAsJsonAsync($"/api/Routes/{DatabaseSeed.RouteToTestId}", routeModel);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task UpdateRoute_WhenRouteIsInvalid_ReturnsBadRequest()
    {
        var routeModel = new RouteCreateViewModel {
            Name = "",
            TypeId = 1,
        };
        
        var response = await _httpClient.PutAsJsonAsync($"/api/Routes/{DatabaseSeed.RouteToTestId}", routeModel);
        var value = await response.Content.ReadAsStringAsync();
        var result = value.VerifyDeSerialize<ValidationModel>();
        result.Errors.CheckIfErrorPresent("Name", "'Name' must not be empty.");
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteRoute_WhenRouteIsDeleted_ReturnsNoContent()
    {
        var response = await _httpClient.DeleteAsync($"/api/Routes/{DatabaseSeed.RouteToDeleteId}");
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task CreatePlotpoint_WhenPlotpointIsCreated_ReturnsCreated()
    {
        var plotPointModel = new PlotPointCreateModel {
            XCoordinate = 53,
            YCoordinate = 34,
            Description = "End Point",
            PlotOrder = 1,
        };

        var response = await _httpClient.PostAsJsonAsync($"/api/Routes/{DatabaseSeed.RouteToTestId}/plotpoints", plotPointModel);
        var value = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task UpdatePlotpoint_WhenPlotpointIsUpdated_ReturnsNoContent()
    {
        var plotPointModel = new PlotPointCreateModel {
            XCoordinate = 53,
            YCoordinate = 34,
            Description = "End Point",
            PlotOrder = 1,
        };

        var response = await _httpClient.PutAsJsonAsync($"/api/Routes/{DatabaseSeed.RouteToTestId}/plotpoints/{DatabaseSeed.PlotpointToTestId}", plotPointModel);
        var value = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeletePlotpoint_WhenPlotpointDeleted_ReturnsNoContent()
    {
        var response = await _httpClient.DeleteAsync($"/api/Routes/{DatabaseSeed.RouteToTestId}/plotpoints/{DatabaseSeed.PlotpointToDeleteId}");
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}