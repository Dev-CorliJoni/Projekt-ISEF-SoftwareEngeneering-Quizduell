using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Quixduell.Blazor.EditFormModel;
using Quixduell.Blazor.Services;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality;

namespace Quixduell.Blazor.Pages.StudysetPages
{
    public partial class CreateEditStudyset
    {


        [Parameter]
        public Guid StudysetID { get; set; } = default!;

        [Inject]
        private UserService UserService { get; set; } = default!;
        [Inject]
        private StudysetHandler StudysetHandler { get; set; } = default!;


        public CreateEditStudyset()
        {

        }

        private CreateEditStudysetFormModel? FormModel { get; set; }
        private EditContext EditContext { get; set; } = default!;
        private ValidationMessageStore ValidationMessage { get; set; } = default!;

        private CreateEditQuestionFormModel? _currentQuestion;

        protected override async Task OnParametersSetAsync()
        {
            var user = await UserService.GetAuthenticatedUserOrRedirect();
            if (user is null) { return; }



            Studyset? getStudyset = null;
            if (StudysetID == Guid.Empty)
            {
                FormModel = new CreateEditStudysetFormModel(user);
            }

            getStudyset = await StudysetHandler.GetStudysetViaIdAsync(StudysetID);
            if (getStudyset is null)
            {
                FormModel = new CreateEditStudysetFormModel(user);
            }
            else
            {
                FormModel = new CreateEditStudysetFormModel(getStudyset);
            }




            EditContext = new EditContext(FormModel!);
            ValidationMessage = new ValidationMessageStore(EditContext);
            await base.OnParametersSetAsync();
        }

        private void AddQuestion ()
        {
            FormModel.QuestionFormModels.Add(new CreateEditQuestionFormModel());
        }


        private void ComplexValidate(EditContext editContext)
        {
            var model = (editContext.Model as Studyset);
            if (model is not null)
            {
                ValidationMessage.Clear();

                foreach (var question in model.Questions)
                {
                    if (question is MultipleChoiceQuestion multipleChoiseQuestion)
                    {
                        if (multipleChoiseQuestion.Answers.Count() < 1)
                        {
                            ValidationMessage.Add(() => question, $"One Answer for {multipleChoiseQuestion.Text} must be present");
                        }
                        if (!multipleChoiseQuestion.Answers.Any(o => o.IsTrue))
                        {
                            ValidationMessage.Add(() => question, $"One Answer from {multipleChoiseQuestion.Text} has to be true");
                        }
                    }
                    else if (question is OpenQuestion openQuestion)
                    {

                    }
                }
            }

        }


    }
}
