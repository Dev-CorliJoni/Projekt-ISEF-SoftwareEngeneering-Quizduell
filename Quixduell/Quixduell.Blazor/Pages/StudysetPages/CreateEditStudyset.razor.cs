using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Quixduell.Blazor.EditFormModel;
using Quixduell.Blazor.Helpers;
using Quixduell.Blazor.Services;
using Quixduell.Blazor.Shared;
using Quixduell.Blazor.Shared.QuestionComponent;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality;

namespace Quixduell.Blazor.Pages.StudysetPages
{
    public partial class CreateEditStudyset
    {


        [Parameter]
        public String StudysetID { get; set; } = default!;

        [CascadingParameter]
        public MainLayout Layout { get; set; }

        [Inject]
        private UserService UserService { get; set; } = default!;

        [Inject]
        private UserManager<User> UserManager { get; set; } = default!;

        [Inject]
        private StudysetHandler StudysetHandler { get; set; } = default!;

        [Inject]
        private CategoryHandler CategoryHandler { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;


        private CreateEditStudysetFormModel? FormModel { get; set; }
        private EditContext EditContext { get; set; } = default!;
        private ValidationMessageStore ValidationMessage { get; set; } = default!;



        protected override async Task OnParametersSetAsync()
        {
            var user = await UserService.GetAuthenticatedUserOrRedirect(UserManager);
            if (user is null) { return; }


            if (Guid.TryParse(StudysetID, out var ID))
            {
                Studyset? getStudyset = null;
                getStudyset = await StudysetHandler.GetStudysetViaIdAsync(ID);
                if (getStudyset is null)
                {
                    FormModel = new CreateEditStudysetFormModel(user);
                }
                else
                {
                    FormModel = new CreateEditStudysetFormModel(getStudyset);
                }
            }
            else
            {
                FormModel = new CreateEditStudysetFormModel(user);
            }




            EditContext = new EditContext(FormModel!);
            ValidationMessage = new ValidationMessageStore(EditContext);
            await base.OnParametersSetAsync();
        }

        private void AddQuestion ()
        {
            var question = new CreateEditQuestionFormModel();
            FormModel?.QuestionFormModels.Add(question);
            ShowQuestionDialog(question);
        }


        private async void ComplexValidate(EditContext editContext)
        {
            var model = (editContext.Model as CreateEditStudysetFormModel);
            if (model is not null)
            {
                bool isValid = true;
                ValidationMessage.Clear();

                if (model.Creator is null)
                {
                    ValidationMessage.Add(() => model.Creator, "Melde dich an");
                    isValid = false;
                }

                if (String.IsNullOrWhiteSpace(model.Name))
                {
                    ValidationMessage.Add(() => model.Name, "Bitte setzte einen Namen");
                    isValid = false;
                }

                if (model.Category is null)
                {
                    ValidationMessage.Add(() => model.Category, "Die Kategorie muss gesetzt werden");
                    isValid = false;
                }

                if (model.QuestionFormModels.Count == 0)
                {
                    ValidationMessage.Add(() => model.QuestionFormModels, "Mindestens eine Frage muss eingetragen werden");
                    isValid = false;
                }

                if (!isValid)
                { return; }




                var questionList = new List<BaseQuestion>();
                foreach (var question in model.QuestionFormModels)
                {
                    questionList.Add(question.ToBaseQuestion());
                }
                if (model.ID != Guid.Empty)
                {
                    await StudysetHandler.UpdateStudysetAsync(model.ID,model.Name, model.Category, model.Creator!, model.Contributors, questionList, model.UserStudysetConnections);
                    NavigationManager.NavigateTo(PageUri.Index, true);
                    return;
                }
                await StudysetHandler.AddStudysetAsync(model.Name, model.Category, model.Creator!,model.Contributors, questionList, model.UserStudysetConnections);

                NavigationManager.NavigateTo(PageUri.Index, true);
            }

        }

        private void ShowQuestionDialog (CreateEditQuestionFormModel formModel)
        {
            Layout.Dialog.ShowDialog<EditQuestion, CreateEditQuestionFormModel>("Frage bearbeiten",new EditQuestion(), formModel, (CreateEditQuestionFormModel o) =>
            {

            }, () => 
            {

            });
        }


    }
}
