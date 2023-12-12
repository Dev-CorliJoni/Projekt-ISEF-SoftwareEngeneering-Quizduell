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

        public  async Task<List<Studyset>> Search(string name)
        {
             return await (await _studysetDataAccess.LoadTopByNameAsync(name)).ToListAsync();
        }

        //public static async Task Save(AppDatabaseContext<User> context, Studyset studyset)
        //{
        //    studyset.Connections.Add(new UserStudysetConnection(context))

        //    return (await new StudysetDataAccess(context).LoadTopByNameAsync(name)).ToList();
        //}
    }
}
