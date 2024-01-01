namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Game
{
    public class GameResult
    {
        public List<AnsweredQuestionResult>  AnsweredQuestionResults { get; set; }

        public GameResult(List<AnsweredQuestion> answeredQuestions)
        {
            AnsweredQuestionResults = new List<AnsweredQuestionResult>();
            foreach (var answer in answeredQuestions) 
            {
                AnsweredQuestionResults.Add(new AnsweredQuestionResult(answer));
            }
        }

    }
}