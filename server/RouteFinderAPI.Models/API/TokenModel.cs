namespace RouteFinderAPI.Models.API;

public class TokenModel
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}

public class TokenValidator : AbstractValidator<TokenModel>
{
    public TokenValidator()
    {
        RuleFor(x => x.Token).NotEmpty().NotNull();
    }
}