namespace RouteFinderAPI.Services.Dto.Plotpoints;

public class PlotpointCreateDto
{
    public double XCoordinate { get; set; }
    public double YCoordinate { get; set; }
    public string Description { get; set; } = string.Empty;
    public int PlotOrder { get; set; }
    public Guid MapRouteId { get; set; }
}