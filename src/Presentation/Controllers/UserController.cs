using Microsoft.AspNetCore.Mvc;
using StockManager.Application.DTO;
using StockManager.Domain.Interfaces.Services;

namespace StockManager.Presentation.Controllers;

[ApiController]
[Route("users/")]
public class UserController(
    IUserService service
): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateUserDTO userDTO)
        => Created("/users", await service.Add(userDTO));
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById(int id)
        => Ok(await service.GetById(id));
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResponseUserDTO>>> GetAll()
        => Ok(await service.GetAllUser());

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Remove(int id)
    {
        await service.Remove(id);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ResponseUserDTO>> Update(int id, [FromBody] UpdateUserDTO userDTO)
        => Ok(await service.Update(id, userDTO));
    
    [HttpPost("forgot-password")]
    public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordUserDTO forgotDTO)
    {
        await service.ForgotPassword(forgotDTO.Email);
        return Ok();
    }

    [HttpPatch("reset-password")]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordUserDTO resetDTO)
    {
        await service.ResetPassword(resetDTO);
        return Ok();
    }
}
