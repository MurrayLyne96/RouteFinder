using System.Data;
using RouteFinderAPI.Common.Helpers;
using RouteFinderAPI.Models.ViewModels;

namespace RouteFinderAPI.Models.API;

public class RouteCreateViewModel
{
    public string Name { get; set; } = string.Empty;
    public int TypeId { get; set; } = default;
    public Guid UserId { get; set; }
    public List<PlotPointCreateModel> PlotPoints { get; set; } = new();
}

public class RouteCreateViewValidator : AbstractValidator<RouteCreateViewModel>
{
    public RouteCreateViewValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull();
        RuleFor(x => x.TypeId).NotEmpty().NotNull();
        RuleFor(x => x.UserId).NotEmpty().NotNull();
        RuleForEach(x => x.PlotPoints).SetValidator(new PlotPointCreateValidator());
        RuleFor(x => x.PlotPoints).Must(x => PlotPointValidator
            .AllPlotPointsUnique(x.Select(y => y.PlotOrder).ToArray()))
            .WithMessage("Please ensure that all plotpoint order values are unqiue");
        RuleFor(x => x.PlotPoints).Must(x => PlotPointValidator
            .AllPlotPointsinOrder(x.Select(y => y.PlotOrder).ToArray()))
            .WithMessage("Please ensure all plotpoints are in order");

    }
}