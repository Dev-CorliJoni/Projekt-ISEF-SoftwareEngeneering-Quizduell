using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.ServiceLayer;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using Microsoft.AspNetCore.Components;
using Quixduell.Blazor.Services;
using System;
using Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Microsoft.AspNetCore.Identity;

namespace Quixduell.Blazor.Pages
{
    public partial class StudysetView
    {
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


        private Studyset Studyset { get; set; } = default!;
        private User User { get; set; } = default!;


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var user = await UserService.GetAuthenticatedUserOrRedirect(UserManager);
            if (user is null) { return; }

            User = user;
            SelectStudyset();
            Initialize();
        }

        private void Initialize()
        {
        }

        private void SelectStudyset()
        {
            if (User.StudysetConnections.Any())
            {
                Studyset = User.StudysetConnections[0].Studyset;
            }
        }

    }
}
