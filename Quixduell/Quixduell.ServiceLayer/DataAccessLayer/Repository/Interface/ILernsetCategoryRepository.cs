using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.RepositoryException;

namespace Quixduell.ServiceLayer.DataAccessLayer.Repository.Interface
{
    /// <summary>
    /// Interface for managing Lernset Categories in the database.
    /// </summary>
    public interface ILernsetCategoryRepository
    {
        /// <summary>
        /// Retrieves all categories asynchronously from the database.
        /// </summary>
        /// <returns>An enumerable collection of categories.</returns>
        Task<IEnumerable<Category>> GetCategoriesAsync();

        /// <summary>
        /// Retrieves a category by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The category if found, otherwise null.</returns>
        Task<Category?> GetCategoryByIDAsync(Guid id);

        /// <summary>
        /// Creates a new category asynchronously in the database.
        /// </summary>
        /// <param name="category">The category to create.</param>
        Task CreateCategoryAsync(Category category);

        /// <summary>
        /// Updates an existing category asynchronously in the database.
        /// </summary>
        /// <param name="category">The updated category information.</param>
        /// <exception cref="CategoryNotFoundException">Throws if Category not found</exception>
        Task UpdateCategoryAsync(Category category);

        /// <summary>
        /// Deletes a category by its ID asynchronously from the database.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        Task DeleteCategoryAsync(Guid id);
    }
}
