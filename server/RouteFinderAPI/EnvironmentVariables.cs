using System.Diagnostics.CodeAnalysis;
namespace RouteFinderAPI;

[ExcludeFromCodeCoverage] 
public static class EnvironmentVariables
{
    private static string DbConnectionStringKey => "DbConnectionString";

    public static string DbConnectionString => DbConnectionStringKey.GetValue("Server=localhost;Port=5432;Database=tripscribe;User Id=admin;Password=password;");
}