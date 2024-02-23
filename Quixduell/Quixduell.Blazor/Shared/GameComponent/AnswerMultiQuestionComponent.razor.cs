using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using Quixduell.Blazor.Services;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Game.AnsweredQuestion;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using System.Reflection.Metadata;

namespace Quixduell.Blazor.Shared.GameComponent
{
    public partial class AnswerMultiQuestionComponent
    {
        [Parameter]
        public MultipleChoiceQuestion? Value { get; set; }

        [Parameter]
        public EventCallback<MultipleChoiceQuestion?> ValueChanged { get; set; }

        [Parameter]
        public EventCallback<AnsweredMultiQuestion> QuestionAnswered { get; set; }

        [Parameter]
        public User User { get; set; }

        [Inject]
        private IJSRuntime JSRuntime { get; set; } = default!;

        private AnsweredMultiQuestion? _answeredQuestion;
        private bool _showHint = false;
        private bool _questionAnswered = false;
        private bool _enableHintButton = false;
        private System.Timers.Timer _hintTimer;


        public AnswerMultiQuestionComponent()
        {
            _hintTimer = new System.Timers.Timer(TimeSpan.FromSeconds(10).TotalMilliseconds);
            _hintTimer.Elapsed += (sender, e) =>
            {
                _enableHintButton = true;
                InvokeAsync(StateHasChanged);
            };
            _hintTimer.AutoReset = false;

            ResetTimer();
        }
        protected override void OnInitialized()
        {
            if (User is null)
                return;

            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            ResetTimer();
            _showHint = false;
            _questionAnswered = false;

            if (Value is null || User is null)
                return;

            _answeredQuestion = new AnsweredMultiQuestion(Value, User, Value.Answers.First(o => o.IsTrue));
        }


        private async Task QuestionComplete()
        {
            await QuestionAnswered.InvokeAsync(_answeredQuestion);
            await JSRuntime.InvokeVoidAsync("clearRadio");
        }

    public void ResetTimer()
        {
            _hintTimer.Stop();
            _hintTimer.Start();
        }
    }
}
