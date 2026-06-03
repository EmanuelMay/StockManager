using Microsoft.EntityFrameworkCore;
using StockManager.Domain.Entities;
using StockManager.Domain.Interfaces;
using StockManager.Infrastructure.Context;

namespace StockManager.Infrastructure.Repositories;

public class ProductRepository(
    AppDbContext context
): IProductRepository
{
    public async Task SaveChanges() 
        => await context.SaveChangesAsync();

    public async Task Create(Product product) 
        => await context.Products.AddAsync(product);

    public async Task<Product?> GetProduct(int id) 
        => await context.Products.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Product>> GetAllProducts() 
        => await context.Products.AsNoTracking().ToListAsync();

    public void Delete(Product product) 
        => context.Products.Remove(product);
    
    public async Task<bool> ProductExists(string? name)
        => await context.Products.AnyAsync(p => p.Name == name);
}
