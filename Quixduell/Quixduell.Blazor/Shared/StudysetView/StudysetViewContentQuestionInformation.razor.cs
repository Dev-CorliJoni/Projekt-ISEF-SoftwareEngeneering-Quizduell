using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Quixduell.Blazor.EditFormModel;
using Quixduell.Blazor.Shared.ControlComponents;
using Quixduell.Blazor.Shared.QuestionComponent;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;
using Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality;

namespace Quixduell.Blazor.Shared.StudysetView
{
    public partial class StudysetViewContentQuestionInformation
    {

        [Inject]
        private StudysetDataAccess StudysetDataAccess { get; set; } = default!;

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

        public async Task OpenAddQuestionAsync(MouseEventArgs e)
        {
            var success = false;
            Layout.Dialog.ShowDialog("Frage hinzufügen", new EditQuestion(), new CreateEditQuestionFormModel(), async (CreateEditQuestionFormModel form) =>
            {
                success = true;
                Studyset.Questions.Add(form.ToBaseQuestion());
                await StudysetDataAccess.UpdateAsync(Studyset);
                StateHasChanged();
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
