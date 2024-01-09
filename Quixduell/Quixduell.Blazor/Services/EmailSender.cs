using Microsoft.AspNetCore.Identity.UI.Services;
using Quixduell.ServiceLayer.Services.MailSender;

namespace Quixduell.Blazor.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IMailSender _mailSender;

        public EmailSender(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await _mailSender.SendMailAsync(null, email, subject, htmlMessage, true);
        }
    }
}
