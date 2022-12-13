namespace RouteFinderAPI.Models.API;

public class PlotPointCreateModel
{
    public double XCoordinate { get; set; } = default;
    public double YCoordinate { get; set; } = default;
    public string Description { get; set; } = string.Empty;
    public int PlotOrder { get; set; } = 0;
}

public class PlotPointCreateValidator : AbstractValidator<PlotPointCreateModel>
{
    public PlotPointCreateValidator()
    {
        RuleFor(x => x.XCoordinate).NotEmpty().NotNull();
        RuleFor(x => x.YCoordinate).NotEmpty().NotNull();
        RuleFor(x => x.Description).NotEmpty().NotNull();
    }
}