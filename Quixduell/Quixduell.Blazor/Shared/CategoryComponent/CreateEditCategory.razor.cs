using Microsoft.AspNetCore.Components;
using Quixduell.Blazor.EditFormModel;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality;

namespace Quixduell.Blazor.Shared.CategoryComponent
{
    public partial class CreateEditCategory
    {

        [Parameter]
        public Category? Value { get; set; }

        [Parameter]
        public EventCallback<Category> ValueChanged { get; set; }

        [Inject]
        private CategoryHandler CategoryHandler { get; set; } = default!;

        private CreateEditCategoryFormModel? _formModel = null;

        private async Task SaveCategory ()
        {
            if (_formModel?.Id == Guid.Empty) 
            {
                var cat  = await CategoryHandler.AddCategoryAsync(_formModel.Name);
                _formModel = null;
                Value = cat;
                return;
            }
            await CategoryHandler.UpdateCategoryAsync(_formModel!.Id, _formModel.Name);
            _formModel = null;
        }


    }
}
