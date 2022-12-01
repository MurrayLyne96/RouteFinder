using Microsoft.EntityFrameworkCore;
using RouteFinderAPI.Data.Entities;
using RouteFinderAPI.Data.Interfaces;
using Type = System.Type;

namespace RouteFinderAPI.Data.Contexts;

public class RouteFinderContext : BaseContext, IRouteFinderDatabase
{
    public RouteFinderContext(DbContextOptions option) : base(option) { }
    public RouteFinderContext(string connectionString) : base(connectionString) { }
        
    public virtual DbSet<User> Users { get; set; }
    
    public virtual DbSet<Route> Routes { get; set; }
    
    public virtual DbSet<Type> Types { get; set; }
}