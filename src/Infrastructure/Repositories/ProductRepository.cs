using Microsoft.EntityFrameworkCore;
using StockManager.Domain.Entities;
using StockManager.Domain.Interfaces;
using StockManager.Infrastructure.Context;

namespace StockManager.Infrastructure.Repositories;

public class ProductRepository(
    AppDbContext repository
): IProductRepository
{
    public async Task SaveChanges() 
        => await repository.SaveChangesAsync();

    public async Task Create(Product product) 
        => await repository.Products.AddAsync(product);

    public async Task<Product?> GetProduct(int id) 
        => await repository.Products.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Product>> GetAllProducts() 
        => await repository.Products.AsNoTracking().ToListAsync();

    public void Delete(Product product) 
        => repository.Products.Remove(product);
    
    public bool ProductExists(string? name)
        => repository.Products.Any(p => p.Name == name);
}
