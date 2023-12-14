namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class UserStudysetConnection : IdModel
    {

        private bool _isStored;
        private float _highscore;


        public User User { get; set; }
        public Studyset Studyset { get; set; }
        public Rating Rating { get; set; }

        public bool IsStored
        {
            get => _isStored;
            set
            {
                LastSeen = DateTime.Now;
                _isStored = value;
            }
        }
        public float Highscore
        {
            get => _highscore;
            set
            {
                LastSeen = DateTime.Now;
                _highscore = value;
            }
        }

        public DateTime LastSeen { get; set; }

        private UserStudysetConnection() 
        {
            LastSeen = DateTime.Now;
        }

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
