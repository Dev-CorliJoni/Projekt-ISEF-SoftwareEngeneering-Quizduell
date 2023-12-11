using System.ComponentModel.DataAnnotations;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class Studyset : IdModel
    {
        [Required]
        public Category Category { get; set; }

        [Required]
        public User Creator { get; set; }
        public List<User> Contributors { get; set; } = new List<User>();

        [Required]
        public List<BaseQuestion> Questions { get; set; } = new List<BaseQuestion>(); 
        public List<UserStudysetConnection> Connections { get; set; }

        public Studyset(Guid id, Category category, User creator, List<User> contributors, List<BaseQuestion> questions, List<UserStudysetConnection> connections) : base(id)
        {
            Category = category;
            Creator = creator;
            Contributors = contributors;
            Questions = questions;
            Connections = connections;
        }
    
    }
}