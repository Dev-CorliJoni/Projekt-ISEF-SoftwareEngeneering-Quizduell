using Quixduell.ServiceLayer.DataAccessLayer.Model.Answers;

namespace Quixduell.Blazor.EditFormModel
{
    public class CreateEditAnswerFormModel
    {
        public bool IsTrue { get; set; } = true;
        public string AnswerText { get; set; } = string.Empty;
        public CreateEditAnswerFormModel(MultipleChoiceAnswer answer)
        {
            AnswerText = answer.Text;
            IsTrue = answer.IsTrue;
        }

        public CreateEditAnswerFormModel(Answer answer)
        {
            AnswerText = answer.Text;
        }
    }
}