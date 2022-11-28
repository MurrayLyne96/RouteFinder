using RouteFinderAPI.Models.ViewModels.Base;

namespace RouteFinderAPI.Models.ViewModels;

public class TypeViewModel : BaseViewModel
{
    public int Id { get; set; } = default;
    public string Name { get; set; } = string.Empty;
}