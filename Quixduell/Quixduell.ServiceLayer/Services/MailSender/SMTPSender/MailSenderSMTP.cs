using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Logging;


namespace Quixduell.ServiceLayer.Services.MailSender.SMTP
{
    public class MailSenderSMTP : IMailSender
    {
        private readonly SMTPEmailConfiguration _emailConfiguration;
        private readonly ILogger<MailSenderSMTP> _logger;

        public MailSenderSMTP(IOptions<SMTPEmailConfiguration> emailConfiguration, ILogger<MailSenderSMTP> logger)
        {
            _emailConfiguration = emailConfiguration.Value;
            _logger = logger;
        }


        public async Task SendMailAsync(string? FromMailAddress, string ToMailAddress, string Subject, string Body, bool isHtml = false)
        {
            if (!_emailConfiguration.CheckValues())
            {
                _logger.LogWarning("No SMTP Server Options configured, do nothing");
                return;
            }
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(FromMailAddress, FromMailAddress ?? _emailConfiguration.DefaultSenderMail));
            message.To.Add(new MailboxAddress(ToMailAddress, ToMailAddress));
            message.Subject = Subject;

            var textSubType = "plain";
            if (isHtml)
            {
                textSubType = "html";
            }

            message.Body = new TextPart(textSubType)
            {
                Text = Body
            };



            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, _emailConfiguration.UseSSL);


                    await client.SendAsync(message);
                    client.Disconnect(true);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Cannot send Mail to: {To}, from: {From}, via Server: {Server} on Port: {Port} (SSL: {UseSSL}) Exception: {ex}", ToMailAddress, FromMailAddress, _emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, _emailConfiguration.UseSSL, ex);
                }

            }
        }
    }
}
