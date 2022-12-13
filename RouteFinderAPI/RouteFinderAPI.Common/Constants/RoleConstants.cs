namespace RouteFinderAPI.Common.Constants;
using System.Linq;
public static class RoleConstants
{
    private static string UserRole => "USR";
    private static string AdminRole => "ADM";

    private static string[] roles =
    {
        UserRole,
        AdminRole
    };

    public static bool ContainsRole(string role)
    {
        return roles.Contains(role);
    }
}