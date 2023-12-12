using Microsoft.AspNetCore.Components.Authorization;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.ServiceLayer;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Quixduell.Blazor.Helpers;

namespace Quixduell.Blazor.Pages
{
    public partial class Index
    {
        [Inject]
        private GlobalSearch GlobalSearch { get; set; }


        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private UserManager<User> UserManager { get; set; }


        private List<Studyset>? _studysets = null;


        public Index()
        {
                
        }

        override protected async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();


            _studysets = await GlobalSearch.Search("");
        }


        private async Task InitSampleData()
        {
            var user = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;

            if (user.Identity!.IsAuthenticated)
            {
                var currentUser = await UserManager.GetUserAsync(user);
                if (currentUser is null)
                {
                    NavigationManager.NavigateTo(PageUri.LoginPage,true);
                    return;
                }


                var Studyset = new Studyset("Test Study", new Category("Test Cat"), currentUser, new List<User>(), new List<BaseQuestion>());

                await GlobalSearch.SaveStudyset(Studyset, currentUser);


                _studysets = await GlobalSearch.Search("");
            }
            else
            {
                NavigationManager.NavigateTo(PageUri.LoginPage,true);
            }
        }
    }
}
