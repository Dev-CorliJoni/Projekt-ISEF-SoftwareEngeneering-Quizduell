using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Game
{
    public abstract class Game
    {
        public Guid Id { get; set; }
        internal Studyset Studyset { get; set; }
        internal List<AnsweredQuestion> AnsweredQuestions { get; set; } = new List<AnsweredQuestion>();

        public Game(Studyset studyset) 
        {
            Id = Guid.NewGuid();
            Studyset = studyset;
        }


        public BaseQuestion? LoadNextQuestion(User player)
        {
            if (AnsweredQuestions.FirstOrDefault(o => o.Player.Id == player.Id) == null)
            {
                //First Question
                return (Studyset.Questions.First());
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

        public void ReportAnsweredQuestion (AnsweredQuestion question)
        {
            AnsweredQuestions.Add(question);
        }
    }
}
