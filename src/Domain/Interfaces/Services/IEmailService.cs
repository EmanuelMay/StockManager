namespace StockManager.Domain.Interfaces.Services;

public interface IEmailService
{
    public Task ResetPasswordEmail(string code, string email, string name);
}
