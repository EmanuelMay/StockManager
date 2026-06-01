using Microsoft.EntityFrameworkCore;
using StockManager.Domain.Entities;
using StockManager.Domain.Interfaces;
using StockManager.Infrastructure.Context;

namespace StockManager.Infrastructure.Repositories;

public class CategoryRepository(
    AppDbContext repository
) : ICategoryRepository
{
    public async Task SaveChanges()
        => await repository.SaveChangesAsync();
    
    public async Task Create(Category category)
        => await repository.Categories.AddAsync(category);
    
    public async Task<Category?> GetCategory(int id)
        => await repository.Categories.FirstOrDefaultAsync(c => c.Id == id);

    public async Task<IEnumerable<Category>> GetAllCategories()
        => await repository.Categories.AsNoTracking().ToListAsync();
    
    public void Delete(Category category)
        => repository.Categories.Remove(category);
}
