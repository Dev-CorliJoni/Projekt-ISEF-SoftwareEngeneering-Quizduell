using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Quixduell.Blazor.EditFormModel;
using Quixduell.Blazor.Shared.DialogComponent;
using System.Text.Json;

namespace Quixduell.Blazor.Shared.QuestionComponent
{
    public partial class EditQuestion : IDialogBase<CreateEditQuestionFormModel>
    {

        [Parameter]
        public CreateEditQuestionFormModel? Value { get; set; }


        [Parameter]
        public EventCallback<CreateEditQuestionFormModel> ValueChanged { get; set; }

        [Parameter]
        public Action<CreateEditQuestionFormModel> OnSubmit { get; set; }

        [Parameter]
        public Action OnCancel { get; set; }


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


        private void Abort ()
        {
            var original = JsonSerializer.Deserialize<CreateEditQuestionFormModel>(_originalFormModelData!) ?? new CreateEditQuestionFormModel(); 
            Value!.QuestionText = original.QuestionText;
            Value!.Hint = original.Hint;
            Value.AnswerFormModels = original.AnswerFormModels;
            OnCancel.Invoke();
        }

        private void ComplexValidate (EditContext editContext)
        {
            ValidationMessageStore = new ValidationMessageStore(editContext);

            if (editContext.Model is CreateEditQuestionFormModel formModel) 
            {
                bool isValid = true;
                ValidationMessageStore.Clear();

                if (formModel.AnswerFormModels.Count() < 1)
                {
                    ValidationMessageStore.Add(() => formModel.AnswerFormModels, "Es muss mindestens eine Antwort hinzugefügt werden!");
                    isValid = false;
                }

                if (formModel.QuestionType == QuestionType.MultipleChoice)
                {
                    if (formModel.AnswerFormModels.Where(o => o.IsTrue).Count() > 1)
                    {
                        ValidationMessageStore.Add(() => formModel.AnswerFormModels, "Es kann maximal eine Antwort richtig sein!");
                        isValid = false;
                    }
                    if (formModel.AnswerFormModels.Where(o => o.IsTrue).Count() < 1)
                    {
                        ValidationMessageStore.Add(() => formModel.AnswerFormModels, "Es muss mindestens eine richtige Antwort hinzugefügt worden sein!");
                        isValid = false;
                    }
                }
                else if (formModel.QuestionType == QuestionType.OpenText)
                {
                    if (formModel.AnswerFormModels.Count() > 1)
                    {
                        ValidationMessageStore.Add(() => formModel.AnswerFormModels, "Bei offenen Fragen kann es maximal eine Antwort geben!");
                        isValid = false;
                    }
                }

                if (isValid) 
                {
                    OnSubmit.Invoke(Value);
                }
            }


        }

    }
}
