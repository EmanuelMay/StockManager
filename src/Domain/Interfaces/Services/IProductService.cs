using StockManager.Application.DTO;

namespace StockManager.Domain.Interfaces.Services;

public interface IProductService
{
    public Task<ResponseProductDTO> Add(CreateProductDTO productDTO);

    public Task<ResponseProductDTO> GetById(int id);

    public Task<IEnumerable<ResponseProductDTO>> GetAll();

    public Task<ResponseProductDTO> Update(int id, UpdateProductDTO productDTO);

    public Task Remove(int id);

    public Task<ResponseProductDTO> UpdateQuantity(int id, int quantity);
}
