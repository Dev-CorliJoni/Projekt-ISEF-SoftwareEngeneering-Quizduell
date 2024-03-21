using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using Quixduell.Blazor.Helpers;
using Quixduell.Blazor.Services;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.Pages
{
    public partial class ContributorRequest
    {
        [Inject]
        private ServiceLayer.ServiceLayer.SharedFunctionality.StudysetHandler StudysetHandler { get; set; } = default!;

        [Inject]
        private ServiceLayer.ServiceLayer.ContributorRequest ContributorRequestService { get; set; } = default!;

        [Inject]
        private UserService UserService { get; set; } = default!;

        [Inject]
        private UserManager<User> UserManager { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;


        [SupplyParameterFromQuery(Name = "user")]
        public string UserGUID { get; set; } = string.Empty;

        [SupplyParameterFromQuery(Name = "studyset")]
        public string StudysetGuid { get; set; } = string.Empty;

        public User User { get; set; }
        public User RequestedUser { get; set; }
        public Studyset Studyset { get; set; }

        public bool Answered { get; set; } = false;

        protected override async Task OnParametersSetAsync()
        {                        
            if (Guid.TryParse(StudysetGuid, out var parsedStudysetGuid))
            {
                Studyset = await StudysetHandler.GetStudysetViaIdAsync(parsedStudysetGuid);
            }

            RequestedUser = await UserService.GetUserViaIdAsync(UserManager, UserGUID);

            var redirect = false;

            if (Studyset == null || RequestedUser == null)
            {
                redirect = true;
            }
            else
            if (User != null && (Studyset.Creator == User || Studyset.Contributors.Contains(User)) == false)
            {
                redirect = true;
            }
            
            if(redirect)
            {
                NavigationManager.NavigateTo($"{PageUri.Index}");
            }

            await base.OnParametersSetAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            User = await UserService.GetAuthenticatedUser(UserManager);
            if (User is null)
            {
                
                NavigationManager.NavigateTo($"Identity/Account/Login?returnUrl=/{Uri.EscapeDataString(NavigationManager.Uri.Replace(NavigationManager.BaseUri, ""))}");
            }
        }

        private async Task RejectContributorRequest(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            await ContributorRequestService.RejectContributor(Studyset, RequestedUser);
            Answered = true;
        }

        private async Task AddContributor(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            await ContributorRequestService.AddContributor(Studyset, RequestedUser);
            Answered = true;
        }
    }
}
