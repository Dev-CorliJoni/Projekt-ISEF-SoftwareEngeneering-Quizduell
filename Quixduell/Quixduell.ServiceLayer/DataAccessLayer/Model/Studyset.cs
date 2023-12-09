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
        public List<BaseQuestion> Questions { get; set; } = new List<BaseQuestion>(); // Ist der initialwert nötig
        public List<UserStudysetConnection> Connections { get; set; }
        
        private Studyset() //Brauch man den leeren Konstruktor
        {
            
        }

        public Studyset(User creator, Category category, List<BaseQuestion> questions)
        {
            Creator = creator;
            Category = category;
            Questions = questions;
        }
    
    }
}