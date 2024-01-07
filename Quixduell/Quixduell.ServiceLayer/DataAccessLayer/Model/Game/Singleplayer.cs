using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Game
{
    public class SinglePlayer : Game
    {
        public PlayerState PlayerState { get; set; }

        public SinglePlayer(User player, Studyset studyset) : base(studyset)
        {
            PlayerState = new PlayerState(player);
        }

        public BaseQuestion? LoadNextQuestion()
        {
            return base.LoadNextQuestion(PlayerState.Player);
        }
    }
}
