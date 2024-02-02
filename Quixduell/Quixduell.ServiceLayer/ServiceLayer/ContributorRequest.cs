using Microsoft.AspNetCore.Components;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;
using Quixduell.ServiceLayer.Services.MailSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quixduell.ServiceLayer.ServiceLayer
{ 
    public class ContributorRequest
    {
        private readonly IMailSender _mailSender;
        private readonly StudysetDataAccess _studysetDataAccess;

        public ContributorRequest(StudysetDataAccess studysetDataAccess, IMailSender mailSender)
        {
            _studysetDataAccess = studysetDataAccess;
            _mailSender = mailSender;
        }

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
