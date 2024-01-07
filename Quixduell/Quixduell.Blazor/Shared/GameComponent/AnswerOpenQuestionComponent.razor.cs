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

        [Inject]
        public UserService UserService { get; set; } = default!;

        [Inject]
        public UserManager<User> UserManager { get; set; } = default!;


        private AnsweredOpenQuestion? _answeredQuestion;
        private bool _showHint = false;
        private bool _showOpenAnswer = false;
        private bool _questionAnswered = false;
        private User? _user;



        protected override async Task OnInitializedAsync()
        {
            var user = await UserService.GetAuthenticatedUserOrRedirect(UserManager);
            if (user is null)
                return;

            _user = user;
        }

        protected override void OnParametersSet()
        {
            _showHint = false;
            _showOpenAnswer = false;
            _questionAnswered = false;

            if (Value is null || _user is null)
                return;




            _answeredQuestion = new AnsweredOpenQuestion(Value, _user, Value.Answer);


        }


        private async Task QuestionComplete()
        {
            await QuestionAnswered.InvokeAsync(_answeredQuestion);
        }

    }
}
