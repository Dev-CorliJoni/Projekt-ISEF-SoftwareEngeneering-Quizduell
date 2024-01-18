using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Microsoft.AspNetCore.Components;
using Quixduell.Blazor.Services;
using Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality;
using Microsoft.AspNetCore.Identity;

namespace Quixduell.Blazor.Pages
{
    public partial class StudysetView
    {

        [Parameter]
        public string? StudysetID { get; set; }

        [Inject]
        private UserService UserService { get; set; } = default!;

        [Inject]
        private UserManager<User> UserManager { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        private StudysetHandler StudysetHandler { get; set; } = default!;

        [Inject]
        private CategoryHandler CategoryHandler { get; set; } = default!;


        private Studyset? Studyset { get; set; } = default!;
        private User User { get; set; } = default!;


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var user = await UserService.GetAuthenticatedUserOrRedirect(UserManager);
            if (user is null) { return; }

            User = user;

        }

        protected override async Task OnParametersSetAsync()
        {
            if (Guid.TryParse(StudysetID, out var ParsedStudysetID))
            {
                Studyset = await StudysetHandler.GetStudysetViaIdAsync(ParsedStudysetID);
                await StudysetHandler.CreateConnection(Studyset, User);
            }
        }
    }
}
