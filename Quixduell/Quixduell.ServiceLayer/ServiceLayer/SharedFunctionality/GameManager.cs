using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Game;
using System.Runtime.CompilerServices;

namespace Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality
{
    public class GameManager
    {
        private static List<Game> Games { get; set; } = new List<Game>();

        public Game? GetGameByID (Guid id)
        {
            return Games.FirstOrDefault(o => o.Id == id);
        }

        public SinglePlayer CreateSinglePlayerGame (User player, Studyset studyset)
        {
            var game = new SinglePlayer(player,studyset);
            Games.Add(game);
            return game;
        }
    }
}
