using StockManager.Domain.Entities;

namespace StockManager.Domain.Interfaces.Repositories;

public interface IProductRepository
{
    public Task SaveChanges();

    public Task Add(Product product);

    public Task<Product?> GetById(int id);

    public Task<IEnumerable<Product>> GetAll();

    public void Remove(Product product);

    public Task<bool> Exists(string? name);
}
