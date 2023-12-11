using Quixduell.Blazor.Data;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quixduell.ServiceLayer.DataAccessLayer.Repository.Interface
{
    public abstract class DatabaseBase
    {
        private readonly AppDatabaseContext<User> dbContext;

        public DatabaseBase(AppDatabaseContext<User> dbContext)
        {
            this.dbContext = dbContext;
        }

        public abstract Task<IEnumerable<TModel>> LoadAsync<TModel>();
        public abstract Task<TModel> GetAsync<TModel>(Guid id);
        public abstract Task AddAsync<TModel>(TModel model);
        public abstract Task UpdateAsync<TModel>(TModel model);
        public abstract Task DeleteAsync<TModel>(TModel model);
    }
}
