using Quixduell.ServiceLayer.DataAccessLayer.Model.Answers;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Game.AnsweredQuestion;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Game
{
    public class AnsweredQuestionResult
    {
        public bool RightAnswer { get; set; } = false;

        public BaseQuestion Question { get; set; }
        public string? SelectedAnswerText { get; set; }

        public Answer CorrectAnswer { get; set; }

        public User Player { get; set; }

        public AnsweredQuestionResult(AnsweredMultiQuestion answeredQuestion)
        {
            Question = answeredQuestion.Question;
            SelectedAnswerText = answeredQuestion.SelectedAnswer?.Text;
            Player = answeredQuestion.Player;
            CorrectAnswer = answeredQuestion.RightAnswer;

            if (answeredQuestion.SelectedAnswer == answeredQuestion.RightAnswer)
            {
                RightAnswer = true;
            }
        }

        public AnsweredQuestionResult(AnsweredOpenQuestion answeredQuestion)
        {
            Question = answeredQuestion.Question;
            SelectedAnswerText = answeredQuestion.CustomAnswer;
            Player = answeredQuestion.Player;
            CorrectAnswer = answeredQuestion.RightAnswer;

            RightAnswer = answeredQuestion.IsRightAnswer;
        }
    }
}