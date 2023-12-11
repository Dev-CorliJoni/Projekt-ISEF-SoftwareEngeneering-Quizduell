using System.ComponentModel.DataAnnotations;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Answers
{
    public class Answer : IdModel
    {
        [Required]
        [StringLength(150)]
        public string Text { get; set; } = string.Empty;

        public Answer(Guid id, string text) : base(id)
        { 
            Text = text;
        }
    }
}