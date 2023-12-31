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


        public AnsweredQuestion LoadNextQuestion(User player)
        {
            if (AnsweredQuestions.FirstOrDefault(o => o.Player.Id == player.Id) == null)
            {
                //First Question
                var question = new AnsweredQuestion(Studyset.Questions.First(), player);
                AnsweredQuestions.Add(question);
                return question;
            }
            else
            {
                var count = AnsweredQuestions.Where(o => o.Player.Id == player.Id).Count();
                if (count < Studyset.Questions.Count) 
                {
                    var question = new AnsweredQuestion(Studyset.Questions[count], player);
                    AnsweredQuestions.Add(question);
                    return question;
                }
                return AnsweredQuestions.Where(o => o.Player.Id == player.Id).Last();
            }

        }
    }
}
