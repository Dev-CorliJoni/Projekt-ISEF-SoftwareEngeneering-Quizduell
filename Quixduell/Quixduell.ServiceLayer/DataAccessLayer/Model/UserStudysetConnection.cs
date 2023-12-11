using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class UserStudysetConnection : IdModel
    {
        public User User { get; set; }
        public Studyset Studyset { get; set; }

        public Rating Rating { get; set; }
        public float Highscore { get; set; }

        private UserStudysetConnection() { }

        public UserStudysetConnection(User user, Studyset studyset, Rating rating, float highscore) : base()
        {
            User = user;
            Studyset = studyset;
            Rating = rating;
            Highscore = highscore;
        }
    }
}
