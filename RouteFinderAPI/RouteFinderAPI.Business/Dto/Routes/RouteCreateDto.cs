namespace RouteFinderAPI.Services.Dto.Routes;

public class RouteCreateDto
{
    public string Name { get; set; }
    public int TypeId { get; set; }
    public Guid UserId { get; set; }
    public List<PlotPointCreateModel> PlotPoints { get; set; } = new();
}