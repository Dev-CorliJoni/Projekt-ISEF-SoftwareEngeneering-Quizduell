using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;

namespace Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality
{
    /// <summary>
    /// Represents a class for handling category operations.
    /// </summary>
    public class CategoryHandler
    {
        private readonly CategoryDataAccess _categoryDataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryHandler"/> class.
        /// </summary>
        /// <param name="categoryDataAccess">The data access object for categories.</param>
        public CategoryHandler(CategoryDataAccess categoryDataAccess)
        {
            _categoryDataAccess = categoryDataAccess;
        }

        /// <summary>
        /// Adds a new category asynchronously.
        /// </summary>
        /// <param name="categoryName">The name of the category to add.</param>
        /// <returns>The added category.</returns>
        public async Task<Category> AddCategoryAsync(string categoryName)
        {
            var cat = (await _categoryDataAccess.LoadByNameAsync(categoryName)).FirstOrDefault();
            if (cat is not null)
                return cat;

            return await _categoryDataAccess.AddAsync(new Category(categoryName));
        }

        /// <summary>
        /// Updates an existing category asynchronously.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="categoryName">The new name of the category.</param>
        /// <returns>The updated category.</returns>
        public async Task<Category> UpdateCategoryAsync(Guid id, string categoryName)
        {
            var cat = (await _categoryDataAccess.LoadAsync(o => o.Id == id)).FirstOrDefault();

            if (cat is not null)
            {
                cat.Name = categoryName;
                await _categoryDataAccess.UpdateAsync(cat);
                return cat;
            }
            cat = new Category(categoryName);
            return await _categoryDataAccess.AddAsync(cat);
        }

        /// <summary>
        /// Searches for categories by name asynchronously.
        /// </summary>
        /// <param name="name">The name of the category to search for.</param>
        /// <returns>A list of categories matching the specified name.</returns>
        public async Task<List<Category>> SearchCategoryAsync(string name)
        {
            return (await _categoryDataAccess.LoadByNameAsync(name)).ToList();
        }
    }
}
