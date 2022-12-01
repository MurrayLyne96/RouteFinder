using RouteFinderAPI.Models.ViewModels.Base;

namespace RouteFinderAPI.Models.ViewModels;

public class UserCreateViewModel : BaseViewModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; } = DateTime.Now;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}


public class UserCreateViewValidator : AbstractValidator<UserCreateViewModel>
{
    public UserCreateViewValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Role).NotNull().NotEmpty().Must(x => RoleConstants.ContainsRole(x));
        RuleFor(x => x.FirstName).NotNull().NotEmpty();
        RuleFor(x => x.LastName).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(6);
        RuleFor(x => x.DateOfBirth).NotNull().NotEmpty().GreaterThanOrEqualTo(DateTime.Parse("01/01/1900"));
    }
}
