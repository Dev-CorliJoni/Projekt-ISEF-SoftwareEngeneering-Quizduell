using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.Pages
{
    public partial class Index
    {
        [CascadingParameter]
        internal Task<AuthenticationState>? AuthenticationState { get; set; }

        internal List<Studyset>? _lernsets = null;

        internal string _errorMessage = "";

        override protected async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();


            //Lernsets = new List<Studyset>(await LernsetRepository.GetStudysetsAsync());
        }


        private async Task InitSampleData()
        {
            var user = (await AuthenticationState).User;

            if (user.Identity.IsAuthenticated)
            {
                var currentUser = await UserManager.GetUserAsync(user);


                //var category = new Category();
                //category.Name = "Test Cat";

                //var Lernset = new Studyset(currentUser, category);

                //await LernsetRepository.CreateStudysetAsync(Lernset);
                //Lernsets = new List<Studyset>(await LernsetRepository.GetStudysetsAsync());
            }
            else
            {
                _errorMessage = "Not Logged In";
            }
        }
    }
}
