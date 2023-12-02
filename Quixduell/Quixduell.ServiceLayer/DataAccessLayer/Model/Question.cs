using System.ComponentModel.DataAnnotations;


namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class Question
    {
        [Key]
        public Guid QuestionID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public Question_Type Question_Type { get; set; }

        [Required]
        [StringLength(1000)]
        public string QuestionText { get; set; } = string.Empty;

        public List<Answer> Answer { get; set; } = new List<Answer>();

        public Lernset? Lernset { get; set; }

    }
}
