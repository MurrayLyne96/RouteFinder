namespace RouteFinderAPI.Models.ViewModels;

public class RouteViewModel
{
    public Guid Id { get; set; } = Guid.Empty;
    public string RouteName { get; set; } = string.Empty;
    public Guid UserId { get; set; } = Guid.Empty;
    public int TypeId { get; set; }
    public TypeViewModel Type { get; set; } = new();
}

public class RouteViewValidator : AbstractValidator<RouteViewModel>
{
    public RouteViewValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.RouteName).NotNull().NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.TypeId).NotEmpty();
        RuleFor(x => x.Type).NotNull().NotEmpty();
    }
}