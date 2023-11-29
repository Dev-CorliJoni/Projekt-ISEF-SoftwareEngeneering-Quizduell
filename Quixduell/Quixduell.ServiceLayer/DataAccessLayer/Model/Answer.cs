using System.ComponentModel.DataAnnotations;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class Answer
    {
        [Key]
        public Guid AnswerID { get; set; }

        [Required]
        [StringLength(150)]
        public string Text { get; set; } = string.Empty;

        public bool IsTrue { get; set; } 


    }
}