using Microsoft.AspNetCore.Components;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Game;

namespace Quixduell.Blazor.Shared.GameComponent
{
    public partial class GameResultComponent
    {
        [Parameter]
        public GameResult Value { get; set; }

        [Parameter]
        public EventCallback<GameResult> ValueChanged { get; set; }


        private string GetColor(AnsweredQuestionResult result)
        {

            if (result.RightAnswer)
                return "bg-success";

            return "bg-danger";
        }
    }
}
