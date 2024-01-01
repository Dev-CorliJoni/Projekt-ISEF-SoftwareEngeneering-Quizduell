using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Game
{
    public abstract class Game
    {
        public Guid Id { get; set; }
        internal Studyset Studyset { get; set; }
        internal List<AnsweredQuestion> AnsweredQuestions { get; set; } = new List<AnsweredQuestion>();

        public GameResult? GameResult { get; set; }
        internal GameState GameState { get; set; }
        public Game(Studyset studyset) 
        {
            GameState = GameState.Created;
            Id = Guid.NewGuid();
            Studyset = studyset;
        }


        public BaseQuestion? LoadNextQuestion(User player)
        {
            ThrowIfFinished();
            if (AnsweredQuestions.FirstOrDefault(o => o.Player.Id == player.Id) == null)
            {
                //First Question
                return (Studyset.Questions.First());
            }
            else
            {
                var count = AnsweredQuestions.Where(o => o.Player.Id == player.Id).Count();
                if (count < Studyset.Questions.Count) 
                {
                    return Studyset.Questions[count];
                }
                return null;
            }

        }

        public void ReportAnsweredQuestion (AnsweredQuestion question)
        {
            ThrowIfFinished();
            AnsweredQuestions.Add(question);
        }

        public void GameFinished ()
        {
            ThrowIfFinished();

            GameResult = new GameResult(AnsweredQuestions);
            GameState = GameState.Finished;

            
        }

        private void ThrowIfFinished ()
        {
            if (GameState == GameState.Finished)
                throw new NotImplementedException ();
        }
    }
}
