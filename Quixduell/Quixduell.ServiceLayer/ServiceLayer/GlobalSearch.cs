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
        public static async Task<List<Studyset>> Search(AppDatabaseContext<User> context, string name)
        {
            return (await new StudysetDataAccess(context).LoadTopByNameAsync(name)).ToList();
        }

        //public static async Task Save(AppDatabaseContext<User> context, Studyset studyset)
        //{
        //    studyset.Connections.Add(new UserStudysetConnection(context))

        //    return (await new StudysetDataAccess(context).LoadTopByNameAsync(name)).ToList();
        //}
    }
}
