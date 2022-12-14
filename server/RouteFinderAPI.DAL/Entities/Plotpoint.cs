namespace RouteFinderAPI.Data.Entities;

[Table("plotpoints")]
public class Plotpoint
{
    [Key] 
    [Column("id")] 
    public Guid Id { get; set; }
    
    [Column("x_coordinate")]
    public double XCoordinate { get; set; }
    
    [Column("y_coordinate")]
    public double YCoordinate { get; set; }
    
    [Column("point_description")]
    public string Description { get; set; }
    
    [Column("plot_order")]
    public int PlotOrder { get; set; }
    
    [Column("created")]
    public DateTime Created { get; set; }
    
    [Column("last_modified")]
    public DateTime? LastModified { get; set; }
    
    [ForeignKey("routes")]
    [Column("route_id")]
    public Guid MapRouteId { get; set; }
    
    public MapRoute MapRoute { get; set; }
}