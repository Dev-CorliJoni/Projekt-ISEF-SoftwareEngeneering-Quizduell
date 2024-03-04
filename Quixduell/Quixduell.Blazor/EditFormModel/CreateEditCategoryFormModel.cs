using Quixduell.ServiceLayer.DataAccessLayer.Model;
using System.ComponentModel.DataAnnotations;

namespace Quixduell.Blazor.EditFormModel
{
    public class CreateEditCategoryFormModel
    {
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Der Name muss eine Länge zwischen {1} und {2} haben")]
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
