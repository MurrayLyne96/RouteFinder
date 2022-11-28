using RouteFinderAPI.Models.ViewModels;

namespace RouteFinderAPI.Models.API;

public class RouteCreateModel
{
    public string Name { get; set; } = string.Empty;
    public int TypeId { get; set; } = default;
    public Guid UserId { get; set; }
    public List<PlotPointViewModel> PlotPoints { get; set; } = new();
}