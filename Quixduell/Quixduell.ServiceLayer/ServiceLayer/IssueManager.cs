using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using Quixduell.ServiceLayer.Services.MailSender;

namespace Quixduell.ServiceLayer
{
    public class IssueManager
    {
        private readonly IMailSender _mailSender;

        public IssueManager(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }

        public async Task ReportIssueAsync (Studyset studyset,BaseQuestion reportedQuestion,User reporter, string Issue)
        {
            var sub = $"Issue for Studyset {studyset.Name} was reported";
            var body = $"{reporter.Email} has reported the issue: {Issue}";
            await _mailSender.SendMailAsync(null, studyset.Creator.Email, sub, body);

            foreach (var cont in studyset.Contributors)
            {
                await _mailSender.SendMailAsync(null, studyset.Creator.Email, sub, body);
            }
        }
       
    }
}
