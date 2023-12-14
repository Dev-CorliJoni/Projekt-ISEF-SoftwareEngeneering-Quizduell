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
    }
}
