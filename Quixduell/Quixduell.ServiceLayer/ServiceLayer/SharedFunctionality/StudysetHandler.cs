using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;

namespace Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality
{
    public class StudysetHandler
    {
        private readonly StudysetDataAccess _studysetDataAccess;

        public StudysetHandler(StudysetDataAccess studysetDataAccess)
        {
            _studysetDataAccess = studysetDataAccess;
        }

        public async Task<Studyset> AddStudysetAsync (string name, Category category, User creator)
        {
            return await AddStudysetAsync(name,category,creator, new List<User>(), new List<BaseQuestion>());
        }

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

        public async Task<Studyset?> GetStudysetViaIdAsync(Guid id)
        {
            return (await _studysetDataAccess.LoadAsync(o => o.Id == id)).FirstOrDefault();
        }
    }
}
