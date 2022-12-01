using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouteFinderAPI.Data.Entities;

public class Type
{
    [Key] 
    [Column("id")] 
    public int Id { get; set; }
    
    [Column("name")]
    public string? Name { get; set; }
    
    [Column("created")]
    public DateTime Created { get; set; }
    
    [Column("last_modified")]
    public DateTime? LastModified { get; set; }
}