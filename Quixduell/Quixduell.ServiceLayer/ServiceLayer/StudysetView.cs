using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.Services.MailSender;
using Microsoft.AspNetCore.Components;

namespace Quixduell.ServiceLayer.ServiceLayer
{
    /// <summary>
    /// Represents a class for managing study set views.
    /// </summary>
    public class StudysetView
    {
        private readonly IMailSender _mailSender;
        private readonly StudysetDataAccess _studysetDataAccess;
        public NavigationManager _navigationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudysetView"/> class.
        /// </summary>
        /// <param name="studysetDataAccess">The data access object for study sets.</param>
        /// <param name="mailSender">The mail sender service.</param>
        /// <param name="navigationManager">The navigation manager.</param>
        public StudysetView(StudysetDataAccess studysetDataAccess, IMailSender mailSender, NavigationManager navigationManager)
        {
            _studysetDataAccess = studysetDataAccess;
            _mailSender = mailSender;
            _navigationManager = navigationManager;
        }

        /// <summary>
        /// Stars or unstars a study set for a user.
        /// </summary>
        /// <param name="studyset">The study set to star or unstar.</param>
        /// <param name="connection">The user-studyset connection.</param>
        /// <param name="user">The user.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
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

        /// <summary>
        /// Rates a study set.
        /// </summary>
        /// <param name="studyset">The study set to rate.</param>
        /// <param name="connection">The user-studyset connection.</param>
        /// <param name="user">The user.</param>
        /// <param name="rating">The rating value.</param>
        /// <param name="text">The description of the rating.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
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

        /// <summary>
        /// Adds a user as a contributor to a study set.
        /// </summary>
        /// <param name="studyset">The study set.</param>
        /// <param name="user">The user to add as a contributor.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
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

        /// <summary>
        /// Sends a request to become a contributor for a study set.
        /// </summary>
        /// <param name="studyset">The study set.</param>
        /// <param name="user">The user requesting to become a contributor.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SendContributorRequest(Studyset studyset, User user)
        {
            if (studyset.UsersRequestedToBecomeContributor.Contains(user) == false)
            {
                var sub = $"User {user.UserName} has requested to become a contributor for Studyset {studyset.Name}";
                var body = $"If you want to process his request click link: {_navigationManager.BaseUri}contributorrequest?user={user.Id}&studyset={studyset.Id}";
                await _mailSender.SendMailAsync(null, studyset.Creator.Email, sub, body);

                foreach (var contributor in studyset.Contributors)
                {
                    await _mailSender.SendMailAsync(null, contributor.Email, sub, body);
                }

                studyset.UsersRequestedToBecomeContributor.Add(user);
                await _studysetDataAccess.UpdateAsync(studyset);
            }
        }
    }
}
