using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;

namespace Quixduell.ServiceLayer.ServiceLayer
{
    public class GlobalSearch
    {
        private readonly StudysetDataAccess _studysetDataAccess;

        public GlobalSearch(StudysetDataAccess studysetDataAccess)
        {
            _studysetDataAccess = studysetDataAccess;
        }

        public async Task<List<Studyset>> Search(string name)
        {
             return await (await _studysetDataAccess.LoadTopByNameAsync(name)).ToListAsync();
        }

        public async Task SaveStudyset(Studyset studyset, User currentUser)
        {
            if(currentUser.StudysetConnections.Any(sc => sc.Studyset.Name == studyset.Name) == false)
            {
                var studysetUserConnection = new UserStudysetConnection(currentUser, studyset);
                currentUser.StudysetConnections.Add(studysetUserConnection);
                studyset.Connections.Add(studysetUserConnection);
                await _studysetDataAccess.UpdateAsync(studyset);
            }
        }
    }
}
