using StockManager.Application.DTO;
using StockManager.Domain.Interfaces;

namespace StockManager.Application.Services;

public class AuthService(
    IUserRepository repository,
    TokenService service
)
{
    public async Task<string> Login(LoginDTO dto)
    {
        var user = await repository.GetUserByEmail(dto.Email)
            ?? throw new Exception();
        
        var passwordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
        if (!passwordValid)
            throw new Exception();
        
        return service.Generate(user);
    }
}
