using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Quixduell.Blazor.Services;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.Blazor.Shared.StudysetView
{
    public partial class StudysetViewContentQuestionInformation
    {
        [Parameter] 
        public Studyset Studyset { get; set; }

        private Dictionary<string, Func<int>> _questionData;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            _questionData = new Dictionary<string, Func<int>>
            {
                { "Questions", () => Studyset.Questions.Count },
                { "Open Questions", () => Studyset.Questions.Count(q => q.GetType() == typeof(OpenQuestion)) },
                { "Multiple Choice Questions", () => Studyset.Questions.Count(q => q.GetType() == typeof(MultipleChoiceQuestion)) }
            };
        }

    }
}
