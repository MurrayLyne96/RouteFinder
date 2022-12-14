namespace RouteFinderAPI.Data.Entities;

[Table("routes")]
public class MapRoute
{
    [Key] 
    [Column("id")] 
    public Guid Id { get; set; }
    
    [Column("route_name")]
    public string RouteName { get; set; }
    
    [Column("created")]
    public DateTime Created { get; set; }
    
    [Column("last_modified")]
    public DateTime? LastModified { get; set; }
    
    [Column("user_id")]
    [ForeignKey("users")]
    public Guid UserId { get; set; }
    
    public User User { get; set; }
    
    [Column("type_id")]
    [ForeignKey("types")]
    public int TypeId { get; set; }
    
    public RouteType Type { get; set; }
    
    public List<Plotpoint> Plotpoints { get; set; }
}