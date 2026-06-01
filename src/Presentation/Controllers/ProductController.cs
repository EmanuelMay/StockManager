using Microsoft.AspNetCore.Mvc;
using StockManager.Application.DTO;
using StockManager.Application.Services;

namespace StockManager.Presentation.Controllers;

[ApiController]
[Route("products/")]
public class ProductController(
    ProductService service
) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ResponseProductDTO>> Create([FromBody] CreateProductDTO productDTO) 
        => Created("products/", await service.Create(productDTO));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ResponseProductDTO>> GetProduct(int id)
        => Ok(await service.GetProduct(id));
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResponseProductDTO>>> GetAllProducts()
        => Ok(await service.GetAllProducts());
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ResponseProductDTO>> Update(int id, [FromBody] UpdateProductDTO productDTO)
        => Ok(await service.Update(id, productDTO));

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await service.Delete(id);
        return NoContent();
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<ResponseProductDTO>> UpdateQuantity(int id, [FromBody] int quantity)
        => Ok(await service.UpdateQuantity(id, quantity));
}
