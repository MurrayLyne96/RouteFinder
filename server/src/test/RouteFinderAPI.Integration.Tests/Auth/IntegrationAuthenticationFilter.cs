using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Security.Claims;
using RouteFinderAPI.Common.Constants.Test;

[ExcludeFromCodeCoverage]
public class IntegrationAuthenticationFilter: AuthenticationHandler<IntegrationAuthenticationSchemeOptions>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public IntegrationAuthenticationFilter(IOptionsMonitor<IntegrationAuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock, IHttpContextAccessor httpContextAccessor)
        : base(options, logger, encoder, clock) =>_httpContextAccessor = httpContextAccessor;

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var email = Options.DefaultEmail;
        var name = Options.DefaultName;
        var claims = new[] { new Claim(ClaimTypes.Upn, email), new Claim(ClaimTypes.Name, name), new Claim(ClaimTypes.Role, "ADM")};
        var identity = new ClaimsIdentity(claims, TestConstants.DevAuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, TestConstants.DevAuthenticationScheme);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}