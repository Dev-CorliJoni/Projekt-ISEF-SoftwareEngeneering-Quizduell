using Quixduell.ServiceLayer.DataAccessLayer.Model.Answers;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Questions
{
    public class OpenQuestion : BaseQuestion
    {
        public Answer Answer {  get; set; }

        private OpenQuestion() { }

        public OpenQuestion(string text, string hint, Answer answer) : base(text, hint) {
            Answer = answer;
        }
    }
}
