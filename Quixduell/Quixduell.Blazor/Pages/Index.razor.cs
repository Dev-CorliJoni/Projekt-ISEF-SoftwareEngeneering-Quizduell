using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.ServiceLayer;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using Microsoft.AspNetCore.Components;
using Quixduell.Blazor.Services;
using System;

namespace Quixduell.Blazor.Pages
{
    public partial class Index
    {
        [Inject]
        private GlobalSearch GlobalSearch { get; set; } = default!;

        [Inject]
        private UserService UserService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;




        private List<Studyset>? _studysets = null;
        private List<Category>? _categories = null;

        private string SearchText { get; set; } = "";
        private string SelectedCategoryName { get; set; } = "";
        private DateTime MinDate { get; set; } = DateTime.Now;




        override protected async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            _categories = await GlobalSearch.SearchCategory("");
            await SearchForStudysets();
        }


        private async Task InitSampleData()
        {

            var currentUser = await UserService.GetAuthenticatedUserOrRedirect();
            if (currentUser is null) { return; }

            var Studyset = new Studyset(GenerateRandomString(10), new Category(GenerateRandomString(10)), currentUser, new List<User>(), new List<BaseQuestion>());

            await GlobalSearch.StoreStudyset(Studyset, currentUser);


            await SearchForStudysets();

        }

        private async Task SearchForStudysets ()
        {
            _studysets = await GlobalSearch.Search(SearchText,null,SelectedCategoryName);
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
