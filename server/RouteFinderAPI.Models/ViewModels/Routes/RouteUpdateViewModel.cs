namespace RouteFinderAPI.Models.API;

public class RouteUpdateViewModel
{
    public string Name { get; set; } = string.Empty;
    public int TypeId { get; set; } = default;
}

public class RouteUpdateViewValidator : AbstractValidator<RouteUpdateViewModel>
{
    public RouteUpdateViewValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull();
        RuleFor(x => x.TypeId).NotEmpty().NotNull();
    }
}