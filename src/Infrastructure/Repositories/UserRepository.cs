using Microsoft.EntityFrameworkCore;
using StockManager.Domain.Entities;
using StockManager.Domain.Interfaces.Repositories;
using StockManager.Infrastructure.Context;

namespace StockManager.Infrastructure.Repositories;

public class UserRepository(
    AppDbContext context
) : IUserRepository
{
    public async Task SaveChanges()
        => await context.SaveChangesAsync();

    public async Task Add(User user)
        => await context.Users.AddAsync(user);

    public async Task<User?> GetById(int id)
        => await context.Users.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<User>> GetAll()
        => await context.Users.AsNoTracking().ToListAsync();

    public void Remove(User user)
        => context.Users.Remove(user);

    public async Task<bool> EmailExists(string? email)
        => await context.Users.AnyAsync(u => u.Email == email);
    
    public async Task<User?> GetByEmail(string email)
        => await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    
    public async Task CreateResetPassword(UserResetPassword resetDTO)
        => await context.UserResetPassword.AddAsync(resetDTO);

    public async Task<UserResetPassword?> GetResetPasswordCode(string code, int id)
        => await context.UserResetPassword.FirstOrDefaultAsync(u => 
            u.Code == code &&
            u.UserId == id &&
            u.ExpiresAt > DateTime.UtcNow &&
            u.IsUsed == false);
}
