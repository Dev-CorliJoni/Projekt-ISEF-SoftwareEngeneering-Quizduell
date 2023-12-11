using System.ComponentModel.DataAnnotations;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class ProcessedQuestion
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public QuestionProgress QuestionProgress { get; set; }

        [Required]
        public BaseQuestion Question { get; set; }
    }
}