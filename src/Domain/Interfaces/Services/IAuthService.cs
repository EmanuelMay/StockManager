using StockManager.Application.DTO;

namespace StockManager.Domain.Interfaces.Services;

public interface IAuthService
{
    public Task<string> Login(LoginDTO loginDTO);
}
