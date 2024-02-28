using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.ServiceLayer;
using Microsoft.AspNetCore.Components;
using Quixduell.Blazor.Services;
using Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality;
using Microsoft.AspNetCore.Identity;
using Quixduell.Blazor.Shared;

namespace Quixduell.Blazor.Pages
{
    public partial class Index
    {
        [Inject]
        private GlobalSearch GlobalSearch { get; set; } = default!;

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

        [CascadingParameter]
        public MainLayout Layout { get; set; }



        private List<Studyset>? _studysets = null;
        private List<Category>? _categories = null;

        private string SearchText { get; set; } = "";
        private Category? SelectedCategory { get; set; }
        private bool ShowConnected { get; set; } = true;

        private User User { get; set; } = default!;



        override protected async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var user = await UserService.GetAuthenticatedUserOrRedirect(UserManager);
            if (user is null) { return; }

            User = user;

            //await InitSampleData.GenerateSampleData(user);

            _categories = await CategoryHandler.SearchCategoryAsync("");
            await SearchForStudysets();
        }

        private bool CheckIfCurrentUserStored (Studyset studyset) 
        {
            var connection = studyset.Connections.FirstOrDefault(con => con.User == User);
            if (connection is not null && connection.IsStored)
            {
                return true;
            }
            return false;
        }

        private bool CheckEditRights (Studyset studyset)
        {
            if (studyset.Creator.Id == User.Id || studyset.Contributors.Any(o => o.Id == User.Id))
            {
                return true;
            }
            return false;
        }

        private async Task NoticeStudyset (Studyset studySet)
        {
            await GlobalSearch.NoticeStudyset(studySet, User);
        }

        private async Task UnNoticeStudyset(Studyset studySet)
        {
            await GlobalSearch.UnNoticeStudyset(studySet, User);
        }

        private async Task SearchForStudysets ()
        {
            if (ShowConnected)
            {
                _studysets = await GlobalSearch.Search(SearchText, User, SelectedCategory?.Name??"");
            }
            else
            {
                _studysets = await GlobalSearch.Search(SearchText, null, SelectedCategory?.Name ?? "");
            }
        }
    }
}
