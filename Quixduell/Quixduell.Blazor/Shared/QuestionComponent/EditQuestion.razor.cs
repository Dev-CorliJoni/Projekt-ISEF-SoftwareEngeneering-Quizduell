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
        private CreateEditQuestionFormModel? _originalFormModel = null;


        protected override void OnParametersSet()
        {
            if (Value is null) 
            {
                throw new NotImplementedException();
            }
            base.OnParametersSet();
        }

        private void AddAnswer ()
        {
            var answer = new CreateEditAnswerFormModel();
            Value?.AnswerFormModels.Add(answer);
            _selectedAnswer = answer;

        }

        private void RemoveAnswer(CreateEditAnswerFormModel formModel) 
        {
            Value?.AnswerFormModels.Remove(formModel);
            _selectedAnswer = null;
        }
        private async Task ValidSubmitAsync ()
        {
            await ValueChanged.InvokeAsync(Value);

        }

    }
}
