using System.Text.RegularExpressions;

namespace StockManager.Domain.Entities;

public class UserResetPassword
{
    private UserResetPassword() { }

    public UserResetPassword(string email, string code, int userId)
    {
        Validation(email);

        Email = email;
        Code = code;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        ExpiresAt = DateTime.UtcNow.AddMinutes(30);
        IsUsed = false;
    }

    public int Id { get; private set; }
    public string Email { get; private set; } = null!;
    public DateTime ExpiresAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string Code { get; private set; } = null!;
    public bool IsUsed { get; private set; }
    public int UserId { get; private set; }
    public User User { get; private set; } = null!;

    public void Used()
    {
        IsUsed = true;
    }

    public void Validation(string email)
    {
        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("invalid email");
        if (email.Length > 255)
            throw new ArgumentException("invalid credentials");
    }
}
