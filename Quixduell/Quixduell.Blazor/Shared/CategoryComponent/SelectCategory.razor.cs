using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality;

namespace Quixduell.Blazor.Shared.CategoryComponent
{
    public partial class SelectCategory
    {
        [Parameter]
        public Category? Value { get; set; }

        [Parameter]
        public EventCallback<Category> ValueChanged { get; set; }


        private List<Category>? _categories = null;
        private string _selectedId;

        private string SelectedId
        {
            get => _selectedId; set
            { 
                _selectedId = value;
                if (_categories is not null)
                {
                    Value = _categories.FirstOrDefault(o => o.Id == Guid.Parse(_selectedId));
                    ValueChanged.InvokeAsync(Value);
                }
            }
        }

        [Inject]
        private CategoryHandler CategoryHandler { get; set; } = default!;


        protected override async Task OnInitializedAsync()
        {
            _categories = await CategoryHandler.SearchCategoryAsync("");
            await base.OnInitializedAsync();
        }

        protected override Task OnParametersSetAsync()
        {
            return base.OnParametersSetAsync();
        }

    }
}
