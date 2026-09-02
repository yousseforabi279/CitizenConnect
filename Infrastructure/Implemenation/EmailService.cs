using Application.Contracts;
using Application.Contracts.Repos;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendPasswordResetCodeAsync(string toEmail, string code)
        {
            var subject = "Your password reset code";
            var body = $"""
                Your password reset code is: {code}

                This code will expire in 10 minutes.
                If you didn't request this, you can safely ignore this email.
                """;

            using var message = new MailMessage(_settings.FromAddress, toEmail, subject, body);
            using var client = new SmtpClient(_settings.SmtpHost, _settings.SmtpPort)
            {
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                EnableSsl = true
            };

            await client.SendMailAsync(message);
        }
    }
}