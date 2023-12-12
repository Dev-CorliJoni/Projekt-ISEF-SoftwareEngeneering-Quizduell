using Microsoft.EntityFrameworkCore;
using Quixduell.Blazor.Data;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Interface;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.RepositoryException;
using System.Linq;

namespace Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation
{
    /// <summary>
    /// Repository for managing Studyset Categories in the database.
    /// </summary>
    internal class CategoryDataAccess : DataAccessBase<Category>, ICategoryDataAccess
    {


        public CategoryDataAccess(AppDatabaseContext<User> dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Creates a new category asynchronously in the database.
        /// </summary>
        /// <param name="model">The category to create.</param>
        public override async Task AddAsync(Category model)
        {
            await dbContext.Categories.AddAsync(model);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Count the elements of type <see cref="Category"/>
        /// </summary>
        /// <returns>Count of elements</returns>
        public override async Task<int> Count()
        {
            return await dbContext.Categories.CountAsync();
        }

        ///// <summary>
        ///// Deletes a category by its ID asynchronously from the database.
        ///// </summary>
        ///// <param name="id">The ID of the category to delete.</param>
        public override async Task DeleteAsync(Category model)
        {
            dbContext.Categories.Remove(model);
            await dbContext.SaveChangesAsync();
        }

        ///// <summary>
        ///// Retrieves a category by its ID asynchronously.
        ///// </summary>
        ///// <param name="id">The ID of the category to retrieve.</param>
        ///// <returns>The category if found, otherwise null.</returns>
        public override async Task<Category> GetAsync(Guid id)
        {
            return await dbContext.Categories.SingleAsync(o => o.Id == id);
        }

        /// <summary>
        /// Retrieves a set of categories from the database.
        /// </summary>
        /// <returns>An enumerable collection of categories.</returns>
        public override async Task<IEnumerable<Category>> LoadAsync(Func<Category, bool> where = null)
        {
            var results = await LoadQueryableAsync();
            if (where is not null)
            {
                return results.Where(where);
            }
            return results;
        }

        public override async Task<IQueryable<Category>> LoadQueryableAsync()
        {
            return await Task.Run(() =>
            {
                return dbContext.Categories;
            });
        }



        /// <summary>
        /// Updates an existing category asynchronously in the database.
        /// </summary>
        /// <param name="category">The updated category information.</param>
        /// <exception cref="CategoryNotFoundException">Throws if Category not found</exception>
        public override async Task UpdateAsync(Category model)
        {
            dbContext.Update(model);
            await dbContext.SaveChangesAsync();
        }

    }
}
