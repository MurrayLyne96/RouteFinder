namespace RouteFinderAPI.Services.Dto.Plotpoints;

public class PlotpointDto
{
    public float XCoordinate { get; set; } = default;
    public float YCoordinate { get; set; } = default;
    public string Description { get; set; } = string.Empty;
    public int PlotOrder { get; set; } = 0;
}