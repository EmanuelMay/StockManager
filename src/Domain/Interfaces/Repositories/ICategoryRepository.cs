using StockManager.Domain.Entities;

namespace StockManager.Domain.Interfaces.Repositories;

public interface ICategoryRepository
{
    public Task SaveChanges();

    public Task Add(Category category);

    public Task<Category?> GetById(int id);

    public Task<IEnumerable<Category>> GetAll();

    public void Remove(Category category);

    public Task<bool> Exists(string? name);
}
