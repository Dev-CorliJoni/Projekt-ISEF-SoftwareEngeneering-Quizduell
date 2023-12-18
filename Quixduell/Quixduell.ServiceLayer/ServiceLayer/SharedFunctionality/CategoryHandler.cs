using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;

namespace Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality
{
    public class CategoryHandler
    {
        private readonly CategoryDataAccess _categoryDataAccess;

        public CategoryHandler(CategoryDataAccess categoryDataAccess)
        {
            _categoryDataAccess = categoryDataAccess;
        }

        public async Task<Category> AddCategoryAsync(string categoryName)
        {
            var cat = (await _categoryDataAccess.LoadByNameAsync(categoryName)).FirstOrDefault();
            if (cat is not null)
                return cat;

            return await _categoryDataAccess.AddAsync(new Category(categoryName));
        }

        public async Task<Category> UpdateCategoryAsync (Guid id, string categoryName)
        {
            var cat = (await _categoryDataAccess.LoadAsync(o => o.Id == id)).FirstOrDefault();

            if (cat is not null)
            {
                cat.Name = categoryName;
                await _categoryDataAccess.UpdateAsync(cat);
                return cat;
            }
            cat = new Category(categoryName);
            return await _categoryDataAccess.AddAsync (cat);

        }

        public async Task<List<Category>> SearchCategoryAsync(string name)
        {
            return await (await _categoryDataAccess.LoadByNameAsync(name)).ToListAsync();
        }

        
    }
}
