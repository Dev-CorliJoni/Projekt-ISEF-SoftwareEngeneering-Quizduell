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

        public async Task<List<Studyset>> Search(string? name, User? user=null)
        {
             return await (await _studysetDataAccess.LoadTopByParamsAsync(name, user)).ToListAsync();
        }

        public async Task StoreStudyset(Studyset studyset, User currentUser)
        {
            await Task.Run(() =>
            {
                if (currentUser.StudysetConnections.Any(sc => sc.Studyset.Name == studyset.Name) == false)
                {
                    var studysetUserConnection = new UserStudysetConnection(currentUser, studyset, true);
                    currentUser.StudysetConnections.Add(studysetUserConnection);
                    studyset.Connections.Add(studysetUserConnection);
                }
                else
                {
                    var connection = studyset.Connections.Find(sc => sc.Studyset.Name == studyset.Name)!;
                    connection.IsStored = true;
                }
            });
            
            await _studysetDataAccess.UpdateAsync(studyset);
        }
    }
}
