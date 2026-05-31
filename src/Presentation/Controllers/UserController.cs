using Microsoft.AspNetCore.Mvc;
using StockManager.Application.DTO;
using StockManager.Application.Services;

namespace StockManager.Presentation.Controllers;

[ApiController]
[Route("users/")]
public class UserController(
    UserService service
): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDTO userDTO)
        => Created("/users", await service.Create(userDTO));
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetUser(int id)
        => Ok(await service.GetUser(id));
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResponseUserDTO>>> GetAllUser()
        => Ok(await service.GetAllUser());

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await service.Delete(id);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ResponseUserDTO>> Update(int id, [FromBody] UpdateUserDTO userDTO)
        => Ok(await service.Update(id, userDTO));
}
