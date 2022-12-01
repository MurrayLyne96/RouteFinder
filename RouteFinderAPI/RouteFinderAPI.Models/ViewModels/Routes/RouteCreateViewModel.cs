using RouteFinderAPI.Models.ViewModels;

namespace RouteFinderAPI.Models.API;

public class RouteCreateViewModel
{
    public string Name { get; set; } = string.Empty;
    public int TypeId { get; set; } = default;
    public Guid UserId { get; set; }
    public List<PlotPointViewModel> PlotPoints { get; set; } = new();
}

public class RouteCreateViewValidator : AbstractValidator<RouteCreateViewModel>
{
    public RouteCreateViewValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull();
        RuleFor(x => x.TypeId).NotEmpty().NotNull();
        RuleFor(x => x.UserId).NotEmpty().NotNull();
        RuleForEach(x => x.PlotPoints).NotEmpty().NotNull();

    }
}