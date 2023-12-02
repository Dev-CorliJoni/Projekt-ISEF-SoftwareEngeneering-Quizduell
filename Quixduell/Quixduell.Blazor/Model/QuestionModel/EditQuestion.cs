using Quixduell.ServiceLayer.DataAccessLayer.Model;
using System.ComponentModel.DataAnnotations;

namespace Quixduell.Blazor.Model.QuestionModel
{
    /// <summary>
    /// Request Class for Edit a Question
    /// </summary>
    public class EditQuestion
    {

        public string Name { get; set; } = string.Empty;

        [Required]
        public Question_Type Question_Type { get; set; }


        [StringLength(1000, MinimumLength = 10)]
        public string QuestionText { get; set; } = string.Empty;

        [MinLength(1)]
        public List<Answer> Answers { get; set; }


        public EditQuestion(Question question)
        {
            Name = question.Name;
            QuestionText = question.QuestionText;
            Question_Type = question.Question_Type;
            Answers = question.Answer;
        }
    }
}
