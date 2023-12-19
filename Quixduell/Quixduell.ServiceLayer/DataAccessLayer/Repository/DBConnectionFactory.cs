using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Options;

namespace Quixduell.ServiceLayer.DataAccessLayer.Repository
{
    public class DBConnectionFactory
    {
        private readonly IOptions<DataAccessOptions> _options;

        public DBConnectionFactory(IOptions<DataAccessOptions> options)
        {
            _options = options;
        }

        public AppDatabaseContext<User> GetAppDatabaseContext ()
        {
            var options = new DbContextOptionsBuilder<AppDatabaseContext<User>>()
                .UseSqlServer(_options.Value.ConnectionString);

            return new AppDatabaseContext<User>(options.Options);
        }
    }
}
