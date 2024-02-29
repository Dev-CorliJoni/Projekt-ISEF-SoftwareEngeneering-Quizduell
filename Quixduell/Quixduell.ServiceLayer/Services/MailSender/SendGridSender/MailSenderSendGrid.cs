using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Diagnostics;

namespace Quixduell.ServiceLayer.Services.MailSender.SendGrid
{
    /// <summary>
    /// Represents a mail sender implementation using SendGrid.
    /// </summary>
    public class MailSenderSendGrid : IMailSender
    {
        private ISendGridClient _client;
        private readonly SendGridEmailConfiguration _emailConfiguration;
        private readonly ILogger<MailSenderSendGrid> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MailSenderSendGrid"/> class.
        /// </summary>
        /// <param name="client">The SendGrid client.</param>
        /// <param name="emailConfiguration">The email configuration options.</param>
        /// <param name="logger">The logger.</param>
        public MailSenderSendGrid(ISendGridClient client, IOptions<SendGridEmailConfiguration> emailConfiguration, ILogger<MailSenderSendGrid> logger)
        {
            _client = client;
            _emailConfiguration = emailConfiguration.Value;
            _logger = logger;
        }

        /// <summary>
        /// Sends an email asynchronously using SendGrid.
        /// </summary>
        /// <param name="FromMailAddress">The sender's email address.</param>
        /// <param name="ToMailAddress">The recipient's email address.</param>
        /// <param name="Subject">The subject of the email.</param>
        /// <param name="Body">The body of the email.</param>
        /// <param name="isHtml">Specifies whether the email body is HTML.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
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
