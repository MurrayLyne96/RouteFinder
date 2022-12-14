namespace RouteFinderAPI.Models.API;

public class UserAuthModel
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
public class UserAuthValidator : AbstractValidator<UserAuthModel>
{
    public UserAuthValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    } 
}