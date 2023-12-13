using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.ServiceLayer;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using Microsoft.AspNetCore.Components;
using Quixduell.Blazor.Services;

namespace Quixduell.Blazor.Pages
{
    public partial class Index
    {
        [Inject]
        private GlobalSearch GlobalSearch { get; set; }

        [Inject]
        private UserService UserService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }




        private List<Studyset>? _studysets = null;




        override protected async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();


            _studysets = await GlobalSearch.Search("");
        }


        private async Task InitSampleData()
        {

            var currentUser = await UserService.GetAuthenticatedUserOrRedirect();
            if (currentUser is null) { return; }

            var Studyset = new Studyset(GenerateRandomString(10), new Category(GenerateRandomString(10)), currentUser, new List<User>(), new List<BaseQuestion>());

            await GlobalSearch.StoreStudyset(Studyset, currentUser);


            _studysets = await GlobalSearch.Search("");

        }


        static Random _random = new Random();
        static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; // Zeichen, die im zufälligen String verwendet werden sollen
            char[] stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[_random.Next(chars.Length)];
            }

            return new string(stringChars);
        }


    }
}
