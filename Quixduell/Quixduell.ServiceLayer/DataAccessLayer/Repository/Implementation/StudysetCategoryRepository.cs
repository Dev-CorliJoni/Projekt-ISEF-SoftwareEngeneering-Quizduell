using Microsoft.EntityFrameworkCore;
using Quixduell.Blazor.Data;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Interface;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.RepositoryException;

namespace Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation
{
    /// <summary>
    /// Repository for managing Studyset Categories in the database.
    /// </summary>
    internal class StudysetCategoryRepository : IStudysetCategoryRepository
    {
        private readonly AppDatabaseContext<User> _dbContext;

        //public StudysetCategoryRepository(AppDatabaseContext<User> dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        ///// <summary>
        ///// Retrieves all categories asynchronously from the database.
        ///// </summary>
        ///// <returns>An enumerable collection of categories.</returns>
        //public async Task<IEnumerable<Category>> GetCategoriesAsync()
        //{
        //    return await _dbContext.Categories.ToListAsync();
        //}

        ///// <summary>
        ///// Retrieves a category by its ID asynchronously.
        ///// </summary>
        ///// <param name="id">The ID of the category to retrieve.</param>
        ///// <returns>The category if found, otherwise null.</returns>
        //public async Task<Category?> GetCategoryByIDAsync(Guid id)
        //{
        //    return await _dbContext.Categories.FirstOrDefaultAsync(o => o.CategoryID == id);
        //}

        ///// <summary>
        ///// Creates a new category asynchronously in the database.
        ///// </summary>
        ///// <param name="category">The category to create.</param>
        //public async Task CreateCategoryAsync(Category category)
        //{
        //    await _dbContext.Categories.AddAsync(category);
        //    await _dbContext.SaveChangesAsync();
        //}

        ///// <summary>
        ///// Updates an existing category asynchronously in the database.
        ///// </summary>
        ///// <param name="category">The updated category information.</param>
        ///// <exception cref="CategoryNotFoundException">Throws if Category not found</exception>
        //public async Task UpdateCategoryAsync(Category category)
        //{
        //    var obj = await GetCategoryByIDAsync(category.CategoryID);

        //    if (obj is null)
        //    {
        //        throw new CategoryNotFoundException(category.CategoryID);
        //    }

        //    obj.Name = category.Name;

        //    await _dbContext.SaveChangesAsync();
        //}

        ///// <summary>
        ///// Deletes a category by its ID asynchronously from the database.
        ///// </summary>
        ///// <param name="id">The ID of the category to delete.</param>
        //public async Task DeleteCategoryAsync(Guid id)
        //{
        //    var obj = await GetCategoryByIDAsync(id);

        //    if (obj is null)
        //    {
        //        return;
        //    }

        //    _dbContext.Categories.Remove(obj);
        //    await _dbContext.SaveChangesAsync();
        //}
    }
}
