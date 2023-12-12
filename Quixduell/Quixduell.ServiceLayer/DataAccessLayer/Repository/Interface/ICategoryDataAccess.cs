using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.ServiceLayer.DataAccessLayer.Repository.Interface
{
    internal interface ICategoryDataAccess
    {
        /// <summary>
        /// Creates a new category asynchronously in the database.
        /// </summary>
        /// <param name="model">The category to create</param>
        Task AddAsync(Category model);
        /// <summary>
        /// Counts the elements from type <see cref="Category"/>
        /// </summary>
        /// <returns></returns>
        Task<int> Count();
        /// <summary>
        /// Delete element synchronously in the databas
        /// </summary>
        /// <param name="model">The category to delete</param>
        /// <returns></returns>
        Task DeleteAsync(Category model);
        /// <summary>
        /// Get <see cref="Category" by id/>
        /// </summary>
        /// <param name="id">The id of the element</param>
        /// <returns></returns>
        Task<Category> GetAsync(Guid id);
        /// <summary>
        /// Get a subset of <see cref="Category"/>
        /// </summary>
        /// <param name="where">Query function</param>
        /// <returns></returns>
        Task<IEnumerable<Category>> LoadAsync(Func<Category, bool> where);
        Task<IQueryable<Category>> LoadQueryableAsync();
        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The category to update</returns>
        Task UpdateAsync(Category model);
    }
}