using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.Blazor.EditFormModel
{
    public class CreateEditQuestionFormModel
    {
        public QuestionType QuestionType { get; set; } = QuestionType.MultipleChoise;
        public List<CreateEditAnswerFormModel> AnswerFormModels { get; set; } = new List<CreateEditAnswerFormModel>();

        public string QuestionText { get; set; } = String.Empty;

        public string Hint { get; set; } = String.Empty;

        public CreateEditQuestionFormModel(BaseQuestion question)
        {
            if (question is MultipleChoiceQuestion multipleChoiseQuestion)
            {
                QuestionType = QuestionType.MultipleChoise;
                foreach (var answer in multipleChoiseQuestion.Answers)
                {
                    AnswerFormModels.Add(new CreateEditAnswerFormModel(answer));
                }
            }
            else if  (question is OpenQuestion openQuestion)
            {
                QuestionType = QuestionType.OpenText;
                AnswerFormModels.Add(new CreateEditAnswerFormModel(openQuestion.Answer));
            }

            QuestionText = question.Text;
            Hint = question.Hint;
        }

        public CreateEditQuestionFormModel()
        {
            QuestionText = "Question";
        }
    }





    public enum QuestionType 
    {
        MultipleChoise, OpenText
    }
}