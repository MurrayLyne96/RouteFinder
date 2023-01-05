using RouteFinderAPI.Common.Constants.Test;
using RouteFinderAPI.Data.Contexts;

namespace RouteFinderAPI.Integration.Tests.Base;

public class IntegrationClassFixture : IDisposable
{
    public readonly WebApplicationFactory<Program> Host;

    public IntegrationClassFixture() 
    {
        Host = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => {
                builder.ConfigureTestServices(e => {
                    e.AddDbContext<RouteFinderContext>(options => options
                    .EnableSensitiveDataLogging()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()),
                    ServiceLifetime.Singleton,
                    ServiceLifetime.Singleton);
                    e.AddTransient<IRouteFinderDatabase, RouteFinderContext>();
                    e.AddAuthentication(TestConstants.DevAuthenticationScheme)
                    .AddScheme<IntegrationAuthenticationSchemeOptions, IntegrationAuthenticationFilter>(
                        TestConstants.DevAuthenticationScheme, options =>
                        {
                            options.DefaultEmail = TestConstants.DevEmail;
                            options.DefaultName = TestConstants.DevName;
                        });
            
                    e.AddAuthorization(options =>
                    {
                        options.AddPolicy(string.Empty, builder =>
                        {
                            builder.AuthenticationSchemes.Add(TestConstants.DevAuthenticationScheme);
                            builder.RequireAuthenticatedUser();
                        });
                    });
                });
            });
        DatabaseSeed.SeedDatabase(Host.Services.GetService<RouteFinderContext>());
    }

    public void Dispose()
    {
        Host?.Dispose();
    }
}