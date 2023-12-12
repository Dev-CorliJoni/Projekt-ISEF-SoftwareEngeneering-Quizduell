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
        private readonly User _user;

        internal GlobalSearch(StudysetDataAccess studysetDataAccess, User user)
        {
            _studysetDataAccess = studysetDataAccess;
            _user = user;
        }

        public async Task<List<Studyset>> Search(string name)
        {
             return await (await _studysetDataAccess.LoadTopByNameAsync(name)).ToListAsync();
        }

        public async Task SaveStudyset(Studyset studyset)
        {
            if(_user.StudysetConnections.Any(sc => sc.Studyset.Name == studyset.Name) == false)
            {
                var studysetUserConnection = new UserStudysetConnection(_user, studyset);
                _user.StudysetConnections.Add(studysetUserConnection);
                studyset.Connections.Add(studysetUserConnection);
                await _studysetDataAccess.UpdateAsync(studyset);
            }
        }
    }
}
