using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Quixduell.Blazor.EditFormModel;

namespace Quixduell.Blazor.Shared.AnswerComponent
{
    public partial class EditAnswer
    {
        [Parameter]
        public CreateEditAnswerFormModel? Value { get; set; }

        [Parameter]
        public EventCallback<CreateEditAnswerFormModel> ValueChanged { get; set; }





        protected override void OnParametersSet()
        {
            if (Value is null)
            {
                throw new NotImplementedException();
            }


            base.OnParametersSet();
        }

        private async Task OnSumbmit ()
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }
}
