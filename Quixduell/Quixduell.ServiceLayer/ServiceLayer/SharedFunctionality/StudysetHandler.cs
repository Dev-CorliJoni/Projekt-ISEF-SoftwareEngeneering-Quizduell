using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;

namespace Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality
{
    /// <summary>
    /// Represents a class for handling study set operations.
    /// </summary>
    public class StudysetHandler
    {
        private readonly StudysetDataAccess _studysetDataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudysetHandler"/> class.
        /// </summary>
        /// <param name="studysetDataAccess">The data access object for study sets.</param>
        public StudysetHandler(StudysetDataAccess studysetDataAccess)
        {
            _studysetDataAccess = studysetDataAccess;
        }

        /// <summary>
        /// Adds a new study set asynchronously.
        /// </summary>
        /// <param name="name">The name of the study set.</param>
        /// <param name="category">The category of the study set.</param>
        /// <param name="creator">The creator of the study set.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<Studyset> AddStudysetAsync(string name, Category category, User creator)
        {
            return await AddStudysetAsync(name, category, creator, new List<User>(), new List<BaseQuestion>());
        }

        /// <summary>
        /// Adds a new study set asynchronously.
        /// </summary>
        /// <param name="name">The name of the study set.</param>
        /// <param name="category">The category of the study set.</param>
        /// <param name="creator">The creator of the study set.</param>
        /// <param name="contributors">The contributors of the study set.</param>
        /// <param name="questions">The questions in the study set.</param>
        /// <param name="userStudysetConnections">The connections associated with the study set.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<Studyset> AddStudysetAsync(string name, Category category, User creator, List<User> contributors, List<BaseQuestion> questions, List<UserStudysetConnection>? userStudysetConnections = null)
        {
            Studyset? studyset = null;
            if (userStudysetConnections is null)
            {
                studyset = new Studyset(name, category, creator, contributors, questions, new List<UserStudysetConnection>());
            }
            studyset = new Studyset(name, category, creator, contributors, questions, userStudysetConnections!);
            return await _studysetDataAccess.AddAsync(studyset!);
        }

        /// <summary>
        /// Updates an existing study set asynchronously.
        /// </summary>
        /// <param name="id">The ID of the study set to update.</param>
        /// <param name="name">The updated name of the study set.</param>
        /// <param name="category">The updated category of the study set.</param>
        /// <param name="creator">The updated creator of the study set.</param>
        /// <param name="contributors">The updated contributors of the study set.</param>
        /// <param name="questions">The updated questions in the study set.</param>
        /// <param name="userStudysetConnections">The updated connections associated with the study set.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<Studyset> UpdateStudysetAsync(Guid id, string name, Category category, User creator, List<User> contributors, List<BaseQuestion> questions, List<UserStudysetConnection> userStudysetConnections)
        {
            var studyset = (await _studysetDataAccess.LoadAsync(o => o.Id == id)).FirstOrDefault();

            if (studyset is null)
            {
                throw new NullReferenceException(nameof(studyset));
            }
            studyset.Name = name;
            studyset.Category = category;
            studyset.Questions = questions;
            studyset.Contributors = contributors;
            studyset.Connections = userStudysetConnections;

            await _studysetDataAccess.UpdateAsync(studyset!);

            return studyset;
        }

        /// <summary>
        /// Retrieves a study set asynchronously by its ID.
        /// </summary>
        /// <param name="id">The ID of the study set to retrieve.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<Studyset?> GetStudysetViaIdAsync(Guid id)
        {
            return (await _studysetDataAccess.LoadAsync(o => o.Id == id)).FirstOrDefault();
        }

        /// <summary>
        /// Creates a connection between a user and a study set.
        /// </summary>
        /// <param name="studyset">The study set.</param>
        /// <param name="user">The user.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CreateConnection(Studyset studyset, User user)
        {
            var connection = studyset.Connections.FirstOrDefault(c => c.User == user);

            if (connection is null)
            {
                connection = new UserStudysetConnection(user, studyset, false);
                studyset.Connections.Add(connection);
                await _studysetDataAccess.UpdateAsync(studyset!);
            }
        }
    }
}
