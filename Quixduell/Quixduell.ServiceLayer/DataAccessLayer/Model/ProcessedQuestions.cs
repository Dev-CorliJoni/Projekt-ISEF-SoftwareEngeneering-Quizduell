using System.ComponentModel.DataAnnotations;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class ProcessedQuestion
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public QuestionProgress QuestionProgress { get; set; }

        [Required]
        public Question Question { get; set; }
    }
}