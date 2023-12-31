using Quixduell.ServiceLayer.DataAccessLayer.Model.Answers;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Game
{
    public class AnsweredQuestion
    {
        public BaseQuestion Question { get; set; }
        public Answer? SelectedAnswer { get; set; }

        public User Player { get; set; }

        public AnsweredQuestion(BaseQuestion question, User player)
        {
            Question = question;
            Player = player;
        }
    }
}
