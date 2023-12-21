using Quixduell.ServiceLayer.DataAccessLayer.Model;
using System.ComponentModel.DataAnnotations;

namespace Quixduell.Blazor.EditFormModel
{
    public class CreateEditCategoryFormModel
    {
        [StringLength(50, MinimumLength = 5)]
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
