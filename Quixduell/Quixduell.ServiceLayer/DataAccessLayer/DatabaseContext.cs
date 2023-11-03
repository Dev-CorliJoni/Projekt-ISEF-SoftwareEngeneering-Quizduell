using Microsoft.EntityFrameworkCore;

namespace Quixduell.ServiceLayer.DataAccessLayer
{
    internal class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
