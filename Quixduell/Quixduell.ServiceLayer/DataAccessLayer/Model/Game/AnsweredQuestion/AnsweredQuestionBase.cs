using Quixduell.ServiceLayer.DataAccessLayer.Model.Answers;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Game.AnsweredQuestion
{
    public abstract class AnsweredQuestionBase
    {
        public BaseQuestion Question { get; set; }
        public Answer RightAnswer { get; set; }
        public User Player { get; set; }

        public AnsweredQuestionBase(BaseQuestion question, User player, Answer rightAnswer)
        {
            Question = question;
            Player = player;
            RightAnswer = rightAnswer;
        }
    }
}
