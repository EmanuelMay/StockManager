using Microsoft.EntityFrameworkCore;
using StockManager.Domain.Entities;

namespace StockManager.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; private set; }
    public DbSet<Product> Products { get; private set; }
    public DbSet<Category> Categories { get; private set; }
    public DbSet<UserResetPassword> UserResetPassword { get; private set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Category>()
            .ToTable("categories");

        mb.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();
        
        mb.Entity<Product>()
            .ToTable("products");

        mb.Entity<Product>()
            .HasIndex(p => p.Name)
            .IsUnique();
        
        mb.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(10,2)");

        mb.Entity<User>()
            .ToTable("users");

        mb.Entity<User>()
            .Property(u => u.Name)
            .HasColumnType("VARCHAR")
            .HasMaxLength(150);

        mb.Entity<User>()
            .Property(u => u.Email)
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);

        mb.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        mb.Entity<UserResetPassword>()
            .ToTable("user_reset_password");
    }
}
