namespace Quixduell.ServiceLayer.Services.MailSender
{
    public interface IMailSender
    {
        Task SendMailAsync(string? FromMailAddress, string ToMailAddress, string Subject, string Body, bool isHtml = false);
    }
}