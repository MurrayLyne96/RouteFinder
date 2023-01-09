namespace RouteFinderAPI.Data.Entities;

[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("email_address")]
    public string Email { get; set; }
    
    [Column("first_name")]
    public string FirstName { get; set; }
    
    [Column("last_name")]
    public string LastName { get; set; }
    
    [Column("password")]
    public string Password { get; set; }
    
    [Column("date_of_birth")]
    public DateTime DateOfBirth { get; set; }
    
    [ForeignKey("roles")]
    [Column("role_id")]
    public Guid RoleId { get; set; }
    public Role Role { get; set; }
    
    [Column("created")]
    public DateTime Created { get; set; }
    
    [Column("last_modified")]
    public DateTime LastModified { get; set; }
    
    public List<MapRoute> Routes { get; set; }
}