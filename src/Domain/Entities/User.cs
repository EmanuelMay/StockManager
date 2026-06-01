using System.Text.RegularExpressions;

namespace StockManager.Domain.Entities;

public class User
{
    private User() { }

    public User(string name, string email, string passwordHash)
    {
        Validation(name, email);

        Name = name;
        Email = email;
        PasswordHash = passwordHash;
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;

    public void Update(string? name, string? email)
    {
        UpdateValidation(name, email);

        if (!string.IsNullOrWhiteSpace(name))
            Name = name;
        if (!string.IsNullOrWhiteSpace(email))
            Email = email;
    }

    private void UpdateValidation(string? name, string? email)
    {
        if (!string.IsNullOrWhiteSpace(name))
            if (name.Length > 150)
                throw new ArgumentException("name max length is 150");
        
        if (!string.IsNullOrWhiteSpace(email))
        {
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("invalid email");
            if (email.Length > 255)
                throw new ArgumentException("email max length is 255");
        }
    }

    private void Validation(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("invalid credentials");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("invalid credentials");
        if (name.Length > 150)
            throw new ArgumentException("name max length is 150");
        if (email.Length > 255)
            throw new ArgumentException("email max length is 255");
        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("invalid email");
    }
}
