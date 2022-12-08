namespace RouteFinderAPI.DAL.Specifications.Users;

public class UserByIdSpec : Specification<User>
{
    private readonly Guid _userId;
    public UserByIdSpec(Guid userId) => _userId = userId;

    public override Expression<Func<User, bool>> BuildExpression() => x => x.Id == _userId;
}