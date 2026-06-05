using StockManager.Application.DTO;

namespace StockManager.Domain.Interfaces.Services;

public interface ICategoryService
{
    public Task<ResponseCategoryDTO> Add(CreateCategoryDTO categoryDTO);

    public Task<ResponseCategoryDTO> GetById(int id);

    public Task<IEnumerable<ResponseCategoryDTO>> GetAll();

    public Task Remove(int id);

    public Task<ResponseCategoryDTO> Update(int id, UpdateCategoryDTO categoryDTO);
}
