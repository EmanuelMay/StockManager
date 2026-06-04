using Microsoft.AspNetCore.Mvc;
using StockManager.Application.DTO;
using StockManager.Application.Services;

namespace StockManager.Presentation.Controllers;

[ApiController]
[Route("/categories")]
public class CategoryController(
    CategoryService service
) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ResponseCategoryDTO>> Create([FromBody] CreateCategoryDTO categoryDTO)
        => Created("categories/", await service.Create(categoryDTO));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ResponseCategoryDTO>> GetCategory(int id)
        => Ok(await service.GetCategory(id));

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResponseCategoryDTO>>> GetAllCategories()
        => Ok(await service.GetAllCategory());
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await service.Delete(id);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ResponseCategoryDTO>> Update(int id, [FromBody] UpdateCategoryDTO categoryDTO)
        => Ok(await service.Update(id, categoryDTO));
}
