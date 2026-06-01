using StockManager.Domain.Entities;

namespace StockManager.Domain.Interfaces;

public interface ICategoryRepository
{
    public Task SaveChanges();

    public Task Create(Category category);

    public Task<Category?> GetCategory(int id);

    public Task<IEnumerable<Category>> GetAllCategories();

    public void Delete(Category category);
}
