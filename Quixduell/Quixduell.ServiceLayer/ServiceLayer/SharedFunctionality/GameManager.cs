using Microsoft.IdentityModel.Tokens;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Answers;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Game;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;
using System.Transactions;

namespace Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality
{
    public class GameManager
    {

        private readonly StudysetDataAccess _studysetDataAccess;

        public GameManager(StudysetDataAccess studysetDataAccess)
        {
            _studysetDataAccess = studysetDataAccess;
        }

        private static List<Game> Games { get; set; } = new List<Game>();

        public Game? GetGameByID (Guid id)
        {
            return Games.FirstOrDefault(o => o.Id == id);
        }

        public SinglePlayer CreateSinglePlayerGame (User player, Studyset studyset)
        {
            var game = new SinglePlayer(player,studyset);
            game.GameState = GameState.Started;
            Games.Add(game);
            return game;
        }

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
        /// <param name="singlePlayer"></param>
        /// <returns></returns>
        public async Task EndSinglePlayerGameAsync(SinglePlayer singlePlayer)
        {
            singlePlayer.PlayerState.IsFinished = true;
            singlePlayer.GameFinished();

            //TODO Persist Game ??
            await UpdateUserConnection(singlePlayer);


            Games.Remove(singlePlayer);

        }

        /// <summary>
        /// Ends the Game, calculate the Highscore for the Player and Persist Change 
        /// </summary>
        /// <param name="singlePlayer"></param>
        /// <returns></returns>
        public async Task EndMultiplayerGameAsync(Multiplayer multiplayer, User currentPlayer)
        {
            multiplayer.Players.First(o => o.Player.Id == currentPlayer.Id).IsFinished = true;
            //TODO Persist Game ??
            await UpdateUserConnection(multiplayer, currentPlayer);

            if (multiplayer.Players.All(o => o.IsFinished))
            {
                multiplayer.GameFinished();

            }

            Games.Remove(multiplayer);

        }

        private async Task UpdateUserConnection(SinglePlayer singlePlayer)
        {
            var connection = singlePlayer.Studyset.Connections.Where(s => s.User.Id == singlePlayer.PlayerState.Player.Id).FirstOrDefault();
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

        private async Task UpdateUserConnection(Multiplayer multiplayer, User currentPlayer)
        {
            var connection = multiplayer.Studyset.Connections.Where(s => s.User.Id == currentPlayer.Id).FirstOrDefault();
            if (connection is null)
            {
                multiplayer.Studyset.Connections.Add(
                    new UserStudysetConnection(currentPlayer, multiplayer.Studyset, false, new Rating(), GetHighscore(multiplayer.GameResult!, currentPlayer)));
                await _studysetDataAccess.UpdateAsync(multiplayer.Studyset);
            }
            else
            {
                var newHighscore = GetHighscore(multiplayer.GameResult!, currentPlayer);
                if (newHighscore > connection.Highscore)
                {
                    connection.Highscore = newHighscore;
                    await _studysetDataAccess.UpdateAsync(multiplayer.Studyset);
                }
            }
        }

        /// <summary>
        /// Calculate Highscore for specific User in a Game
        /// </summary>
        /// <param name="game"></param>
        /// <param name="player"></param>
        /// <returns>Current User Highscore</returns>
        private float GetHighscore(GameResult gameResult, User player)
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

        public Multiplayer CreateMultiplayerGame(User player, Studyset studyset)
        {
            var game = new Multiplayer(studyset);
            Games.Add(game);
            return game;
        }


    }
}
