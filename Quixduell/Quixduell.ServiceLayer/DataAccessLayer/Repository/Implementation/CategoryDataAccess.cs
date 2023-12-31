﻿using Microsoft.EntityFrameworkCore;
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


        public CategoryDataAccess(DBConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        /// <summary>
        /// Creates a new category asynchronously in the database.
        /// </summary>
        /// <param name="model">The category to create.</param>
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

        ///// <summary>
        ///// Deletes a category by its ID asynchronously from the database.
        ///// </summary>
        ///// <param name="id">The ID of the category to delete.</param>
        public override async Task DeleteAsync(Category model)
        {
            dbContext.Categories.Remove(model);
            await dbContext.SaveChangesAsync();
        }

        public override async Task<bool> ExistsAsync(Category model)
        {
            return await dbContext.Categories.ContainsAsync(model);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await dbContext.Categories.AnyAsync(s => s.Name == name);
        }

        ///// <summary>
        ///// Retrieves a category by its ID asynchronously.
        ///// </summary>
        ///// <param name="id">The ID of the category to retrieve.</param>
        ///// <returns>The category if found, otherwise null.</returns>
        public override async Task<Category?> GetAsync(Guid id)
        {
            return await dbContext.Categories.SingleAsync(o => o.Id == id);
        }

        ///// <summary>
        ///// Retrieves a category by its ID asynchronously.
        ///// </summary>
        ///// <param name="id">The ID of the category to retrieve.</param>
        ///// <returns>The category if found, otherwise null.</returns>
        public async Task<Category?> GetAsync(string name)
        {
            return await dbContext.Categories.SingleOrDefaultAsync(o => o.Name == name);
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

        /// <summary>
        /// Retrieves a set of categories compare with LIKE on param
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IQueryable<Category>> LoadByNameAsync(string name, int amount = 50)
        {
            var results = await LoadQueryableAsync();
            return results.Where(c => EF.Functions.Like(c.Name, $"%{name}%")).Take(amount);
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
