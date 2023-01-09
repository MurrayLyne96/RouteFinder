namespace RouteFinderAPI.Models.ViewModels;

public class TypeViewModel
{
    public int Id { get; set; } = default;
    public string Name { get; set; } = string.Empty;
}

public class TypeViewValidator : AbstractValidator<TypeViewModel>, IValidator
{
    public TypeViewValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().NotNull();
    }
}