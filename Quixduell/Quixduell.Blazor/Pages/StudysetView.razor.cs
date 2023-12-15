using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.ServiceLayer;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using Microsoft.AspNetCore.Components;
using Quixduell.Blazor.Services;
using System;
using Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Quixduell.Blazor.Pages
{
    public partial class StudysetView
    {

        [Inject]
        private InitSampleData InitSampleData { get; set; } = default!;

        [Inject]
        private GlobalSearch GlobalSearch { get; set; } = default!;

        [Inject]
        private UserService UserService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        private StudysetHandler StudysetHandler { get; set; } = default!;

        [Inject]
        private CategoryHandler CategoryHandler { get; set; } = default!;


        private Studyset _studyset;


        private Studyset Studyset { 
            set {
                _studyset = value;                
                Connection = _studyset.Connections.Find((sc) => sc.User == User)!;
            }
            get
            {
                return _studyset;
            }
        }

        private UserStudysetConnection Connection { get; set; } = default!;
        private User User { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var user = await UserService.GetAuthenticatedUserOrRedirect();
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

        private string GetStarColor()
        {
            return Connection.IsStored ? "yellow" : "lightgray";
        }

        private async Task StarFunction(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            await Task.Run(() =>
            {
                Connection.IsStored = !Connection.IsStored;
            });
        }

        private Task Play(Microsoft.AspNetCore.Components.Web.MouseEventArgs e) 
        {
            throw new NotImplementedException();
        }

        
    }
}
