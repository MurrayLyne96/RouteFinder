namespace RouteFinderAPI.Models.ViewModels.Base;

public class BaseViewModel
{
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime LastModified { get; set; } = DateTime.Now;
}