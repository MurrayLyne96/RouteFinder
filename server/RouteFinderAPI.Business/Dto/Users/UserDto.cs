using RouteFinderAPI.Services.Dto.Roles;

namespace RouteFinderAPI.Services.Dto.Users;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public Guid RoleId { get; set; }
    public RoleDto Role { get; set; }
}