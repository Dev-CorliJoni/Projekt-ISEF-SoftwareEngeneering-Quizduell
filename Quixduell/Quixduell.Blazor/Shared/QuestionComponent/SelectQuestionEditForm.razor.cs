using Microsoft.AspNetCore.Components;
using Quixduell.Blazor.EditFormModel;

namespace Quixduell.Blazor.Shared.QuestionComponent
{
    public partial class SelectQuestionEditForm
    {
        [Parameter]
        public List<CreateEditQuestionFormModel>? QuestionForms { get; set; }

        [Parameter]
        public EventCallback<List<CreateEditQuestionFormModel>> QuestionFormsChanged { get; set; }

        [Parameter]
        public CreateEditQuestionFormModel? Value { get; set; }

        [Parameter]
        public EventCallback<CreateEditQuestionFormModel> ValueChanged { get; set; } 


        private List<CreateEditQuestionFormModel>? _filteredQuestions;
        private string _searchString = String.Empty;
                    

        protected override async Task OnParametersSetAsync()
        {
            ArgumentNullException.ThrowIfNull(QuestionForms, nameof(QuestionForms));
            _filteredQuestions = QuestionForms;
            SearchQuestion();

            await base.OnParametersSetAsync();
        }

        private void SearchQuestion ()
        {
            if (QuestionForms is not null) 
            {
                _filteredQuestions = QuestionForms.FindAll(o => o.QuestionText.ToLower().Contains(_searchString.ToLower()));
            }
        }

        private async Task SelectQuestionAsync (CreateEditQuestionFormModel questionForm)
        {
            if (questionForm != Value)
            {
                Value = questionForm;
                await ValueChanged.InvokeAsync(Value);
            }
        }

    }
}
