using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using api.Settings;

namespace api.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly MailSettings _mailSettings;

        public EmailSenderService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Email),
                Subject = subject
            };
            emailMessage.To.Add(MailboxAddress.Parse(email));
            emailMessage.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));

            // Read and prepare the email body
            string htmlBody;
            var filePath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Templetes\\OtpTemplete.html";

            try
            {
                using var str = new StreamReader(filePath);
                htmlBody = await str.ReadToEndAsync();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new InvalidOperationException("Failed to read email template file.", ex);
            }

            htmlBody = htmlBody.Replace("[name]", email).Replace("[otp]", message);

            var builder = new BodyBuilder
            {
                HtmlBody = htmlBody
            };
            emailMessage.Body = builder.ToMessageBody();

            // Send the email
            try
            {
                using var smtp = new SmtpClient();
                // Connect to the SMTP server
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);

                // Authenticate
                await smtp.AuthenticateAsync(_mailSettings.Email, _mailSettings.Password);

                // Send the email
                await smtp.SendAsync(emailMessage);

                // Disconnect
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new InvalidOperationException("Failed to send email.", ex);
            }
        }
    }
}
