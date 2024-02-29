using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using Quixduell.ServiceLayer.Services.MailSender;

namespace Quixduell.ServiceLayer
{
    /// <summary>
    /// Represents a class for managing reported issues.
    /// </summary>
    public class IssueManager
    {
        private readonly IMailSender _mailSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueManager"/> class.
        /// </summary>
        /// <param name="mailSender">The mail sender service.</param>
        public IssueManager(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }

        /// <summary>
        /// Reports an issue related to a study set.
        /// </summary>
        /// <param name="studyset">The study set related to the issue.</param>
        /// <param name="reportedQuestion">The reported question.</param>
        /// <param name="reporter">The user reporting the issue.</param>
        /// <param name="issue">The description of the issue.</param>
        public async Task ReportIssueAsync(Studyset studyset, BaseQuestion reportedQuestion, User reporter, string issue)
        {
            var sub = $"Issue for Studyset {studyset.Name} was reported";
            var body = $"{reporter.Email} has reported the issue: {issue}";
            await _mailSender.SendMailAsync(null, studyset.Creator.Email, sub, body);

            foreach (var contributor in studyset.Contributors)
            {
                await _mailSender.SendMailAsync(null, contributor.Email, sub, body);
            }
        }
    }
}
