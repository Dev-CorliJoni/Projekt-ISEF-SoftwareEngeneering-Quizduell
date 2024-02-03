using Microsoft.Extensions.Logging;

namespace Quixduell.ServiceLayer.Services.MailSender
{
    public class DummyMailSender : IMailSender
    {
        private readonly ILogger<DummyMailSender> _logger;

        public DummyMailSender(ILogger<DummyMailSender> logger)
        {
            _logger = logger;
        }

        public Task SendMailAsync(string? FromMailAddress, string ToMailAddress, string Subject, string Body, bool isHtml = false)
        {
            _logger.LogWarning("Cannot send Mail to: {To}, no Mail Option enabled", ToMailAddress);
            return Task.CompletedTask;
        }
    }
}
