using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.EditFormModel
{
    public class CreateEditCategoryFormModel
    {
        public string Name { get; set; } = string.Empty;
        public Guid Id { get; set; } = Guid.Empty;


        public CreateEditCategoryFormModel()
        {
            
        }

        public CreateEditCategoryFormModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }

    }
}
