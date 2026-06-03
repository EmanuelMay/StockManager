using StockManager.Domain.Entities;

namespace StockManager.Domain.Interfaces;

public interface IUserRepository
{
    public Task SaveChanges();

    public Task Create(User user);

    public Task<User?> GetUser(int id);

    public Task<IEnumerable<User>> GetAllUsers();

    public void Delete(User user);

    public Task<bool> EmailExists(string? email);

    public Task<User?> GetUserByEmail(string email);

    public Task CreateResetPassword(UserResetPassword resetDTO);

    public Task<UserResetPassword?> GetResetPasswordCode(string code, int id);
}
