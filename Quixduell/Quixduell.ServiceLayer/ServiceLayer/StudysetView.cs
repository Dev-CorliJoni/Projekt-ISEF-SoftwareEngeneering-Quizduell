using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.AspNetCore.Identity;
using Quixduell.ServiceLayer.Services.MailSender;
using Microsoft.AspNetCore.Components;

namespace Quixduell.ServiceLayer.ServiceLayer
{
    public class StudysetView
    {
        private readonly IMailSender _mailSender;
        private readonly StudysetDataAccess _studysetDataAccess;
        private readonly CategoryDataAccess _categoryDataAccess;
        public NavigationManager _navigationManager;

        public StudysetView(StudysetDataAccess studysetDataAccess, CategoryDataAccess categoryDataAccess, IMailSender mailSender, NavigationManager navigationManager)
        {
            _studysetDataAccess = studysetDataAccess;
            _categoryDataAccess = categoryDataAccess;
            _mailSender = mailSender;
            _navigationManager = navigationManager;
        }

        public async Task StarStudysetAsync(Studyset studyset, UserStudysetConnection? connection, User user)
        {
            await Task.Run(() =>
            {
                connection = connection == null ? studyset.Connections.Find((sc) => sc.User == user) : connection;

                if (connection == null)
                {
                    connection = new UserStudysetConnection(user, studyset, false);
                    studyset.Connections.Add(connection);
                }

                connection.IsStored = !connection.IsStored;
            });

            await _studysetDataAccess.UpdateAsync(studyset);
        }

        public async Task RateAsync(Studyset studyset, UserStudysetConnection connection, User user, float rating, string text)
        {
            await Task.Run(() =>
            {
                if (connection.Rating == null)
                {
                    connection.Rating = new Rating();
                }

                if (rating != connection.Rating.Value || text != connection.Rating.Description)
                {
                    connection.Rating.Value = rating;
                    connection.Rating.Description = text;

                }
            });

            await _studysetDataAccess.UpdateAsync(studyset);
        }

        public async Task<bool> AddContributorAsync(Studyset studyset, User user)
        {
            bool result = false;

            await Task.Run(() =>
            {
                if (studyset.Creator != user && studyset.Contributors.Contains(user) == false)
                {
                    studyset.Contributors.Add(user);
                    result = true;
                }
            });

            await _studysetDataAccess.UpdateAsync(studyset);
            return result;
        }

        public async Task SendContributorRequest(Studyset studyset, User user)
        {
            var sub = $"User {user.UserName} has requested to become a contributor for Studyset {studyset.Name}";
            var body = $"If you want to process his request click link: {_navigationManager.BaseUri}contributorrequest?user={user.Id}&studyset={studyset.Id}";
            await _mailSender.SendMailAsync(null, studyset.Creator.Email, sub, body);

            foreach (var cont in studyset.Contributors)
            {
                await _mailSender.SendMailAsync(null, studyset.Creator.Email, sub, body);
            }

            studyset.UsersRequestedToBecomeContributor.Add(user);
            await _studysetDataAccess.UpdateAsync(studyset);
        }
    }
}
