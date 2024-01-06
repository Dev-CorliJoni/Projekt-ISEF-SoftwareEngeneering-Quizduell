using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Quixduell.Blazor.EditFormModel;
using System.Text.Json;

namespace Quixduell.Blazor.Shared.QuestionComponent
{
    public partial class EditQuestion
    {

        [Parameter]
        public CreateEditQuestionFormModel? Value { get; set; }


        [Parameter]
        public EventCallback<CreateEditQuestionFormModel> ValueChanged { get; set; }

        [Parameter]
        public EventCallback<CreateEditQuestionFormModel> OnValidSubmit { get; set; }

        [Parameter]
        public EventCallback OnAbort { get; set; }

        private CreateEditAnswerFormModel? _selectedAnswer;

        private CreateEditQuestionFormModel? _originalFormModel = null;
        private string _originalFormModelData = "";
        private ValidationMessageStore ValidationMessageStore { get; set; } = default!;

        protected override void OnParametersSet()
        {
            if (Value is null) 
            {
                throw new NotImplementedException();
            }
            if (_originalFormModel != Value)
            {
                _originalFormModel = Value;
                _originalFormModelData = JsonSerializer.Serialize(Value);
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


        private async Task Abort ()
        {
            var original = JsonSerializer.Deserialize<CreateEditQuestionFormModel>(_originalFormModelData!) ?? new CreateEditQuestionFormModel(); 
            Value!.QuestionText = original.QuestionText;
            Value!.Hint = original.Hint;
            Value.AnswerFormModels = original.AnswerFormModels;
            await OnAbort.InvokeAsync();
        }

        private async Task ComplexValidate (EditContext editContext)
        {
            ValidationMessageStore = new ValidationMessageStore(editContext);

            if (editContext.Model is CreateEditQuestionFormModel formModel) 
            {
                bool isValid = true;
                ValidationMessageStore.Clear();

                if (formModel.AnswerFormModels.Count() < 1)
                {
                    ValidationMessageStore.Add(() => formModel.AnswerFormModels, "One Answer has to be set!");
                    isValid = false;
                }

                if (formModel.QuestionType == QuestionType.MultipleChoise)
                {
                    if (formModel.AnswerFormModels.Where(o => o.IsTrue).Count() > 1)
                    {
                        ValidationMessageStore.Add(() => formModel.AnswerFormModels, "Only one Answer can be true");
                        isValid = false;
                    }
                    if (formModel.AnswerFormModels.Where(o => o.IsTrue).Count() < 1)
                    {
                        ValidationMessageStore.Add(() => formModel.AnswerFormModels, "One one Answer has to be true");
                        isValid = false;
                    }
                }
                if (formModel.QuestionType == QuestionType.OpenText)
                {
                    if (formModel.AnswerFormModels.Count() > 1)
                    {
                        ValidationMessageStore.Add(() => formModel.AnswerFormModels, "Only Answer has to be set!");
                        isValid = false;
                    }
                }

                    if (isValid) 
                {
                    await OnValidSubmit.InvokeAsync(Value);
                }
            }


        }

    }
}
