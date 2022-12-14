namespace RouteFinderAPI.Data.Entities;

[Table("types")]
public class RouteType
{
    [Key] 
    [Column("id")] 
    public int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("created")]
    public DateTime Created { get; set; }
    
    [Column("last_modified")]
    public DateTime? LastModified { get; set; }
}