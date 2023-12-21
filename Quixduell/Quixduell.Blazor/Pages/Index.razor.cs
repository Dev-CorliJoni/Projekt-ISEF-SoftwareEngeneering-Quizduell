using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.ServiceLayer;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using Microsoft.AspNetCore.Components;
using Quixduell.Blazor.Services;
using System;
using Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace Quixduell.Blazor.Pages
{
    public partial class Index
    {

        [Inject]
        private InitSampleData InitSampleData { get; set; } = default!;

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

        private async Task InitSampleDataMethod()
        {
            var cat = await CategoryHandler.AddCategoryAsync(GenerateRandomString(10));
            var studySet = await StudysetHandler.AddStudysetAsync(GenerateRandomString(10), cat, User);

            await NoticeStudyset(studySet);

            await SearchForStudysets();

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
