using StockManager.Application.DTO;
using StockManager.Domain.Entities;
using StockManager.Domain.Exceptions;
using StockManager.Domain.Interfaces;

namespace StockManager.Application.Services;

public class ProductService(
    IProductRepository repository
)
{
    public async Task<ResponseProductDTO> Create(CreateProductDTO productDTO)
    {
        if (await repository.ProductExists(productDTO.Name)) 
            throw new ProductAlreadyExistsException("product already exists");

        var product = new Product(productDTO.Name, productDTO.Quantity, productDTO.Price, productDTO.CategoryId);

        await repository.Create(product);
        await repository.SaveChanges();

        return ToDTO(product);
    }

    public async Task<ResponseProductDTO> GetProduct(int id)
        => ToDTO(await GetProductOrThrow(id));
    
    public async Task<IEnumerable<ResponseProductDTO>> GetAllProducts()
    {
        var products = await repository.GetAllProducts();

        return products.Select(p => ToDTO(p));
    }

    public async Task<ResponseProductDTO> Update(int id, UpdateProductDTO productDTO)
    {
        var product = await GetProductOrThrow(id);

        if (await repository.ProductExists(productDTO.Name))
            throw new ProductAlreadyExistsException("product already exists");
            
        product.Update(productDTO.Name, productDTO.Quantity, productDTO.Price, productDTO.CategoryId);
        await repository.SaveChanges();

        return ToDTO(product);
    }

    public async Task Delete(int id)
    {
        var product = await GetProductOrThrow(id);
        repository.Delete(product);
        
        await repository.SaveChanges();
    }

    public async Task<ResponseProductDTO> UpdateQuantity(int id, int quantity)
    {
        var product = await GetProductOrThrow(id);

        product.UpdateQuantity(quantity);
        await repository.SaveChanges();

        return ToDTO(product);
    }

    private async Task<Product> GetProductOrThrow(int id)
    {
        var product = await repository.GetProduct(id) 
            ?? throw new ProductNotFoundException("product not found");
        return product;
    }

    private static ResponseProductDTO ToDTO(Product product)
    {
        return new ResponseProductDTO()
        {
            Id = product.Id,
            Name = product.Name,
            Quantity = product.Quantity,
            Price = product.Price,
            CategoryId = product.CategoryId
        };
    }
}
