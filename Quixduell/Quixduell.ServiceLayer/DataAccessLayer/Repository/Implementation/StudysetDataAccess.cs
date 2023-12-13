﻿using Microsoft.EntityFrameworkCore;
using Quixduell.Blazor.Data;
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

        public async Task<IQueryable<Studyset>> LoadTopByParamsAsync(string? name, User? user, int amount = 50)
        {
            List<Func<Studyset, bool>> conditions = new List<Func<Studyset, bool>> 
            { 
                (s) => name != null ? EF.Functions.Like(s.Name, $"%{name}%") : true,
                (s) => user != null ? s.Creator == user || s.Contributors.Contains(user) : true
            };

            return (await LoadQueryableAsync())
                .Where(s => conditions.All(condition => condition(s)))
                .Take(amount);
        }

        public override async Task<IEnumerable<Studyset>> LoadAsync(Func<Studyset, bool> where = null)
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
