using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.Blazor.EditFormModel
{
    public class CreateIssueFormModel
    {
        public string Issue { get; set; } = String.Empty;
        public BaseQuestion ReportedQuestion { get; set; }


        public CreateIssueFormModel(BaseQuestion reportedQuestion)
        {
            ReportedQuestion = reportedQuestion;
        }
    }
}
