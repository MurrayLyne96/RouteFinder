namespace RouteFinderAPI.Services.Dto.Users;

public class UserDetailDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public List<RouteDto> Routes { get; set; } = new();
}