namespace RouteFinderAPI.Services.Dto.Users;

public class UserUpdateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime DateOfBirth { get; set; } = DateTime.Now;
}