namespace RouteFinderAPI.Services.Dto.Routes;

public class RouteDetailDto
{
    public Guid Id { get; set; }
    public string RouteName { get; set; }
    public Guid UserId { get; set; }
    public int TypeId { get; set; }
    public TypeViewModel Type { get; set; }
    public List<PlotPointViewModel> PlotPoints { get; set; } = new();
}