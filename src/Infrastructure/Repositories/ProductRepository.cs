using Microsoft.EntityFrameworkCore;
using StockManager.Domain.Entities;
using StockManager.Domain.Interfaces.Repositories;
using StockManager.Infrastructure.Context;

namespace StockManager.Infrastructure.Repositories;

public class ProductRepository(
    AppDbContext context
): IProductRepository
{
    public async Task SaveChanges() 
        => await context.SaveChangesAsync();

    public async Task Add(Product product) 
        => await context.Products.AddAsync(product);

    public async Task<Product?> GetById(int id) 
        => await context.Products.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Product>> GetAll() 
        => await context.Products.AsNoTracking().ToListAsync();

    public void Remove(Product product) 
        => context.Products.Remove(product);
    
    public async Task<bool> Exists(string? name)
        => await context.Products.AnyAsync(p => p.Name == name);
}
