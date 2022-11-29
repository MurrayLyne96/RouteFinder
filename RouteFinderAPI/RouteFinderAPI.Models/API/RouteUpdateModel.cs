namespace RouteFinderAPI.Models.API;

public class RouteUpdateModel
{
    public string Name { get; set; } = string.Empty;
    public int TypeId { get; set; } = default;
}

public class RouteUpdateValidator : AbstractValidator<RouteUpdateModel>
{
    public RouteUpdateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull();
        RuleFor(x => x.TypeId).NotEmpty().NotNull();
    }
}