using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Interface;

namespace Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation
{
    public class StudysetDataAccess : DataAccessBase<Studyset>
    {
        public StudysetDataAccess(AppDatabaseContext<User> dbContext) : base(dbContext) {  }

        public override async Task<Studyset> AddAsync(Studyset model)
        {
            await this.dbContext.Studysets.AddAsync(model);
            await this.dbContext.SaveChangesAsync();
            return model;
        }

        public override async Task<int> CountAsync()
        {
            return await this.dbContext.Studysets.CountAsync();
        }

        public override async Task<bool> ExistsAsync(Studyset model)
        {
            return await dbContext.Studysets.ContainsAsync(model);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await dbContext.Studysets.AnyAsync(s => s.Name == name);
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

        public async Task<Studyset> GetAsync(string name)
        {
            return await dbContext.Studysets.SingleAsync(o => o.Name == name);
        }

        public async Task<IQueryable<Studyset>> LoadTopByParamsAsync(string? name = null, User? creatorOrContributor = null, User? userHasStored = null, string? categoryName = null, int amount = 50)
        {
            var result = await LoadQueryableAsync();
            if (name is not null)
            {
                result = result.Where((s) => EF.Functions.Like(s.Name, $"%{name}%"));
            }
            if (creatorOrContributor is not null)
            {
                result = result.Where((s) => s.Creator == creatorOrContributor || s.Contributors.Contains(creatorOrContributor));
            }
            if (userHasStored is not null)
            { 
               result = result.Where((s) => s.Connections.Any(con => con.User.Id == userHasStored.Id && con.IsStored == true));
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
