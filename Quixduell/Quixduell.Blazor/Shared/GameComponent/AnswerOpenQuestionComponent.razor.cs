using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Quixduell.Blazor.Services;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Game.AnsweredQuestion;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.Shared.GameComponent
{
    public partial class AnswerOpenQuestionComponent
    {
        [Parameter]
        public OpenQuestion? Value { get; set; }

        [Parameter]
        public EventCallback<OpenQuestion?> ValueChanged { get; set; }

        [Parameter]
        public EventCallback<AnsweredOpenQuestion> QuestionAnswered { get; set; }

        [Parameter]
        public User User { get; set; }


        [Inject]
        public UserManager<User> UserManager { get; set; } = default!;


        private AnsweredOpenQuestion? _answeredQuestion;
        private bool _showHint = false;
        private bool _showOpenAnswer = false;
        private bool _questionAnswered = false;
        private bool _enableHintButton = false;
        private System.Timers.Timer _hintTimer;



        public AnswerOpenQuestionComponent()
        {
            _hintTimer = new System.Timers.Timer(TimeSpan.FromSeconds(10).TotalMilliseconds);
            _hintTimer.Elapsed += (sender, e) =>
            {
                _enableHintButton = true;
                InvokeAsync(StateHasChanged);
            };
            _hintTimer.AutoReset = false;
            _hintTimer.Start();
        }
        protected override async Task OnInitializedAsync()
        {
            if (User is null)
                return;
        }

        protected override void OnParametersSet()
        {
            _showHint = false;
            _showOpenAnswer = false;
            _questionAnswered = false;

            if (Value is null || User is null)
                return;




            _answeredQuestion = new AnsweredOpenQuestion(Value, User, Value.Answer);


        }


        private async Task QuestionComplete()
        {
            await QuestionAnswered.InvokeAsync(_answeredQuestion);
        }

    }
}
