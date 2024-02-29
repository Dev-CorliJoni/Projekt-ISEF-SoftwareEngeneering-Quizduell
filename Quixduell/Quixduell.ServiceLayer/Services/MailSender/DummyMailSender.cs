using Microsoft.Extensions.Logging;

namespace Quixduell.ServiceLayer.Services.MailSender
{
    /// <summary>
    /// Represents a dummy mail sender implementation.
    /// </summary>
    public class DummyMailSender : IMailSender
    {
        private readonly ILogger<DummyMailSender> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DummyMailSender"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public DummyMailSender(ILogger<DummyMailSender> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Sends a mail asynchronously.
        /// </summary>
        /// <param name="FromMailAddress">The sender's email address.</param>
        /// <param name="ToMailAddress">The recipient's email address.</param>
        /// <param name="Subject">The subject of the email.</param>
        /// <param name="Body">The body of the email.</param>
        /// <param name="isHtml">Specifies whether the email body is HTML.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task SendMailAsync(string? FromMailAddress, string ToMailAddress, string Subject, string Body, bool isHtml = false)
        {
            _logger.LogWarning("Cannot send Mail to: {To}, no Mail Option enabled", ToMailAddress);
            return Task.CompletedTask;
        }
    }
}
