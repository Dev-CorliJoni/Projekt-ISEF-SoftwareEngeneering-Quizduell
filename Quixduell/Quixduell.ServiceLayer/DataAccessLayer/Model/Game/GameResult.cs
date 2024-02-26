using Quixduell.ServiceLayer.DataAccessLayer.Model.Game.AnsweredQuestion;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Game
{
    public class GameResult
    {

        public Studyset Studyset { get; set; }
        public List<AnsweredQuestionResult>  AnsweredQuestionResults { get; set; }

        public GameResult(Studyset studyset, List<AnsweredQuestionBase> answeredQuestions)
        {
            Studyset = studyset;
            AnsweredQuestionResults = new List<AnsweredQuestionResult>();
            foreach (var answer in answeredQuestions) 
            {
                if (answer is AnsweredOpenQuestion answeredOpen) 
                {
                    AnsweredQuestionResults.Add(new AnsweredQuestionResult(answeredOpen));
                }
                else if (answer is AnsweredMultiQuestion answeredMulti)
                {
                    AnsweredQuestionResults.Add(new AnsweredQuestionResult(answeredMulti));
                }

            }
        }

    }
}