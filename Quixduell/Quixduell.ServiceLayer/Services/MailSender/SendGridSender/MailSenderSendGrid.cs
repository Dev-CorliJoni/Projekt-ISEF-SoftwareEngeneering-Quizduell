using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Diagnostics;

namespace Quixduell.ServiceLayer.Services.MailSender.SendGrid
{
    public class MailSenderSendGrid : IMailSender
    {
        private ISendGridClient _client;
        private readonly SendGridEmailConfiguration _emailConfiguration;
        private readonly ILogger<MailSenderSendGrid> _logger;

        public MailSenderSendGrid(ISendGridClient client, IOptions<SendGridEmailConfiguration> emailConfiguration, ILogger<MailSenderSendGrid> logger)
        {
            _client = client;
            _emailConfiguration = emailConfiguration.Value;
            _logger = logger;
        }

        public async Task SendMailAsync(string? FromMailAddress, string ToMailAddress, string Subject, string Body, bool isHtml = false)
        {
            if (!_emailConfiguration.CheckValues())
            {
                _logger.LogWarning("No SendGrid Options configured, do nothing");
                return;
            }

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(FromMailAddress ?? _emailConfiguration.DefaultSender),
                Subject = Subject,
            };
            if (isHtml)
            {
                msg.AddContent(MimeType.Html, Body);
            }
            else
            {
                msg.AddContent(MimeType.Text, Body);
            }

            msg.AddTo(new EmailAddress(ToMailAddress));

            var response = await _client.SendEmailAsync(msg);

            if (response is null)
            {
                _logger.LogError("No response from Send Grid Client");
            }

            if (!(response!.IsSuccessStatusCode))
            {
                _logger.LogError("Error sending Mail with Send Grid: {error}", await response.DeserializeResponseBodyAsync());
            }




        }
    }
}
