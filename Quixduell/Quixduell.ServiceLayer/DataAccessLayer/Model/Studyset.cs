using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class Studyset : IdModel
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        public Category Category { get; set; }

        [Required]
        public User Creator { get; set; }
        public List<User> Contributors { get; set; } 

        [Required]
        public List<BaseQuestion> Questions { get; set; }
        public List<UserStudysetConnection> Connections { get; set; } = new List<UserStudysetConnection>();

        public Studyset(){}

        public Studyset(string name, Category category, User creator, List<User> contributors, List<BaseQuestion> questions, List<UserStudysetConnection> userStudysetConnections) : base()
        {
            Name = name;
            Category = category;
            Creator = creator;
            Contributors = contributors;
            Questions = questions;
            Connections = userStudysetConnections;

        }

    }
}