using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;

namespace Quixduell.ServiceLayer.ServiceLayer
{
    public class GlobalSearch
    {
        private readonly StudysetDataAccess _studysetDataAccess;
        private readonly CategoryDataAccess _categoryDataAccess;

        public GlobalSearch(StudysetDataAccess studysetDataAccess, CategoryDataAccess categoryDataAccess)
        {
            _studysetDataAccess = studysetDataAccess;
            _categoryDataAccess = categoryDataAccess;
        }

        public async Task<List<Studyset>> Search(string? name = null, User? user=null, string? categoryName = null)
        {
             return await (await _studysetDataAccess.LoadTopByParamsAsync(name, user, categoryName)).ToListAsync();
        }

        public async Task<List<Category>> SearchCategory(string name)
        {
            return await (await _categoryDataAccess.LoadByNameAsync(name)).ToListAsync();
        }

        public async Task StoreStudyset(Studyset studyset, User currentUser)
        {
            await Task.Run(() =>
            {
                if (currentUser.StudysetConnections.Any(sc => sc.Studyset.Name == studyset.Name) == false)
                {
                    var studysetUserConnection = new UserStudysetConnection(currentUser, studyset, true, new Rating(),0);
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
