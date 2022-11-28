namespace RouteFinderAPI.Models.API;

public class PlotPointCreateModel
{
    public float XCoordinate { get; set; } = default;
    public float YCoordinate { get; set; } = default;
    public string Description { get; set; } = string.Empty;
}