using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.Interfaces;
using api.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace api.Services
{
    public class EmailSenderService(IOptions<MailSettings> _mailSettings) : IEmailSenderService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {

            var emailMessage = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Value.Email),
                Subject = subject
            };
            emailMessage.To.Add(MailboxAddress.Parse(email));

            var filePath = $"{Directory.GetCurrentDirectory()}\\Templetes\\OtpTemplete.html";
            var str = new StreamReader(filePath);
            var htmlBody = str.ReadToEnd();
            str.Close();
            htmlBody = htmlBody.Replace("[name]", email).Replace("[otp]", message);

            var builder = new BodyBuilder
            {
                HtmlBody = htmlBody
            };
            emailMessage.Body = builder.ToMessageBody();
            emailMessage.From.Add(new MailboxAddress(_mailSettings.Value.DisplayName, _mailSettings.Value.Email));

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Value.Host, _mailSettings.Value.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Value.Email, _mailSettings.Value.Password);
            await smtp.SendAsync(emailMessage);
            smtp.Disconnect(true);
        }
    }
}



