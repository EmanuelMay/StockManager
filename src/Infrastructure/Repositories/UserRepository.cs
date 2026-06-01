using Microsoft.EntityFrameworkCore;
using StockManager.Domain.Entities;
using StockManager.Domain.Interfaces;
using StockManager.Infrastructure.Context;

namespace StockManager.Infrastructure.Repositories;

public class UserRepository(
    AppDbContext repository
) : IUserRepository
{
    public async Task SaveChanges()
        => await repository.SaveChangesAsync();

    public async Task Create(User user)
        => await repository.Users.AddAsync(user);

    public async Task<User?> GetUser(int id)
        => await repository.Users.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<User>> GetAllUser()
        => await repository.Users.AsNoTracking().ToListAsync();

    public void Delete(User user)
        => repository.Users.Remove(user);

    public async Task<bool> EmailExists(string? email)
        => await repository.Users.AnyAsync(u => u.Email == email);
}
