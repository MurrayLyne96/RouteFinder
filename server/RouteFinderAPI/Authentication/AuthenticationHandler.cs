using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RouteFinderAPI.Common.Constants;

namespace RouteFinderAPI.Authentication;

public class AuthenticationHandler: AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IHttpContextAccessor _contextAccessor;
    
    public AuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IHttpContextAccessor accessor) 
        : base(options, logger, encoder, clock)
    {
        _contextAccessor = accessor;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var header = _contextAccessor.HttpContext?.Request.Headers["authorization"].ToString().Replace("Bearer ", string.Empty);
        var handler = new JwtSecurityTokenHandler();
        var tokenType = RetrieveTokenType();
        var secretKey = tokenType == TokenConstants.TOKEN_TYPE.ACCESS ? Encoding.UTF8.GetBytes("JWTIWASBORNINTHEUSA") : Encoding.UTF8.GetBytes("JWTIWASBORNINTHEUK");
        
        var validation = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            ValidateAudience = false,
            ValidateIssuer = false
        };

        try
        {
            var principal = handler.ValidateToken(header, validation, out var validatedToken);
            
            var ticket = new AuthenticationTicket(principal, string.Empty);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Task.FromResult(AuthenticateResult.Fail("Authentication Failed"));
            
        }
    }

    private TokenConstants.TOKEN_TYPE RetrieveTokenType() {
        var routeValues = _contextAccessor.HttpContext.Request.RouteValues;
        var controllerName = routeValues["controller"].ToString();
        var actionName = routeValues["action"].ToString();
        return controllerName == "Auth" && actionName == "refresh" ? TokenConstants.TOKEN_TYPE.REFRESH : TokenConstants.TOKEN_TYPE.ACCESS;
    }
}