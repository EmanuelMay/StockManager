using Microsoft.AspNetCore.Mvc;
using StockManager.Application.DTO;
using StockManager.Domain.Interfaces.Services;

namespace StockManager.Presentation.Controllers;

[ApiController]
[Route("/categories")]
public class CategoryController(
    ICategoryService service
) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ResponseCategoryDTO>> Add([FromBody] CreateCategoryDTO categoryDTO)
        => Created("categories/", await service.Add(categoryDTO));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ResponseCategoryDTO>> GetById(int id)
        => Ok(await service.GetById(id));

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResponseCategoryDTO>>> GetAll()
        => Ok(await service.GetAll());
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Remove(int id)
    {
        await service.Remove(id);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ResponseCategoryDTO>> Update(int id, [FromBody] UpdateCategoryDTO categoryDTO)
        => Ok(await service.Update(id, categoryDTO));
}
