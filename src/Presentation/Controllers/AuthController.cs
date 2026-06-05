using Microsoft.AspNetCore.Mvc;
using StockManager.Application.DTO;
using StockManager.Domain.Interfaces.Services;

namespace StockManager.Presentation.Controllers;

[ApiController]
[Route("auth/")]
public class AuthController(
    IAuthService service
) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        var token = await service.Login(dto);
        return Ok(new { token });
    }
}
