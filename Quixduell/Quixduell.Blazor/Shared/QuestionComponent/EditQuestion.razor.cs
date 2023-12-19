using Microsoft.AspNetCore.Components;
using Quixduell.Blazor.EditFormModel;

namespace Quixduell.Blazor.Shared.QuestionComponent
{
    public partial class EditQuestion
    {


        [Parameter]
        public CreateEditQuestionFormModel? Value { get; set; }

        [Parameter]
        public EventCallback<CreateEditQuestionFormModel> ValueChanged { get; set; }



        protected override void OnParametersSet()
        {
            if (Value is null) 
            {
                throw new NotImplementedException();
            }
            base.OnParametersSet();
        }

        private async Task ValidSubmitAsync ()
        {
            await ValueChanged.InvokeAsync(Value);

        }

      
    }
}
