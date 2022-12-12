namespace RouteFinderAPI.Services.Dto.Plotpoints;

public class PlotpointDto
{
    public double XCoordinate { get; set; } = default;
    public double YCoordinate { get; set; } = default;
    public string Description { get; set; } = string.Empty;
    public int PlotOrder { get; set; } = 0;
    public Guid MapRouteId { get; set; }
}