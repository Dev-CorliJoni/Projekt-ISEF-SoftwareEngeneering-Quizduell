using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;

namespace Quixduell.ServiceLayer.ServiceLayer
{
    /// <summary>
    /// Represents a class for performing global search operations.
    /// </summary>
    public class GlobalSearch
    {
        private readonly StudysetDataAccess _studysetDataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalSearch"/> class.
        /// </summary>
        /// <param name="studysetDataAccess">The data access object for study sets.</param>
        public GlobalSearch(StudysetDataAccess studysetDataAccess)
        {
            _studysetDataAccess = studysetDataAccess;
        }

        /// <summary>
        /// Searches for study sets based on specified parameters.
        /// </summary>
        /// <param name="name">The name of the study set.</param>
        /// <param name="user">The user associated with the study set.</param>
        /// <param name="categoryName">The name of the category.</param>
        /// <returns>A list of study sets matching the search criteria.</returns>
        public async Task<List<Studyset>> Search(string? name = null, User? user = null, string? categoryName = null)
        {
            return await (await _studysetDataAccess.LoadTopByParamsAsync(name, user, categoryName)).ToListAsync();
        }

        /// <summary>
        /// Notices a study set for the current user.
        /// </summary>
        /// <param name="studyset">The study set to notice.</param>
        /// <param name="currentUser">The current user.</param>
        public async Task NoticeStudyset(Studyset studyset, User currentUser)
        {
            if (currentUser.StudysetConnections.Any(sc => sc.Studyset.Id == studyset.Id) == false)
            {
                var studysetUserConnection = new UserStudysetConnection(currentUser, studyset, true, new Rating(), 0);
                currentUser.StudysetConnections.Add(studysetUserConnection);
                studyset.Connections.Add(studysetUserConnection);
            }
            else
            {
                var connection = studyset.Connections.Find(sc => sc.User.Id == currentUser.Id)!;
                connection.IsStored = true;
            }

            await _studysetDataAccess.UpdateAsync(studyset);
        }

        /// <summary>
        /// Removes the notice for a study set for the current user.
        /// </summary>
        /// <param name="studyset">The study set to unnotice.</param>
        /// <param name="currentUser">The current user.</param>
        public async Task UnNoticeStudyset(Studyset studyset, User currentUser)
        {
            var connection = studyset.Connections.FirstOrDefault(sc => sc.User.Id == currentUser.Id);

            if (connection is not null)
            {
                connection.IsStored = false;
                await _studysetDataAccess.UpdateAsync(studyset);
            }
        }
    }
}
