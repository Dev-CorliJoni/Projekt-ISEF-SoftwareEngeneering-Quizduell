using System.ComponentModel.DataAnnotations;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class Studyset
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public Category Category { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();

        public AppUser Creator { get; set; }

        public List<AppUser> Contributors { get; set; } = new List<AppUser>();

        private Studyset()
        {
            
        }

        public Studyset(AppUser creator, Category category)
        {
            Creator = creator;
            Category = category;
        }
    
    }
}