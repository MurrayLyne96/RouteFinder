namespace RouteFinderAPI.Authentication;

public class AccessAuthenticationHandler: AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IHttpContextAccessor _contextAccessor;
    
    public AccessAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IHttpContextAccessor accessor) 
        : base(options, logger, encoder, clock)
    {
        _contextAccessor = accessor;
        HandleAuthenticateAsync();
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var header = _contextAccessor.HttpContext?.Request.Headers["authorization"].ToString().Replace("Bearer ", string.Empty);
        var handler = new JwtSecurityTokenHandler();
        var secretKey = Encoding.UTF8.GetBytes("IWASBORNINTHEUSA");
        
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
}