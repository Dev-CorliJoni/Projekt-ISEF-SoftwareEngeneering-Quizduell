using Microsoft.AspNetCore.Components;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;
using Quixduell.ServiceLayer.Services.MailSender;
using System;
using System.Threading.Tasks;

namespace Quixduell.ServiceLayer.ServiceLayer
{
    /// <summary>
    /// Handles contributor requests for study sets.
    /// </summary>
    public class ContributorRequest
    {
        private readonly IMailSender _mailSender;
        private readonly StudysetDataAccess _studysetDataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContributorRequest"/> class.
        /// </summary>
        /// <param name="studysetDataAccess">The study set data access.</param>
        /// <param name="mailSender">The mail sender.</param>
        public ContributorRequest(StudysetDataAccess studysetDataAccess, IMailSender mailSender)
        {
            _studysetDataAccess = studysetDataAccess;
            _mailSender = mailSender;
        }

        /// <summary>
        /// Adds a contributor to the study set and sends an acceptance email.
        /// </summary>
        /// <param name="studyset">The study set.</param>
        /// <param name="requestedUser">The user requesting to become a contributor.</param>
        public async Task AddContributor(Studyset studyset, User requestedUser)
        {
            await Task.Run(() =>
            {
                if (studyset.Creator != requestedUser && studyset.Contributors.Contains(requestedUser) == false)
                {
                    studyset.Contributors.Add(requestedUser);
                }

                if (studyset.UsersRequestedToBecomeContributor.Contains(requestedUser))
                {
                    studyset.UsersRequestedToBecomeContributor.Remove(requestedUser);
                }
            });

            await _studysetDataAccess.UpdateAsync(studyset);

            var sub = $"Contributor request has been accepted!";
            var body = $"The Request to become contributor for Studyset '{studyset.Name}' has been accepted!";
            await _mailSender.SendMailAsync(null, requestedUser.Email, sub, body);
        }

        /// <summary>
        /// Rejects a contributor request and sends a rejection email.
        /// </summary>
        /// <param name="studyset">The study set.</param>
        /// <param name="requestedUser">The user whose request is rejected.</param>
        public async Task RejectContributor(Studyset studyset, User requestedUser)
        {
            await Task.Run(() =>
            {
                if (studyset.UsersRequestedToBecomeContributor.Contains(requestedUser))
                {
                    studyset.UsersRequestedToBecomeContributor.Remove(requestedUser);
                }
            });

            var sub = $"Rejected contributor request!";
            var body = $"The Request to become contributor for Studyset '{studyset.Name}' has been rejected!";

            await _mailSender.SendMailAsync(null, requestedUser.Email, sub, body);
            await _studysetDataAccess.UpdateAsync(studyset);
        }
    }
}
