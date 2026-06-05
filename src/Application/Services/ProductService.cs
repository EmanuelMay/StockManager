using StockManager.Application.DTO;
using StockManager.Domain.Entities;
using StockManager.Domain.Exceptions;
using StockManager.Domain.Interfaces.Repositories;
using StockManager.Domain.Interfaces.Services;

namespace StockManager.Application.Services;

public class ProductService(
    IProductRepository repository
) : IProductService
{
    public async Task<ResponseProductDTO> Add(CreateProductDTO productDTO)
    {
        if (await repository.Exists(productDTO.Name)) 
            throw new ProductAlreadyExistsException("product already exists");

        var product = new Product(productDTO.Name, productDTO.Quantity, productDTO.Price, productDTO.CategoryId);

        await repository.Add(product);
        await repository.SaveChanges();

        return ToDTO(product);
    }

    public async Task<ResponseProductDTO> GetById(int id)
        => ToDTO(await GetProductOrThrow(id));
    
    public async Task<IEnumerable<ResponseProductDTO>> GetAll()
    {
        var products = await repository.GetAll();

        return products.Select(p => ToDTO(p));
    }

    public async Task<ResponseProductDTO> Update(int id, UpdateProductDTO productDTO)
    {
        var product = await GetProductOrThrow(id);

        if (await repository.Exists(productDTO.Name))
            throw new ProductAlreadyExistsException("product already exists");
            
        product.Update(productDTO.Name, productDTO.Quantity, productDTO.Price, productDTO.CategoryId);
        await repository.SaveChanges();

        return ToDTO(product);
    }

    public async Task Remove(int id)
    {
        var product = await GetProductOrThrow(id);
        repository.Remove(product);
        
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
        var product = await repository.GetById(id) 
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
