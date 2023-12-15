﻿using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.ServiceLayer.DataAccessLayer.Repository.Interface
{
    public abstract class DataAccessBase<TModel>
    {
        protected readonly AppDatabaseContext<User> dbContext;

        public DataAccessBase(AppDatabaseContext<User> dbContext)
        {
            this.dbContext = dbContext;
        }

        public abstract Task<int> CountAsync();
        public abstract Task<bool> ExistsAsync(TModel model);
        public abstract Task<IQueryable<TModel>> LoadQueryableAsync();
        public abstract Task<IEnumerable<TModel>> LoadAsync(Func<TModel, bool> where);
        public abstract Task<TModel> GetAsync(Guid id);
        public abstract Task<TModel> AddAsync(TModel model);
        public abstract Task UpdateAsync(TModel model);
        public abstract Task DeleteAsync(TModel model);
    }
}
