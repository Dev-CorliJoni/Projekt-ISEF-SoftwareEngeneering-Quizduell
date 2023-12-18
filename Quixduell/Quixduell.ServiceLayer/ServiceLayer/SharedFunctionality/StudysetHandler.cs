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
            var Studyset = new Studyset(name, category, creator, new List<User>(), new List<BaseQuestion>());
            return await _studysetDataAccess.AddAsync(Studyset);
        }

        public async Task<Studyset?> GetStudysetViaIDAsync(Guid id)
        {
            return (await _studysetDataAccess.LoadAsync(o => o.Id == id)).FirstOrDefault();
        }
    }
}
