using Quixduell.ServiceLayer.DataAccessLayer.Model;
using System.ComponentModel.DataAnnotations;

namespace Quixduell.Blazor.Model.AnswerModel
{
    public class EditAnswerModel
    {
        public bool IsTrue { get; set; }

        [StringLength(150, MinimumLength =10)]
        public string Text { get; set; }


        public EditAnswerModel(Answer answer)
        {
            IsTrue = answer.IsTrue;
            Text = answer.Text;
        }
    }
}
