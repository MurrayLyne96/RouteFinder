namespace RouteFinderAPI.Integration.Tests.Base;

public static class DatabaseSeed 
{
    public static readonly Guid UserToTestId = Guid.NewGuid();
    public static readonly Guid RouteToTestId = Guid.NewGuid();
    public static void SeedDatabase(IRouteFinderDatabase database) 
    {
        var adminRole = CreateAdminRole(database);

        database.Add<User>(new User {
            Id = UserToTestId,
            FirstName = "Test",
            LastName = "User",
            Email = "testuser@test.com",
            Created = DateTime.UtcNow,
            Password = BCrypt.Net.BCrypt.HashPassword("Testuserpassword"),
            RoleId = adminRole.Id,
        });

        database.Add<RouteType>(new RouteType {
            Id = 1,
            Name = "Cycling",
            Created = DateTime.UtcNow,
            LastModified = DateTime.UtcNow
        });

        database.Add<MapRoute>(new MapRoute {
            Id = RouteToTestId,
            RouteName = "Test Route",
            Created = DateTime.UtcNow,
            LastModified = DateTime.UtcNow,
            UserId = UserToTestId,
            TypeId = 1
        });

        database.SaveChanges();
    }

    private static Role? CreateAdminRole(IRouteFinderDatabase database)
    {
        var role = new Role {
            Id = Guid.NewGuid(),
            RoleName = "ADM",
            RoleDescription = "Administrator"
        };

        database.Add<Role>(role);
        database.SaveChanges();
        
        return database.Get<Role>().Where(x => x.Id == role.Id).Single();
    }
}