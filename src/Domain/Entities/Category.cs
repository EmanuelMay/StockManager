namespace StockManager.Domain.Entities;

public class Category
{
    private Category() { }

    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public List<Product> Products { get; private set; } = [];
}
