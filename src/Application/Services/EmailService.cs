using MailKit.Net.Smtp;
using MimeKit;
using StockManager.Domain.Interfaces;

namespace StockManager.Application.Services;

public class EmailService(
    IConfiguration configuration
) : IEmailService
{
    public async Task ResetPasswordEmail(string code, string email, string name)
    {
        var emailAddress = configuration["Email:Address"]
            ?? throw new Exception("Email address not configured");
        
        var emailPassword = configuration["Email:Password"]
            ?? throw new Exception("Email password not configured");

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("StockManager", emailAddress));
        message.To.Add(new MailboxAddress(name, email));
        message.Subject = "Redefinição de Senha";

        var html = await File.ReadAllTextAsync("Application/Templates/ResetPassword.html");

        html = html.Replace("{{NAME}}", name);
        html = html.Replace("{{CODE}}", code);

        message.Body = new TextPart("html")
        {
            Text = html
        };

        using var client = new SmtpClient();

        await client.ConnectAsync("smtp.gmail.com", 587, false);

        await client.AuthenticateAsync(emailAddress, emailPassword);

        await client.SendAsync(message);

        await client.DisconnectAsync(true);
    }
}
