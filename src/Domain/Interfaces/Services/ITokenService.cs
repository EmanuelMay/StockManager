using StockManager.Domain.Entities;

namespace StockManager.Domain.Interfaces.Services;

public interface ITokenService
{
    public string Generate(User user);
}
