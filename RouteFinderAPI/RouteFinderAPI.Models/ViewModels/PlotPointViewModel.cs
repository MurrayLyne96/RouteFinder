using RouteFinderAPI.Models.ViewModels.Base;

namespace RouteFinderAPI.Models.ViewModels;

public class PlotPointViewModel : BaseViewModel
{
    public Guid Id { get; set; } = Guid.Empty;
    public float XCoordinate { get; set; } = default;
    public float YCoordinate { get; set; } = default;
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; } = default;
}