using Microsoft.AspNetCore.Authentication;

public class IntegrationAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
    public string DefaultEmail { get; set; }
    public string DefaultName { get; set; }
}