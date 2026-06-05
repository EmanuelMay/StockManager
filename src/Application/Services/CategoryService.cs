using StockManager.Application.DTO;
using StockManager.Domain.Entities;
using StockManager.Domain.Exceptions;
using StockManager.Domain.Interfaces.Repositories;
using StockManager.Domain.Interfaces.Services;

namespace StockManager.Application.Services;

public class CategoryService(
    ICategoryRepository repository
) : ICategoryService
{
    public async Task<ResponseCategoryDTO> Add(CreateCategoryDTO categoryDTO)
    {
        if (await repository.Exists(categoryDTO.Name))
            throw new CategoryAlreadyExistsException("category already exists");

        var category = new Category(categoryDTO.Name);

        await repository.Add(category);
        await repository.SaveChanges();

        return ToDTO(category);
    }

    public async Task<ResponseCategoryDTO> GetById(int id)
        => ToDTO(await GetCategoryOrThrow(id));
    
    public async Task<IEnumerable<ResponseCategoryDTO>> GetAll()
    {
        var categories = await repository.GetAll();

        return categories.Select(c => ToDTO(c));
    }

    public async Task Remove(int id)
    {
        var category = await GetCategoryOrThrow(id);

        repository.Remove(category);
        await repository.SaveChanges();
    }

    public async Task<ResponseCategoryDTO> Update(int id, UpdateCategoryDTO categoryDTO)
    {
        var category = await GetCategoryOrThrow(id);

        if (await repository.Exists(categoryDTO.Name))
            throw new CategoryAlreadyExistsException("category already exists");

        category.Update(categoryDTO.Name);
        await repository.SaveChanges();

        return ToDTO(category);
    }

    private async Task<Category> GetCategoryOrThrow(int id)
    {
        var category = await repository.GetById(id)
            ?? throw new CategoryNotFoundException("category not found");
        return category;
    }

    private static ResponseCategoryDTO ToDTO(Category category)
    {
        return new ResponseCategoryDTO()
        {
            Id = category.Id,
            Name = category.Name
        };
    }
}
