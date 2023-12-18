using Microsoft.AspNetCore.Components;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.Shared.CategoryComponent
{
    public partial class CreateEditCategory
    {
        [Parameter]
        public Category? Value { get; set; }

        [Parameter]
        public EventCallback<Category> ValueChanged { get; set; }
    }
}
