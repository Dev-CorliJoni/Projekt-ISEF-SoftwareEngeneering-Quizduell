using System.ComponentModel.DataAnnotations;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Questions
{
    public abstract class BaseQuestion : IdModel
    {
        [Required]
        [StringLength(1000)]
        public string Text { get; set; } = string.Empty;


        [StringLength(1000)]
        public string Hint { get; set; } = string.Empty;

        protected BaseQuestion(Guid id, string text, string hint) : base(id) 
        {
            Text = text;
            Hint = hint;
        }
    }
}
