using Quixduell.ServiceLayer.DataAccessLayer.Model.Game;
using Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Quixduell.Blazor.Services;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.Blazor.Helpers;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Game.AnsweredQuestion;

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

        private User? Player { get; set; }
        private SinglePlayer? Game { get; set; }
        private BaseQuestion? SelectedQuestion { get; set; }

        private bool _gameFinished = false;


        protected override async Task OnParametersSetAsync()
        {
            if (Player is null)
                return;

            if (Guid.TryParse(LobbyGuidParameter, out var parsedGameGuid))
            {
                var loadedGame = GameManager.GetGameByID(parsedGameGuid);
                if (loadedGame is SinglePlayer singlePlayer)
                {
                    Game = singlePlayer;
                    if (SelectedQuestion  is null) 
                    {
                        await Next();
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
                        Game = GameManager.CreateSinglePlayerGame(Player, studyset);
                        NavigationManager.NavigateTo($"{PageUri.SingeplayerPage}/{Game.Id}");
                    }

                }
            }

        }

        protected override async Task OnInitializedAsync()
        {
            Player = await UserService.GetAuthenticatedUserOrRedirect(UserManager);
        }

        private async Task Next ()
        {
            var question = Game!.LoadNextQuestion();

            if (question is null)
            {
                _gameFinished = true;
                await GameManager.EndSinglePlayerGameAsync(Game);
                SelectedQuestion = null;
                return;
            }

            SelectedQuestion = question;
        }

        private async Task OnMultiQuestionAnswered(AnsweredMultiQuestion answeredQuestion) 
        {
            Game.ReportMultiQuestion(answeredQuestion);
            await Next();
        }

        private async Task OnOpenQuestionAnswered(AnsweredOpenQuestion answeredQuestion)
        {
            Game!.ReportOpenAnsweredQuestion(answeredQuestion);
            await Next();
        }


    }
}
