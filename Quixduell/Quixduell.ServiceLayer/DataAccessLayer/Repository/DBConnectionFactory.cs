using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Options;

namespace Quixduell.ServiceLayer.DataAccessLayer.Repository
{
    public class DBConnectionFactory
    {

        private readonly AppDatabaseContext<User> _databaseContext;

        public DBConnectionFactory(AppDatabaseContext<User> databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public AppDatabaseContext<User> GetAppDatabaseContext ()
        {
            return _databaseContext;
        }
    }
}
