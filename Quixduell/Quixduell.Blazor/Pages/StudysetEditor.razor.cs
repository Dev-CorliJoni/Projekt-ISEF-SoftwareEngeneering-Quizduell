using Microsoft.AspNetCore.Components;
using Quixduell.Blazor.Services;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.ServiceLayer;

namespace Quixduell.Blazor.Pages
{
    public partial class StudysetEditor
    {
        [Inject]
        private GlobalSearch GlobalSearch { get; set; }

        [Inject]
        private UserService UserService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private List<Studyset>? _studysets;

        protected override async Task OnInitializedAsync()
        {
            var currentUser = await UserService.GetAuthenticatedUserOrRedirect();
            if (currentUser is null) { return; }

            _studysets = await GlobalSearch.Search(null, currentUser);


            await base.OnInitializedAsync();
        }



    }
}
