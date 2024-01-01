using Quixduell.ServiceLayer.DataAccessLayer.Model.Answers;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Game
{
    public class AnsweredQuestionResult
    {
        public bool RightAnswer
        {
            get
            {
                return SelectedAnswer == CorrectAnswer;
            }
        }

        public BaseQuestion Question { get; set; }
        public Answer? SelectedAnswer { get; set; }

        public Answer CorrectAnswer { get; set; }

        public User Player { get; set; }

        public AnsweredQuestionResult(AnsweredQuestion answeredQuestion)
        {
            Question = answeredQuestion.Question;
            SelectedAnswer = answeredQuestion.SelectedAnswer;
            Player = answeredQuestion.Player;

            if (answeredQuestion.Question is MultipleChoiceQuestion multipleChoiceQuestion)
            {
                CorrectAnswer = multipleChoiceQuestion.Answers.Where(o => o.IsTrue).First();
            }
            if (answeredQuestion.Question is OpenQuestion openQuestion)
            {
                CorrectAnswer = openQuestion.Answer;
            }
        }
    }
}