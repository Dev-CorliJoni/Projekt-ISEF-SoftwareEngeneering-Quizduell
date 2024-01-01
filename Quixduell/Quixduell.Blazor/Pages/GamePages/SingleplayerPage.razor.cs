using Quixduell.ServiceLayer.DataAccessLayer.Model.Game;
using Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Quixduell.Blazor.Services;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.Blazor.Helpers;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.Blazor.Pages.GamePages
{
    public partial class SingleplayerPage
    {
        [Parameter]
        public string LobbyGuidParameter { get; set; } = string.Empty;

        [Parameter]
        [SupplyParameterFromQuery(Name = "studyset")]
        public string StudysetGuidParameter { get; set; } = string.Empty;

        [Inject]
        public GameManager GameManager { get; set; } = default!;
        [Inject]
        public UserService UserService { get; set; } = default!;
        [Inject]
        public UserManager<User> UserManager { get; set; } = default!;

        [Inject]
        public StudysetHandler StudysetHandler { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        private SinglePlayer? Game { get; set; }
        private BaseQuestion? SelectedQuestion { get; set; }

        private bool _allowNext = false;

        private bool _gameFinished = false;


        protected override async Task OnParametersSetAsync()
        {
            var user = await UserService.GetAuthenticatedUserOrRedirect(UserManager);
            if (user is null) { return; }

            if (Guid.TryParse(LobbyGuidParameter, out var parsedGameGuid))
            {
                var loadedGame = GameManager.GetGameByID(parsedGameGuid);
                if (loadedGame is SinglePlayer singlePlayer)
                {
                    Game = singlePlayer;
                    if (SelectedQuestion  is null) 
                    {
                        Next();
                    }
                }
            }

            if (Game is null)
            {
                if (Guid.TryParse(StudysetGuidParameter, out var parsedStudysetGuid))
                {
                    var studyset = await StudysetHandler.GetStudysetViaIdAsync(parsedStudysetGuid);
                    if (studyset is not null)
                    {
                        Game = GameManager.CreateSinglePlayerGame(user, studyset);
                        NavigationManager.NavigateTo($"{PageUri.SingeplayerPage}/{Game.Id}");
                    }

                }
            }

        }

        private void Next ()
        {
            _allowNext = false;
            var question = Game!.LoadNextQuestion();

            if (question is null)
            {
                _gameFinished = true;
                //TODO Redirect to  Result Page ?
            }

            SelectedQuestion = question;
        }

        private void OnQuestionAnswered (AnsweredQuestion answeredQuestion) 
        {
            Game!.ReportAnsweredQuestion(answeredQuestion);
            _allowNext = true;
        }


    }
}
