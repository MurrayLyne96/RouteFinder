namespace RouteFinderAPI.Services.Dto.Users;

public class UserUpdateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? Password { get; set; }
    public Guid RoleId { get; set; }
    public RoleDto Role { get; set; }
    public DateTime DateOfBirth { get; set; } = DateTime.Now;
}