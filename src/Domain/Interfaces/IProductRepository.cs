using StockManager.Domain.Entities;

namespace StockManager.Domain.Interfaces;

public interface IProductRepository
{
    public Task SaveChanges();

    public Task Create(Product product);

    public Task<Product?> GetProduct(int id);

    public Task<IEnumerable<Product>> GetAllProducts();

    public void Delete(Product product);

    public bool ProductExists(string? name);
}
