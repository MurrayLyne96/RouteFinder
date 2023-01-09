using RouteFinderAPI.Models.ViewModels.Roles;

namespace RouteFinderAPI.Models.ViewModels;
public class UserUpdateViewModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Password { get; set; } = string.Empty;
    public Guid RoleId { get; set; }
    public DateTime DateOfBirth { get; set; } = DateTime.Now;
}


public class UserUpdateViewValidator : AbstractValidator<UserUpdateViewModel>
{
    public UserUpdateViewValidator()
    {
        RuleFor(x => x.FirstName).NotNull().NotEmpty();
        RuleFor(x => x.LastName).NotNull().NotEmpty();
        RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
        RuleFor(x => x.RoleId).NotEmpty();
        RuleFor(x => x.DateOfBirth).NotNull().NotEmpty().GreaterThanOrEqualTo(DateTime.Parse("01/01/1900")).LessThanOrEqualTo(DateTime.UtcNow.Date.AddYears(-18));
    }
}
