namespace RouteFinderAPI.Models.API;

public class PlotPointCreateModel
{
    public float XCoordinate { get; set; } = default;
    public float YCoordinate { get; set; } = default;
    public string Description { get; set; } = string.Empty;
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