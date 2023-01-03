namespace RouteFinderAPI.Data.Entities;

[Table("roles")]
public class Role
{
    [Key] 
    [Column("id")] 
    public Guid Id { get; set; }
    
    [Column("role_name")]
    public string RoleName { get; set; }
    
    [Column("role_description")]
    public string RoleDescription { get; set; }
    
    [Column("created")]
    public DateTime Created { get; set; }
    
    [Column("last_modified")]
    public DateTime LastModified { get; set; }

    public List<User> Users { get; set; }
}