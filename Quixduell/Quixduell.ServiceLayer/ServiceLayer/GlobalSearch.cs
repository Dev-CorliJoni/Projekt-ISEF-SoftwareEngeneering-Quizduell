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

        public async Task<List<Studyset>> Search(string? name = null, User? user = null, string? categoryName = null)
        {
            return await (await _studysetDataAccess.LoadTopByParamsAsync(name, user, categoryName)).ToListAsync();
        }


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
