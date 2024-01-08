using Microsoft.EntityFrameworkCore.Metadata;
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
        /// Ends the Game for the current User, check if all Users Finished and update Highscore
        /// </summary>
        /// <param name="multiplayer"></param>
        /// <param name="currentPlayer"></param>
        /// <returns>True if Game is finished for all Users </returns>
        public async Task<bool> EndMultiplayerGameAsync(Multiplayer multiplayer, User currentPlayer)
        {
           

            multiplayer.PlayerFinished(currentPlayer);

            if (multiplayer.IsGameFinished())
            {
                if (multiplayer.GameResult is not null)
                {
                    await UpdateUserConnections(multiplayer.Players.Select(o => o.Player).ToList(), multiplayer.GameResult, multiplayer.Studyset);
                    Games.Remove(multiplayer);
                    return true;
                }
            }

            return false;

        }

        private async Task UpdateUserConnection(SinglePlayer singlePlayer)
        {
            var connection = singlePlayer.Studyset.Connections.Where(s => s.Studyset.Id == singlePlayer.Studyset.Id).FirstOrDefault();
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
            foreach (var currentPlayer in players)
            {
                var connection = currentPlayer.StudysetConnections.FirstOrDefault(o => o.Studyset.Id == studyset.Id);
                if (connection is null)
                {
                    studyset.Connections.Add(
                        new UserStudysetConnection(currentPlayer, studyset, false, new Rating(), GetHighscore(gameResult, currentPlayer)));
                    await _studysetDataAccess.UpdateAsync(studyset);
                }
                else
                {
                    var newHighscore = GetHighscore(gameResult, currentPlayer);
                    if (newHighscore > connection.Highscore)
                    {
                        connection.Highscore = newHighscore;
                        await _studysetDataAccess.UpdateAsync(studyset);
                    }
                }
            }
        }



        public Multiplayer CreateMultiplayerGame(User player, Studyset studyset)
        {
            var game = new Multiplayer(studyset);
            Games.Add(game);
            return game;
        }

        /// <summary>
        /// Calculate Highscore for specific User in a Game
        /// </summary>
        /// <param name="game"></param>
        /// <param name="player"></param>
        /// <returns>Current User Highscore</returns>
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
