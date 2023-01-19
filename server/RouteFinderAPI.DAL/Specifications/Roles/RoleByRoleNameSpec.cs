namespace RouteFinderAPI.DAL.Specifications.Roles;

public class RoleByRoleNameSpec : Specification<Role>
{
    private readonly string _name;
    public RoleByRoleNameSpec(string name) => _name = name;
    public override Expression<Func<Role, bool>> BuildExpression() => x => x.RoleName == _name;
}