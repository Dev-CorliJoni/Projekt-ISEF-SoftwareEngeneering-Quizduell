using Microsoft.AspNetCore.Components;
using Quixduell.Blazor.EditFormModel;

namespace Quixduell.Blazor.Shared.QuestionComponent
{
    public partial class EditQuestion
    {


        [Parameter]
        public CreateEditQuestionFormModel? Value { get; set; }

        [Parameter]
        public EventCallback<CreateEditQuestionFormModel> ValueChanged { get; set; }

        private CreateEditAnswerFormModel? _selectedAnswer;


        protected override void OnParametersSet()
        {
            if (Value is null) 
            {
                throw new NotImplementedException();
            }
            base.OnParametersSet();
        }

        private void AddQuestion ()
        {
            var answer = new CreateEditAnswerFormModel();
            Value?.AnswerFormModels.Add(answer);
            _selectedAnswer = answer;

        }
        private async Task ValidSubmitAsync ()
        {
            await ValueChanged.InvokeAsync(Value);

        }

        private async Task OnAbort()
        {
            Value = null;
            await ValueChanged.InvokeAsync(Value);
        }



    }
}
