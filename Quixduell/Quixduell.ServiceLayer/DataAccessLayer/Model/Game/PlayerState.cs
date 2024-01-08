namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Game
{
    public class PlayerState
    {
        public User Player { get; set; }
        public bool IsFinished { get; set; } = false;

        public PlayerState(User player)
        {
            Player = player;
        }

    }
}
