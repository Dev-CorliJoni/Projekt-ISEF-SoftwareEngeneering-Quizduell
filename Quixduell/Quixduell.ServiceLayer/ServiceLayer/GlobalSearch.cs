using Microsoft.EntityFrameworkCore;
using Quixduell.Blazor.Data;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quixduell.ServiceLayer.ServiceLayer
{
    public class GlobalSearch
    {
        private readonly StudysetDataAccess _studysetDataAccess;

        internal GlobalSearch(StudysetDataAccess studysetDataAccess)
        {
            _studysetDataAccess = studysetDataAccess;
        }

        public async Task<List<Studyset>> Search(string name)
        {
             return await (await _studysetDataAccess.LoadTopByNameAsync(name)).ToListAsync();
        }

        public async Task SaveStudyset(User user, Studyset studyset)
        {
            if(user.StudysetConnections.Any(sc => sc.Studyset.Name == studyset.Name) == false)
            {
                var studysetUserConnection = new UserStudysetConnection(user, studyset, new Rating());
                user.StudysetConnections.Add(studysetUserConnection);
                studyset.Connections.Add(studysetUserConnection);
                await _studysetDataAccess.UpdateAsync(studyset);
            }
        }
    }
}
