using Microsoft.EntityFrameworkCore;
using RouteFinderAPI.Data.Entities;
using RouteFinderAPI.Data.Interfaces;
namespace RouteFinderAPI.Data.Contexts;

public class RouteFinderContext : BaseContext, IRouteFinderDatabase
{
    public RouteFinderContext(DbContextOptions option) : base(option) { }
    public RouteFinderContext(string connectionString) : base(connectionString) { }
        
    public virtual DbSet<User> Users { get; set; }
    
    public virtual DbSet<MapRoute> Routes { get; set; }
    
    public virtual DbSet<RouteType> Types { get; set; }
    
    public virtual DbSet<Role> Roles { get; set;  }

    public virtual DbSet<Plotpoint> Plotpoints { get; set; }
}