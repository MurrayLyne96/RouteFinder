namespace RouteFinderAPI.Models.ViewModels;

public class RouteDetailViewModel
{
    public Guid Id { get; set; } = Guid.Empty;
    public string RouteName { get; set; } = string.Empty;
    public Guid UserId { get; set; } = Guid.Empty;
    public int TypeId { get; set; }
    public TypeViewModel Type { get; set; } = new();
    public List<PlotPointViewModel> PlotPoints { get; set; } = new();
}

public class RouteDetailViewValidator : AbstractValidator<RouteDetailViewModel>
{
    public RouteDetailViewValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.RouteName).NotNull().NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.TypeId).NotEmpty();
        RuleFor(x => x.Type).NotNull().NotEmpty();
        RuleForEach(x => x.PlotPoints).NotEmpty().NotNull();
    }
}