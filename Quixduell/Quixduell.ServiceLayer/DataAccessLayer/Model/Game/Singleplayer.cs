namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Game
{
    public class SinglePlayer : Game
    {
        public User Player { get; set; }

        public SinglePlayer(User player, Studyset studyset) : base(studyset)
        {
            Player = player;
        }

        public AnsweredQuestion LoadNextQuestion()
        {
            return base.LoadNextQuestion(Player);
        }
    }
}
