using RouteFinderAPI.Models.ViewModels;

namespace RouteFinderAPI.Models.API;

public class RouteCreateModel
{
    public string Name { get; set; } = string.Empty;
    public int TypeId { get; set; } = default;
    public Guid UserId { get; set; }
    public List<PlotPointViewModel> PlotPoints { get; set; } = new();
}

public class RouteCreateValidator : AbstractValidator<RouteCreateModel>
{
    public RouteCreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull();
        RuleFor(x => x.TypeId).NotEmpty().NotNull();
        RuleFor(x => x.UserId).NotEmpty().NotNull();
    }
}