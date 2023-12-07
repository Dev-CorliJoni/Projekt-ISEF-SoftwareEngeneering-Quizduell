using Quixduell.ServiceLayer.DataAccessLayer.Model;
using System.ComponentModel.DataAnnotations;

namespace Quixduell.Blazor.Model.AnswerModel
{
    /// <summary>
    /// Model for Validation of Editing and Answer, uses DataAnnotations for Validation
    /// </summary>
    public class EditAnswerModel
    {
        public bool IsTrue { get; set; }

        [MinLength(7)]
        [MaxLength(150)]
        public string Text { get; set; } = "";


        public EditAnswerModel(Answer answer)
        {
            IsTrue = answer.IsTrue;
            Text = answer.Text;
        }
    }
}
