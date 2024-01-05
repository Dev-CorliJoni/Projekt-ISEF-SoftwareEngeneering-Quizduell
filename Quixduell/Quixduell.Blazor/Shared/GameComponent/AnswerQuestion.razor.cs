﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Quixduell.Blazor.Services;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Game;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.Blazor.Shared.GameComponent
{
    public partial class AnswerQuestion
    {
        [Parameter]
        public BaseQuestion? Value { get; set; }

        [Parameter]
        public EventCallback<BaseQuestion?> ValueChanged { get; set; }

        [Parameter]
        public EventCallback<AnsweredQuestion> QuestionAnswered { get; set; }

        [Inject]
        public UserService UserService { get; set; } = default!;

        [Inject]
        public UserManager<User> UserManager { get; set; } = default!;


        private AnsweredQuestion? _answeredQuestion;
        private bool _showHint = false;
        private bool _showOpenAnswer = false;
        private bool _questionAnswered = false;
        private User? _user;


        protected override async  Task OnInitializedAsync()
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

            _answeredQuestion = new AnsweredQuestion(Value, _user);
        }


        private async Task QuestionComplete ()
        {
            await QuestionAnswered.InvokeAsync(_answeredQuestion);
        }

    }
}
