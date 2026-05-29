namespace Ecomercio.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; }
    public ICollection<Product> Products { get; private set; } = new List<Product>();

    private Category() { }

    public static Category Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("El nombre de la categoría es obligatorio.");

        return new Category { Name = name.Trim() };
    }
}

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}