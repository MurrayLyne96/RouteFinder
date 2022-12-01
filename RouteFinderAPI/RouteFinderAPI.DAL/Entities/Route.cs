using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouteFinderAPI.Data.Entities;

public class Route
{
    [Key] 
    [Column("id")] 
    public Guid Id { get; set; }
    
    [Column("route_name")]
    public string? RouteName { get; set; }
    
    [Column("created")]
    public DateTime Created { get; set; }
    
    [Column("last_modified")]
    public DateTime? LastModified { get; set; }
    
    [Column("user_id")]
    public Guid UserId { get; set; }
    
    public User User { get; set; }
    
    [Column("type_id")]
    public int TypeId { get; set; }
    
    public Type Type { get; set; }
}