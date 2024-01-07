using Quixduell.ServiceLayer.DataAccessLayer.Model.Answers;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Game.AnsweredQuestion
{
    public class AnsweredOpenQuestion : AnsweredQuestionBase
    {
        public bool IsRightAnswer { get; set; } = false;
        public string CustomAnswer { get; set; } = "";
        public AnsweredOpenQuestion(BaseQuestion question, User player, Answer rightAnswer) : base(question, player, rightAnswer)
        {
        }
    }
}
