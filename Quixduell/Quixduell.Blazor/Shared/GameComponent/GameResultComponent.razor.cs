using Microsoft.AspNetCore.Components;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Game;
using System.Data;
using System.Linq;

namespace Quixduell.Blazor.Shared.GameComponent
{
    public partial class GameResultComponent
    {
        [Parameter]
        public GameResult? Value { get; set; }

        [Parameter]
        public EventCallback<GameResult> ValueChanged { get; set; }


        private IOrderedEnumerable<IGrouping<string?, AnsweredQuestionResult>>? _orderResults;

        protected override void OnParametersSet()
        {
            _orderResults = GetResultOrderedByUser();


            base.OnParametersSet();
        }

        private string GetColor(AnsweredQuestionResult result)
        {

            if (result.RightAnswer)
                return "bg-success";

            return "bg-danger";
        }

        private IOrderedEnumerable<IGrouping<string?,AnsweredQuestionResult>>? GetResultOrderedByUser ()
        {
            if (Value is not null)
            {
                var result = Value.AnsweredQuestionResults.GroupBy(o => o.Player.Email)
                    .OrderByDescending(o => o.Count(o =>o.RightAnswer));

                if (result is not null)
                    return result;
            }
            return null;
        }
    }
}
