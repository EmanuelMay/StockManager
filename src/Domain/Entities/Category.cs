namespace StockManager.Domain.Entities;

public class Category
{
    private Category() { }

    public Category(string name)
    {
        Validation(name);

        Name = name;
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public List<Product> Products { get; private set; } = [];

    public void Update(string name)
    {
        Validation(name);

        Name = name;
    }

    private static void Validation(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("invalid credentials");
        if (name.Length > 150)
            throw new ArgumentException("name max length is 150");
    }
}
