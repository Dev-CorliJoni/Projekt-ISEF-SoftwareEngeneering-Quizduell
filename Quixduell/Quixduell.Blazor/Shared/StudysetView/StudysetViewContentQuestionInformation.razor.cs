using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Quixduell.Blazor.EditFormModel;
using Quixduell.Blazor.Shared.ControlComponents;
using Quixduell.Blazor.Shared.QuestionComponent;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.Blazor.Shared.StudysetView
{
    public partial class StudysetViewContentQuestionInformation
    {
        [Parameter]
        public User User { get; set; }
        [Parameter]
        public Studyset Studyset { get; set; }

        [CascadingParameter]
        public MainLayout Layout { get; set; }

        private List<QuestionData> _questionData;

        public List<PieChartPart> GetPieChartParts
        {
            get => _questionData.Skip(1).Select(q => new PieChartPart(q.Percentage, q.Color)).ToList();
        }

        public bool IsUserAdmin() => Studyset.Creator == User || Studyset.Contributors.Contains(User);

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var questionCount = () => Studyset.Questions.Count;
            var openQuestionCount = () => Studyset.Questions.Count(q => q.GetType() == typeof(OpenQuestion));
            var multipleChoiceQuestionCount = () => Studyset.Questions.Count(q => q.GetType() == typeof(MultipleChoiceQuestion));

            _questionData = new List<QuestionData>
            {
                new("Questions", "", questionCount, questionCount),
                new("Open Questions", "#5F0F40", openQuestionCount, questionCount),
                new("Multiple Choice Questions", "#9A031E", multipleChoiceQuestionCount, questionCount)
            };
        }

        public void OpenAddQuestion(MouseEventArgs e)
        {
            var questionForm = new CreateEditQuestionFormModel();
            Layout.Dialog.ShowDialog<EditQuestion, CreateEditQuestionFormModel>("Frage hinzufügen",new EditQuestion(), questionForm, (CreateEditQuestionFormModel form) =>
            {
                //Speichern direkt oder was soll passieren ? :D 
            },() => { });
        }

    }

    public class QuestionData(string title, string color, Func<int> getOwnCount, Func<int> getFullCount)
    {
        public string Title { get; set; } = title;
        public string Color { get; set; } = color;
        public int Percentage { get => (int)(getOwnCount() / (getFullCount() / 100.0)); }
        public Func<int> GetCount { get; set; } = getOwnCount;
    }
}
