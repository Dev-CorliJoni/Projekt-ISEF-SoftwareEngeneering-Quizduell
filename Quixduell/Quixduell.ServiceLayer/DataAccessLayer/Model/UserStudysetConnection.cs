namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class UserStudysetConnection : IdModel
    {
        public User User { get; set; }
        public Studyset Studyset { get; set; }


        public bool IsStored { get; set; }
        public Rating Rating { get; set; }
        public float Highscore { get; set; }

        private UserStudysetConnection() { }

        public UserStudysetConnection(User user, Studyset studyset, bool isStored) : this(user, studyset, isStored, new Rating(), 0)
        {
        }

        public UserStudysetConnection(User user, Studyset studyset, bool isStored, Rating rating, float highscore) : base()
        {
            User = user;
            Studyset = studyset;

            IsStored = isStored;
            Rating = rating;
            Highscore = highscore;
        }
    }
}
