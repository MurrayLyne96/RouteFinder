using RouteFinderAPI.Models.ViewModels.Base;

namespace RouteFinderAPI.Models.ViewModels;

public class PlotPointViewModel : BaseViewModel
{
    public Guid Id { get; set; } = Guid.Empty;
    public double XCoordinate { get; set; }
    public double YCoordinate { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; }
}

public class PlotPointViewValidator : AbstractValidator<PlotPointViewModel>
{
    public PlotPointViewValidator()
    {
        RuleFor((x => x.Id)).NotEmpty();
        RuleFor(x => x.XCoordinate).NotEmpty().NotNull();
        RuleFor(x => x.YCoordinate).NotEmpty().NotNull();
        RuleFor(x => x.Description).NotEmpty().NotNull();
        RuleFor(x => x.Order).NotEmpty().NotNull().GreaterThanOrEqualTo(0);
    }
}