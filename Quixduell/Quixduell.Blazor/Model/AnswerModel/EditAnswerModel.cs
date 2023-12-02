using Quixduell.ServiceLayer.DataAccessLayer.Model;
using System.ComponentModel.DataAnnotations;

namespace Quixduell.Blazor.Model.AnswerModel
{
    public class EditAnswerModel
    {
        public bool IsTrue { get; set; }

        [MinLength(7)]
        [MaxLength(150)]
        public string Text { get; set; } = "Answer";


        public EditAnswerModel(Answer answer)
        {
            IsTrue = answer.IsTrue;
            Text = answer.Text;
        }
    }
}
