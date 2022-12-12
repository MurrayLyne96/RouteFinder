namespace RouteFinderAPI.Services.Dto.Routes;

public class RouteDto
{
    public Guid Id { get; set; }
    public string RouteName { get; set; }
    public Guid UserId { get; set; }
    public int TypeId { get; set; }
    public TypeDto Type { get; set; } = new();
}