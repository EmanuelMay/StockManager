using StockManager.Domain.Entities;

namespace StockManager.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    public Task SaveChanges();

    public Task Add(User user);

    public Task<User?> GetById(int id);

    public Task<IEnumerable<User>> GetAll();

    public void Remove(User user);

    public Task<bool> EmailExists(string? email);

    public Task<User?> GetByEmail(string email);

    public Task CreateResetPassword(UserResetPassword resetDTO);

    public Task<UserResetPassword?> GetResetPasswordCode(string code, int id);
}
