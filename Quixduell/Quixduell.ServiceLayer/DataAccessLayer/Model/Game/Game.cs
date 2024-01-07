using Quixduell.ServiceLayer.DataAccessLayer.Model.Game.AnsweredQuestion;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Game
{
    public abstract class Game
    {
        public Guid Id { get; set; }
        internal Studyset Studyset { get; set; }
        internal List<AnsweredQuestionBase> AnsweredQuestions { get; set; } = new List<AnsweredQuestionBase>();

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
                return (Studyset.Questions.FirstOrDefault());
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

        public void ReportOpenAnsweredQuestion (AnsweredOpenQuestion question)
        {
            ThrowIfFinished();
            AnsweredQuestions.Add(question);
        }
        public void ReportMultiQuestion(AnsweredMultiQuestion question)
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
