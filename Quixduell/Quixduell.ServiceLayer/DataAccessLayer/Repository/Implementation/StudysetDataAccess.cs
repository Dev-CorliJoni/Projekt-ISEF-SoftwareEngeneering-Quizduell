using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Answers;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Interface;

namespace Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation
{
    /// <summary>
    /// Represents the data access layer for study sets.
    /// </summary>
    public class StudysetDataAccess : DataAccessBase<Studyset>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StudysetDataAccess"/> class.
        /// </summary>
        /// <param name="connectionFactory">The database connection factory.</param>
        public StudysetDataAccess(DBConnectionFactory connectionFactory) : base(connectionFactory) { }

        /// <summary>
        /// Adds a new study set asynchronously.
        /// </summary>
        /// <param name="model">The study set to add.</param>
        /// <returns>The added study set.</returns>
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
            return await (await LoadQueryableAsync()).SingleAsync(s => s.Id == id);
        }

        public async Task<Studyset> GetAsync(string name)
        {
            return await (await LoadQueryableAsync()).SingleAsync(o => o.Name == name);
        }

        /// <summary>
        /// Loads the top study sets by specified parameters asynchronously.
        /// </summary>
        /// <param name="name">The name of the study set.</param>
        /// <param name="containsUser">The user contained in the study set.</param>
        /// <param name="categoryName">The name of the category.</param>
        /// <param name="amount">The number of study sets to load.</param>
        /// <returns>An IQueryable of study sets.</returns>
        public async Task<IQueryable<Studyset>> LoadTopByParamsAsync(string? name = null, User? containsUser = null, string? categoryName = null, int amount = 50)
        {
            var result = await LoadQueryableAsync();

            if (!String.IsNullOrWhiteSpace(name))
            {
                result = result.Where((s) => EF.Functions.Like(s.Name, $"%{name}%"));
            }

            if (!String.IsNullOrWhiteSpace(categoryName))
            {
                result = result.Where((s) => EF.Functions.Like(s.Category.Name, $"%{categoryName}%"));
            }

            if (containsUser is not null)
            {
                result = result.Where(o => o.Creator.Id == containsUser.Id || o.Contributors.Any(o => o.Id == containsUser.Id) ||
                                        (o.Connections.Any(i => i.User.Id == containsUser.Id && i.IsStored)));
            }

            return result.Take(amount);
        }

        /// <summary>
        /// Loads study sets asynchronously.
        /// </summary>
        /// <param name="where">The condition to filter study sets.</param>
        /// <returns>A collection of study sets.</returns>
        public override async Task<IEnumerable<Studyset>> LoadAsync(Func<Studyset, bool>? where = null)
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

        /// <summary>
        /// Loads an IQueryable of study sets asynchronously.
        /// </summary>
        /// <returns>An IQueryable of study sets.</returns>
        public override async Task<IQueryable<Studyset>> LoadQueryableAsync()
        {
            return await Task.Run(() =>
            {
                return dbContext.Studysets
                    .Include(o => o.Creator)
                    .Include(o => o.Connections)
                        .ThenInclude(c => c.Rating)
                    .Include(o => o.Connections)
                        .ThenInclude(c => c.User)
                    .Include(o => o.Category)
                    .Include(o => o.Contributors)
                    .Include(o => o.UsersRequestedToBecomeContributor)
                    .Include(o => o.Questions)
                        .ThenInclude(o => ((MultipleChoiceQuestion)o).Answers)
                    .Include(o => o.Questions)
                        .ThenInclude(o => ((OpenQuestion)o).Answer);
            });
        }

        /// <summary>
        /// Updates a study set asynchronously.
        /// </summary>
        /// <param name="model">The study set to update.</param>
        public override async Task UpdateAsync(Studyset model)
        {
            this.dbContext.Studysets.Update(model);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
