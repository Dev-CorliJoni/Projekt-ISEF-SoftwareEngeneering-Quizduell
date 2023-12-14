﻿using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Interface;

namespace Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation
{
    public class StudysetDataAccess : DataAccessBase<Studyset>
    {
        public StudysetDataAccess(AppDatabaseContext<User> dbContext) : base(dbContext) {  }

        public override async Task AddAsync(Studyset model)
        {
            await this.dbContext.Studysets.AddAsync(model);
            await this.dbContext.SaveChangesAsync();
        }

        public override async Task<int> Count()
        {
            return await this.dbContext.Studysets.CountAsync();
        }

        public override async Task DeleteAsync(Studyset model)
        {
            this.dbContext.Studysets.Remove(model);
            await this.dbContext.SaveChangesAsync();
        }

        public override async Task<Studyset> GetAsync(Guid id)
        {
            return await this.dbContext.Studysets.SingleAsync(s => s.Id == id);
        }

        public async Task<IQueryable<Studyset>> LoadTopByParamsAsync(string? name = null, User? user = null, string? categoryName = null, int amount = 50)
        {
            var result = await LoadQueryableAsync();
            if (name is not null)
            {
                result = result.Where((s) => EF.Functions.Like(s.Name, $"%{name}%"));
            }
            if (user is not null)
            {
                result = result.Where((s) => s.Creator == user || s.Contributors.Contains(user));
            }
            if (categoryName is not null)
            {
                result = result.Where((s) => EF.Functions.Like(s.Category.Name, $"%{categoryName}%"));
            }


            return result.Take(amount);
        }

        public override async Task<IEnumerable<Studyset>> LoadAsync(Func<Studyset, bool>? where = null)
        {
            var results = await LoadQueryableAsync();

            if (where != null)
            {
                return results.Include(o => o.Questions).Where(where);
            }
            else 
            {   
                return results; 
            }
        }

        public override async Task<IQueryable<Studyset>> LoadQueryableAsync()
        {
            return await Task.Run(() =>
            {
                return dbContext.Studysets
                .Include(o => o.Questions)
                .Include(o => o.Creator)
                .Include(o => o.Connections)
                .Include(o => o.Category)
                .Include(o => o.Contributors);
            });
        }

        public override async Task UpdateAsync(Studyset model)
        {
            this.dbContext.Studysets.Update(model);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
