using RouteFinderAPI.Models.ViewModels.Base;

namespace RouteFinderAPI.Models.ViewModels;

public class RouteViewModel : BaseViewModel
{
    public Guid Id { get; set; } = Guid.Empty;
    public string RouteName { get; set; } = string.Empty;
    public Guid UserId { get; set; } = Guid.Empty;
    public Guid TypeId { get; set; } = Guid.Empty;
    public TypeViewModel Type { get; set; } = new();
}