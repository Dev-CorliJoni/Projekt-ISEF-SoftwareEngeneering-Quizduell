using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Quixduell.Blazor.Helpers;
using Quixduell.Blazor.Services;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Game.AnsweredQuestion;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Game;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality;
using Quixduell.Blazor.Shared;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Quixduell.Blazor.Pages.GamePages
{
    public partial class MultiplayerPage
    {
        [Parameter]
        public string LobbyGuidParameter { get; set; } = string.Empty;

        [Parameter]
        [SupplyParameterFromQuery(Name = "studyset")]
        public string StudysetGuidParameter { get; set; } = string.Empty;

        [CascadingParameter]
        public MainLayout Layout { get; set; }

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

        private User? CurrentPlayer { get; set; }
        private Multiplayer? Game { get; set; }
        private BaseQuestion? SelectedQuestion { get; set; }

        private bool _gameFinished = false;


        protected override async Task OnParametersSetAsync()
        {
            if (CurrentPlayer is null)
                return;

            if (Guid.TryParse(LobbyGuidParameter, out var parsedGameGuid))
            {
                var loadedGame = GameManager.GetGameByID(parsedGameGuid);
                if (loadedGame is Multiplayer multiplayer)
                {
                    Game = multiplayer;

                    if (multiplayer.IsGameStarted())
                    {
                        _gameFinished = loadedGame.IsGameFinished();
                        if (multiplayer.Players.FirstOrDefault(o => o.Player.Id == CurrentPlayer.Id) == null)
                        {
                            Layout.Alert.AddAlert("Sorry Game is already running", TimeSpan.FromSeconds(30), Shared.AlertComponent.AlertMessageType.Error);
                            return;
                        }

                    }
                    else
                    {
                        Game.AddPlayer(CurrentPlayer);
                    }

                    SubscripeGameEvents();

                }
            }

            if (Game is null)
            {
                if (Guid.TryParse(StudysetGuidParameter, out var parsedStudysetGuid))
                {
                    var studyset = await StudysetHandler.GetStudysetViaIdAsync(parsedStudysetGuid);
                    if (studyset is not null)
                    {
                        Game = GameManager.CreateMultiPlayerGame(CurrentPlayer, studyset);
                        NavigationManager.NavigateTo($"{PageUri.MultiplayerPage}/{Game.Id}");
                    }

                }
            }

        }

        private void SubscripeGameEvents ()
        {
            if (Game is null) return;
            Game.OnUserJoined += Game_OnUserJoined;
            Game.OnGameStarted += Game_OnGameStarted;
            Game.OnGameEnd += Game_OnGameEnd;
        }

        private void Game_OnGameEnd(object? sender, EventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }

        private void Game_OnGameStarted(object? sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                await Next();
                await InvokeAsync(StateHasChanged);
            });
        }

        private void Game_OnUserJoined(object? sender, EventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }

        protected override async Task OnInitializedAsync()
        {
            CurrentPlayer = await UserService.GetAuthenticatedUserOrRedirect(UserManager);
        }

        private async Task Next()
        {
            var question = Game!.LoadNextQuestion(CurrentPlayer);

            if (question is null)
            {
                _gameFinished = true;
                SelectedQuestion = null;
                var gameIsOver = await GameManager.EndMultiplayerGameAsync(Game, CurrentPlayer);

                if (!gameIsOver)
                    Layout.Alert.AddAlert("Du bist fertig, warte auf die andere Spieler", TimeSpan.FromMinutes(1), Shared.AlertComponent.AlertMessageType.Success);
                return;
            }

            SelectedQuestion = question;
        }

        private async Task OnMultiQuestionAnswered(AnsweredMultiQuestion answeredQuestion)
        {
            Game!.ReportMultiQuestion(answeredQuestion);
            await Next();
        }
        private async Task OnOpenQuestionAnswered(AnsweredOpenQuestion answeredQuestion)
        {
            Game!.ReportOpenAnsweredQuestion(answeredQuestion);
            await Next();
        }

        private void OnStartGame ()
        {
            Game!.StartGame();
        }

    }
}
