using System.Diagnostics.CodeAnalysis;
namespace RouteFinderAPI;

[ExcludeFromCodeCoverage] 
public static class EnvironmentVariables
{
    private static string DbConnectionStringKey => "DbConnectionString";

    public static string DbConnectionString => DbConnectionStringKey.GetValue("Server=db;Port=5432;Database=RouteFinder;User Id=postgres;Password=password;");
}