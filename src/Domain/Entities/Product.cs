namespace StockManager.Domain.Entities;

public class Product
{
    private Product() { }

    public Product(string name, int quantity, decimal price, int idCategory)
    {
        Validation(name, quantity, price);

        Name = name;
        Quantity = quantity;
        Price = price;
        CategoryId = idCategory;
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public int CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;

    public void Update(string? name, int? quantity, decimal? price, int? categoryId)
    {
        Validation(name, quantity, price);

        if (!string.IsNullOrWhiteSpace(name))
            Name = name;
        if (price.HasValue)
            Price = price.Value;
        if (quantity.HasValue)
            Quantity = quantity.Value;
        if (categoryId.HasValue)
            CategoryId = categoryId.Value;
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity + Quantity < 0)
            throw new ArgumentException("quantity cannot be negative");
        
        Quantity += quantity;
    }

    private static void Validation(string? name, int? quantity, decimal? price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("invalid credentials");
        if (name.Length > 150)
            throw new ArgumentException("name max length is 150");
        if (quantity < 0)
            throw new ArgumentException("quantity cannot be negative");
        if (price < 0)
            throw new ArgumentException("prica annot be negative");
    }
}
