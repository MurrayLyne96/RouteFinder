namespace RouteFinderAPI.DAL.Specifications.Users;

public class UserByEmailSpec : Specification<User>
{
    private readonly string _email;
    public UserByEmailSpec(string email) => _email = email;
    public override Expression<Func<User, bool>> BuildExpression() => x => x.Email == _email;
}