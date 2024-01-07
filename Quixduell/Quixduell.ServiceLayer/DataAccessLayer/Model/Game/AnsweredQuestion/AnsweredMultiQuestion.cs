using Quixduell.ServiceLayer.DataAccessLayer.Model.Answers;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Game.AnsweredQuestion
{
    public class AnsweredMultiQuestion : AnsweredQuestionBase
    {
        public Answer? SelectedAnswer { get; set; }

        public AnsweredMultiQuestion(BaseQuestion question, User player, Answer rightAnswer) : base(question, player, rightAnswer)
        {
        }

    }
}
