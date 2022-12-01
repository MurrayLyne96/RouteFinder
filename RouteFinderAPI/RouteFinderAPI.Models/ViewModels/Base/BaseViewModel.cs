namespace RouteFinderAPI.Models.ViewModels.Base;

public class BaseViewModel
{
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastModified { get; set; } = DateTime.UtcNow;
}

public class BaseValidator : AbstractValidator<BaseViewModel>
{
    public BaseValidator()
    {
        RuleFor(x => x.Created).NotEmpty().NotNull();
        RuleFor(x => x.LastModified).NotEmpty().NotNull();
    }
}