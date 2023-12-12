﻿using Microsoft.EntityFrameworkCore;
using Quixduell.Blazor.Data;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation
{
    internal class StudysetDataAccess : DataAccessBase<Studyset>
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

        public async Task<IQueryable<Studyset>> LoadTopByNameAsync(string name, int amount = 50)
        {
            return (await LoadQueryableAsync()).Where(s => EF.Functions.Like(s.Name, $"*{name}*")).Take(amount);
        }

        public override async Task<IEnumerable<Studyset>> LoadAsync(Func<Studyset, bool> where = null)
        {
            var results = await LoadQueryableAsync();

            if (where != null)
            {
                return results.Where(where);
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
                return dbContext.Studysets;
            });
        }

        public override async Task UpdateAsync(Studyset model)
        {
            this.dbContext.Studysets.Update(model);
            await this.dbContext.SaveChangesAsync();
        }
    }
}