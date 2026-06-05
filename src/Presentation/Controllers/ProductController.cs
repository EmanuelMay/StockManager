using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManager.Application.DTO;
using StockManager.Domain.Interfaces.Services;

namespace StockManager.Presentation.Controllers;

[ApiController]
[Route("products/")]
[Authorize]
public class ProductController(
    IProductService service
) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ResponseProductDTO>> Add([FromBody] CreateProductDTO productDTO) 
        => Created("products/", await service.Add(productDTO));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ResponseProductDTO>> GetById(int id)
        => Ok(await service.GetById(id));
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResponseProductDTO>>> GetAll()
        => Ok(await service.GetAll());
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ResponseProductDTO>> Update(int id, [FromBody] UpdateProductDTO productDTO)
        => Ok(await service.Update(id, productDTO));

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Remove(int id)
    {
        await service.Remove(id);
        return NoContent();
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<ResponseProductDTO>> UpdateQuantity(int id, [FromBody] int quantity)
        => Ok(await service.UpdateQuantity(id, quantity));
}
