using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using System;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Game
{
    public class Multiplayer : Game
    {
        public event EventHandler OnUserJoined;
        public event EventHandler OnGameStarted;
        public event EventHandler OnGameEnd;


        private List<PlayerState> _players = new List<PlayerState>();

        public List<PlayerState> Players { get => _players; }
        public Multiplayer(Studyset studyset) : base(studyset)
        {

        }

        public BaseQuestion? LoadNextQuestion(User currentPlayer)
        {
            return base.LoadNextQuestion(currentPlayer);
        }

        public void AddPlayer(User user)
        {
            ThrowIfStarted();

            if (_players.FirstOrDefault(o => o.Player.Id == user.Id) == null) 
            {
                _players.Add(new PlayerState(user));
                OnUserJoined?.Invoke(this, EventArgs.Empty);
            }

        }

        public void StartGame()
        {
            ThrowIfStarted();
            GameState = GameState.Started;
            OnGameStarted?.Invoke(this, EventArgs.Empty);
        }

        public override void GameFinished()
        {
            base.GameFinished();
            OnGameEnd?.Invoke(this, EventArgs.Empty);
        }

        internal void PlayerFinished(User currentPlayer)
        {
            Players.First(o => o.Player.Id == currentPlayer.Id).IsFinished = true;

            if (Players.All(o => o.IsFinished))
            {
                GameFinished();
            }
        }
    }
}
