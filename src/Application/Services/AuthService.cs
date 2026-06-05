using StockManager.Application.DTO;
using StockManager.Domain.Exceptions;
using StockManager.Domain.Interfaces.Repositories;
using StockManager.Domain.Interfaces.Services;

namespace StockManager.Application.Services;

public class AuthService(
    IUserRepository repository,
    ITokenService service
) : IAuthService
{
    public async Task<string> Login(LoginDTO dto)
    {
        var user = await repository.GetByEmail(dto.Email)
            ?? throw new InvalidCredentialsException("invalid credentials");
        
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new InvalidCredentialsException("invalid credentials");
        
        return service.Generate(user);
    }
}
