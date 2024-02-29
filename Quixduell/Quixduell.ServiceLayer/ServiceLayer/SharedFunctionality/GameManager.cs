using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Game;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;

namespace Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality
{
    /// <summary>
    /// Represents a class for managing game operations.
    /// </summary>
    public class GameManager
    {
        private readonly StudysetDataAccess _studysetDataAccess;

        private static List<Game> Games { get; set; } = new List<Game>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GameManager"/> class.
        /// </summary>
        /// <param name="studysetDataAccess">The data access object for study sets.</param>
        public GameManager(StudysetDataAccess studysetDataAccess)
        {
            _studysetDataAccess = studysetDataAccess;
        }

        /// <summary>
        /// Retrieves a game by its ID.
        /// </summary>
        /// <param name="id">The ID of the game to retrieve.</param>
        /// <returns>The game with the specified ID, if found; otherwise, null.</returns>
        public Game? GetGameByID(Guid id)
        {
            return Games.FirstOrDefault(o => o.Id == id);
        }

        /// <summary>
        /// Creates a single player game.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="studyset">The study set.</param>
        /// <returns>The created single player game.</returns>
        public SinglePlayer CreateSinglePlayerGame(User player, Studyset studyset)
        {
            var game = new SinglePlayer(player, studyset);
            game.GameState = GameState.Started;
            Games.Add(game);
            return game;
        }

        /// <summary>
        /// Creates a multiplayer game.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="studyset">The study set.</param>
        /// <returns>The created multiplayer game.</returns>
        public Multiplayer CreateMultiPlayerGame(User player, Studyset studyset)
        {
            var game = new Multiplayer(studyset);
            game.Players.Add(new PlayerState(player));
            game.GameState = GameState.Created;
            Games.Add(game);
            return game;
        }

        /// <summary>
        /// Ends the Game, calculate the Highscore for the Player and Persist Change 
        /// </summary>
        /// <param name="singlePlayer">The single player game to end.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task EndSinglePlayerGameAsync(SinglePlayer singlePlayer)
        {
            singlePlayer.PlayerState.IsFinished = true;
            singlePlayer.GameFinished();

            await UpdateUserConnection(singlePlayer);
            Games.Remove(singlePlayer);
        }

        /// <summary>
        /// Ends the Game for the current User, check if all Users Finished and update Highscore
        /// </summary>
        /// <param name="multiplayer">The multiplayer game to end.</param>
        /// <param name="currentPlayer">The current player.</param>
        /// <returns>True if the game is finished for all users; otherwise, false.</returns>
        public async Task<bool> EndMultiplayerGameAsync(Multiplayer multiplayer, User currentPlayer)
        {
            multiplayer.PlayerFinished(currentPlayer);

            if (multiplayer.IsGameFinished())
            {
                if (multiplayer.GameResult is not null)
                {
                    await UpdateUserConnections(multiplayer.Players.Select(o => o.Player).ToList(), multiplayer.GameResult, multiplayer.Studyset);
                    return true;
                }
            }

            return false;
        }

        private async Task UpdateUserConnection(SinglePlayer singlePlayer)
        {
            var connection = singlePlayer.Studyset.Connections.FirstOrDefault(c => c.User == singlePlayer.PlayerState.Player);
            if (connection is null)
            {
                singlePlayer.Studyset.Connections.Add(
                    new UserStudysetConnection(singlePlayer.PlayerState.Player, singlePlayer.Studyset, false, new Rating(), GetHighscore(singlePlayer.GameResult!, singlePlayer.PlayerState.Player)));
                await _studysetDataAccess.UpdateAsync(singlePlayer.Studyset);
            }
            else
            {
                var newHighscore = GetHighscore(singlePlayer.GameResult!, singlePlayer.PlayerState.Player);
                if (newHighscore > connection.Highscore)
                {
                    connection.Highscore = newHighscore;
                    await _studysetDataAccess.UpdateAsync(singlePlayer.Studyset);
                }
            }
        }

        private async Task UpdateUserConnections(List<User> players, GameResult gameResult, Studyset studyset)
        {
            var newStudyset = await _studysetDataAccess.GetAsync(studyset.Id);
            foreach (var currentPlayer in players)
            {
                var connection = newStudyset.Connections.FirstOrDefault(o => o.User.Id == currentPlayer.Id);
                if (connection is null)
                {
                    studyset.Connections.Add(
                        new UserStudysetConnection(currentPlayer, newStudyset, false, new Rating(), GetHighscore(gameResult, currentPlayer)));
                }
                else
                {
                    var newHighscore = GetHighscore(gameResult, currentPlayer);
                    if (newHighscore > connection.Highscore)
                    {
                        connection.Highscore = newHighscore;
                    }
                }
            }
            await _studysetDataAccess.UpdateAsync(newStudyset);
        }

        /// <summary>
        /// Creates a multiplayer game without a player.
        /// </summary>
        /// <param name="studyset">The study set.</param>
        /// <returns>The created multiplayer game.</returns>
        public Multiplayer CreateMultiplayerGame(User player, Studyset studyset)
        {
            var game = new Multiplayer(studyset);
            Games.Add(game);
            return game;
        }

        /// <summary>
        /// Calculates the high score for a specific user in a game.
        /// </summary>
        /// <param name="gameResult">The game result.</param>
        /// <param name="player">The player.</param>
        /// <returns>The current user's high score.</returns>
        internal float GetHighscore(GameResult gameResult, User player)
        {
            int score = 0;
            foreach (var result in gameResult.AnsweredQuestionResults.Where(o => o.Player.Id == player.Id))
            {
                if (result.RightAnswer)
                {
                    score++;
                }
            }
            return score;
        }
    }
}
