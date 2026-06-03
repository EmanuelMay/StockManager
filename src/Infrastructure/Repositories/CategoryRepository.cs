using Microsoft.EntityFrameworkCore;
using StockManager.Domain.Entities;
using StockManager.Domain.Interfaces;
using StockManager.Infrastructure.Context;

namespace StockManager.Infrastructure.Repositories;

public class CategoryRepository(
    AppDbContext context
) : ICategoryRepository
{
    public async Task SaveChanges()
        => await context.SaveChangesAsync();
    
    public async Task Create(Category category)
        => await context.Categories.AddAsync(category);
    
    public async Task<Category?> GetCategory(int id)
        => await context.Categories.FirstOrDefaultAsync(c => c.Id == id);

    public async Task<IEnumerable<Category>> GetAllCategories()
        => await context.Categories.AsNoTracking().ToListAsync();
    
    public void Delete(Category category)
        => context.Categories.Remove(category);
    
    public async Task<bool> CategoryExists(string? name)
        => await context.Categories.AnyAsync(c => c.Name == name);
}
