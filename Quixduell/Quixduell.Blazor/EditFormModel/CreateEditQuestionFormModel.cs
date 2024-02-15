using Quixduell.ServiceLayer.DataAccessLayer.Model.Answers;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using System.ComponentModel.DataAnnotations;

namespace Quixduell.Blazor.EditFormModel
{
    public class CreateEditQuestionFormModel
    {
        public QuestionType QuestionType { get; set; } = QuestionType.MultipleChoice;

        [Required]
        [MinLength(1)]
        public List<CreateEditAnswerFormModel> AnswerFormModels { get; set; } = new List<CreateEditAnswerFormModel>();

        [StringLength(1000, MinimumLength = 5)]
        public string QuestionText { get; set; } = String.Empty;

        [StringLength(1000, MinimumLength = 5)]
        public string Hint { get; set; } = String.Empty;

        public CreateEditQuestionFormModel()
        {
            QuestionText = "";
        }

        public CreateEditQuestionFormModel(BaseQuestion question)
        {
            if (question is MultipleChoiceQuestion multipleChoiseQuestion)
            {
                QuestionType = QuestionType.MultipleChoice;
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

        public BaseQuestion ToBaseQuestion()
        {
            switch (QuestionType)
            {
                case QuestionType.MultipleChoice:
                    var multipList = new List<MultipleChoiceAnswer>();
                    foreach (var answer in AnswerFormModels)
                    {
                        multipList.Add(answer.ToMultipleChoiceAnswer());
                    }
                    return new MultipleChoiceQuestion(QuestionText, Hint, multipList);
                case QuestionType.OpenText:
                    var answerList = new List<Answer>();
                    foreach (var answer in AnswerFormModels)
                    {
                        answerList.Add(answer.ToAnswer());
                    }
                    return new OpenQuestion(QuestionText, Hint, answerList.First());
            }
            throw new NotImplementedException();
        }


    }

    public enum QuestionType 
    {
        MultipleChoice, OpenText
    }
}