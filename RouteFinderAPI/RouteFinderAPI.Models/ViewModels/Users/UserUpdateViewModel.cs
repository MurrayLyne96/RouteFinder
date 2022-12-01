using RouteFinderAPI.Models.ViewModels.Base;

namespace RouteFinderAPI.Models.ViewModels;
public class UserUpdateViewModel : BaseViewModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; } = DateTime.Now;
}


public class UserUpdateViewValidator : AbstractValidator<UserUpdateViewModel>
{
    public UserUpdateViewValidator()
    {
        RuleFor(x => x.FirstName).NotNull().NotEmpty();
        RuleFor(x => x.LastName).NotNull().NotEmpty();
        RuleFor(x => x.Role).NotNull().NotEmpty().Must(x => RoleConstants.ContainsRole(x));
        RuleFor(x => x.Role).NotNull().NotEmpty().Must(x => RoleConstants.ContainsRole(x));
        RuleFor(x => x.DateOfBirth).NotNull().NotEmpty().GreaterThanOrEqualTo(DateTime.Parse("01/01/1900"));
    }
}
