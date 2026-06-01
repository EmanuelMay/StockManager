using StockManager.Domain.Entities;

namespace StockManager.Domain.Interfaces;

public interface IUserRepository
{
    public Task SaveChanges();

    public Task Create(User user);

    public Task<User?> GetUser(int id);

    public Task<IEnumerable<User>> GetAllUser();

    public void Delete(User user);

    public Task<bool> EmailExists(string? email);
}
