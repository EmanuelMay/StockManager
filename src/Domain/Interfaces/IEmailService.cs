namespace StockManager.Domain.Interfaces;

public interface IEmailService
{
    public Task ResetPasswordEmail(string code, string email, string name);
}
