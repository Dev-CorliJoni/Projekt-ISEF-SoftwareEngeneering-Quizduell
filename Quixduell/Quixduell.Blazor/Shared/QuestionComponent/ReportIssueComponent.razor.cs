using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Quixduell.Blazor.EditFormModel;
using Quixduell.Blazor.Services;
using Quixduell.ServiceLayer;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;


namespace Quixduell.Blazor.Shared.QuestionComponent
{
    public partial class ReportIssueComponent
    {
        [Parameter]
        public BaseQuestion CurrentQuestion { get; set; } = default!;

        [Parameter]
        public Studyset Studyset { get; set; } = default!;


        [Inject]
        private UserService UserService { get; set; } = default!;       

        [Inject]
        private UserManager<User> UserManager { get; set; } = default!;

        [Inject]
        private IssueManager IssueManager { get; set; } = default!;

        [CascadingParameter]
        public MainLayout Layout { get; set; } = default!;



        private User? _currentUser;
        protected async override Task OnInitializedAsync()
        {

            _currentUser = await UserService.GetAuthenticatedUserOrRedirect(UserManager);
            if (_currentUser is null)
                return;

           await base.OnInitializedAsync();
        }

        private void ReportIssue()
        {
            Layout.Dialog.ShowDialog<ReportIssueDialogComponent, CreateIssueFormModel>("Fehler melden", new ReportIssueDialogComponent(), new CreateIssueFormModel(CurrentQuestion!), async ( value) => {
                await IssueManager.ReportIssueAsync(Studyset, value.ReportedQuestion, _currentUser!, value.Issue);
                Layout.Alert.AddAlert("Fehler wurde an die Ersteller gesendet");
            }, () => {
            });
        }
    }
}
