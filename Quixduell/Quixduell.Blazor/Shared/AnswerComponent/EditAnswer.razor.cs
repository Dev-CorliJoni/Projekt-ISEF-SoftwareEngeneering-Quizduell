using Microsoft.AspNetCore.Components;
using Quixduell.Blazor.EditFormModel;

namespace Quixduell.Blazor.Shared.AnswerComponent
{
    public partial class EditAnswer
    {
        [Parameter]
        public CreateEditAnswerFormModel? Value { get; set; }

        [Parameter]
        public EventCallback<CreateEditAnswerFormModel> ValueChanged { get; set; }

        [Parameter]
        public EventCallback<CreateEditAnswerFormModel> OnSubmit { get; set; }

        private async Task OnSumbmit()
        {
            await OnSubmit.InvokeAsync(Value);
        }
    }
}
