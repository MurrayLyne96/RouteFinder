using RouteFinderAPI.Models.ViewModels.Base;

namespace RouteFinderAPI.Models.ViewModels;

public class UserViewModel : BaseViewModel
{
    public Guid Id { get; set; } = Guid.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; } = DateTime.Now;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public class UserViewValidator : AbstractValidator<UserViewModel>
{
    public UserViewValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Role).NotNull().NotEmpty().Must(x => RoleConstants.ContainsRole(x));
        RuleFor(x => x.FirstName).NotNull().NotEmpty();
        RuleFor(x => x.LastName).NotNull().NotEmpty();
        RuleFor(x => x.DateOfBirth).NotNull().NotEmpty().GreaterThanOrEqualTo(DateTime.Parse("01/01/1900"));
    }
}