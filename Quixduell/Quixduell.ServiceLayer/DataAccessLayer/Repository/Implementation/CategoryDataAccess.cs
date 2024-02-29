using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Interface;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.RepositoryException;

namespace Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation
{
    /// <summary>
    /// Repository for managing Studyset Categories in the database.
    /// </summary>
    public class CategoryDataAccess : DataAccessBase<Category>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryDataAccess"/> class.
        /// </summary>
        /// <param name="connectionFactory">The database connection factory.</param>
        public CategoryDataAccess(DBConnectionFactory connectionFactory) : base(connectionFactory) { }

        /// <summary>
        /// Adds a new category asynchronously to the database.
        /// </summary>
        /// <param name="model">The category to add.</param>
        /// <returns>The added category.</returns>
        public override async Task<Category> AddAsync(Category model)
        {
            await dbContext.Categories.AddAsync(model);
            await dbContext.SaveChangesAsync();
            return model;
        }

        /// <summary>
        /// Count the elements of type <see cref="Category"/>
        /// </summary>
        /// <returns>Count of elements</returns>
        public override async Task<int> CountAsync()
        {
            return await dbContext.Categories.CountAsync();
        }

        /// <summary>
        /// Deletes a category asynchronously from the database.
        /// </summary>
        /// <param name="model">The category to delete.</param>
        public override async Task DeleteAsync(Category model)
        {
            dbContext.Categories.Remove(model);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if a category exists in the database asynchronously.
        /// </summary>
        /// <param name="model">The category to check.</param>
        /// <returns>True if the category exists, otherwise false.</returns>
        public override async Task<bool> ExistsAsync(Category model)
        {
            return await dbContext.Categories.ContainsAsync(model);
        }

        /// <summary>
        /// Checks if a category with the specified name exists in the database asynchronously.
        /// </summary>
        /// <param name="name">The name of the category to check.</param>
        /// <returns>True if the category exists, otherwise false.</returns>
        public async Task<bool> ExistsAsync(string name)
        {
            return await dbContext.Categories.AnyAsync(s => s.Name == name);
        }

        /// <summary>
        /// Retrieves a category by its ID asynchronously from the database.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The retrieved category.</returns>
        public override async Task<Category?> GetAsync(Guid id)
        {
            return await dbContext.Categories.SingleAsync(o => o.Id == id);
        }

        /// <summary>
        /// Retrieves a category by its name asynchronously from the database.
        /// </summary>
        /// <param name="name">The name of the category to retrieve.</param>
        /// <returns>The retrieved category.</returns>
        public async Task<Category?> GetAsync(string name)
        {
            return await dbContext.Categories.SingleOrDefaultAsync(o => o.Name == name);
        }

        /// <summary>
        /// Retrieves categories from the database asynchronously.
        /// </summary>
        /// <param name="where">The condition to filter categories.</param>
        /// <returns>A collection of categories.</returns>
        public override async Task<IEnumerable<Category>> LoadAsync(Func<Category, bool>? where = null)
        {
            var results = await LoadQueryableAsync();
            if (where is not null)
            {
                return results.Where(where);
            }
            return results;
        }

        /// <summary>
        /// Retrieves a set of categories compare with LIKE on param
        /// </summary>
        /// <param name="name">The name of the category to search.</param>
        /// <param name="amount">The number of categories to retrieve.</param>
        /// <returns>An IQueryable of categories.</returns>
        public async Task<IQueryable<Category>> LoadByNameAsync(string name, int amount = 50)
        {
            var results = await LoadQueryableAsync();
            return results.Where(c => EF.Functions.Like(c.Name, $"%{name}%")).Take(amount);
        }

        /// <summary>
        /// Retrieves an IQueryable of categories from the database asynchronously.
        /// </summary>
        /// <returns>An IQueryable of categories.</returns>
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
        /// <param name="model">The updated category information.</param>
        public override async Task UpdateAsync(Category model)
        {
            dbContext.Update(model);
            await dbContext.SaveChangesAsync();
        }
    }
}
