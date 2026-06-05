using Microsoft.EntityFrameworkCore;
using StockManager.Domain.Entities;
using StockManager.Domain.Interfaces.Repositories;
using StockManager.Infrastructure.Context;

namespace StockManager.Infrastructure.Repositories;

public class CategoryRepository(
    AppDbContext context
) : ICategoryRepository
{
    public async Task SaveChanges()
        => await context.SaveChangesAsync();
    
    public async Task Add(Category category)
        => await context.Categories.AddAsync(category);
    
    public async Task<Category?> GetById(int id)
        => await context.Categories.FirstOrDefaultAsync(c => c.Id == id);

    public async Task<IEnumerable<Category>> GetAll()
        => await context.Categories.AsNoTracking().ToListAsync();
    
    public void Remove(Category category)
        => context.Categories.Remove(category);
    
    public async Task<bool> Exists(string? name)
        => await context.Categories.AnyAsync(c => c.Name == name);
}
