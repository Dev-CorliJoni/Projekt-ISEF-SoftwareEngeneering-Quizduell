using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class UserStudysetConnection
    {
        [Key]
        public User User { get; set; }
        [Key]
        public Studyset Studyset { get; set; }

        [Range(0, 5)]
        public float Rating { get; set; }
        [Range(0, 1)]
        public float Highscore { get; set; }
    }
}
