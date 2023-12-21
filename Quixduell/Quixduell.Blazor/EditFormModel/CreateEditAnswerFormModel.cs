using Quixduell.ServiceLayer.DataAccessLayer.Model.Answers;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using System.ComponentModel.DataAnnotations;

namespace Quixduell.Blazor.EditFormModel
{
    public class CreateEditAnswerFormModel
    {
        public bool IsTrue { get; set; } = true;
        [StringLength(150, MinimumLength =5)]
        public string AnswerText { get; set; } = string.Empty;

        public CreateEditAnswerFormModel()
        {
        }
        public CreateEditAnswerFormModel(MultipleChoiceAnswer answer)
        {
            AnswerText = answer.Text;
            IsTrue = answer.IsTrue;
        }

        public CreateEditAnswerFormModel(Answer answer)
        {
            AnswerText = answer.Text;
        }

        public Answer ToAnswer ()
        {
            return new Answer(AnswerText);
        }

        public MultipleChoiceAnswer ToMultipleChoiceAnswer()
        {
            return new MultipleChoiceAnswer(AnswerText, IsTrue);
        }
    }
}