using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Game
{
    public class SinglePlayer : Game
    {
        public User Player { get; set; }

        public SinglePlayer(User player, Studyset studyset) : base(studyset)
        {
            Player = player;
        }

        public BaseQuestion? LoadNextQuestion()
        {
            return base.LoadNextQuestion(Player);
        }
    }
}
