using StockManager.Application.DTO;
using StockManager.Domain.Entities;
using StockManager.Domain.Exceptions;
using StockManager.Domain.Interfaces;

namespace StockManager.Application.Services;

public class CategoryService(
    ICategoryRepository repository
)
{
    public async Task<ResponseCategoryDTO> Create(CreateCategoryDTO categoryDTO)
    {
        if (repository.CategoryExists(categoryDTO.Name))
            throw new CategoryAlreadyExistsException("category already exists");

        var category = new Category(categoryDTO.Name);

        await repository.Create(category);
        await repository.SaveChanges();

        return ToDTO(category);
    }

    public async Task<ResponseCategoryDTO> GetCategory(int id)
        => ToDTO(await GetCategoryOrThrow(id));
    
    public async Task<IEnumerable<ResponseCategoryDTO>> GetAllCategory()
    {
        var categories = await repository.GetAllCategories();

        return categories.Select(c => ToDTO(c));
    }

    public async Task Delete(int id)
    {
        var category = await GetCategoryOrThrow(id);

        repository.Delete(category);
        await repository.SaveChanges();
    }

    public async Task<ResponseCategoryDTO> Update(int id, UpdateCategoryDTO categoryDTO)
    {
        var category = await GetCategoryOrThrow(id);

        if (repository.CategoryExists(categoryDTO.Name))
            throw new CategoryAlreadyExistsException("category already exists");

        category.Update(categoryDTO.Name);
        await repository.SaveChanges();

        return ToDTO(category);
    }

    private async Task<Category> GetCategoryOrThrow(int id)
    {
        var category = await repository.GetCategory(id)
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
