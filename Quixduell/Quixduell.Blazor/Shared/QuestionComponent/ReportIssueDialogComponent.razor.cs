using Microsoft.AspNetCore.Components;
using Quixduell.Blazor.EditFormModel;
using Quixduell.Blazor.Shared.DialogComponent;

namespace Quixduell.Blazor.Shared.QuestionComponent
{
    public partial class ReportIssueDialogComponent : IDialogBase<CreateIssueFormModel>
    {
        [Parameter]
        public CreateIssueFormModel? Value { get; set; }
        [Parameter]
        public Action<CreateIssueFormModel>? OnSubmit { get; set; }
        [Parameter]
        public Action? OnCancel { get; set; }


        private void OnAbort()
        {
            OnCancel?.Invoke();
        }

        private void OnValidSubmit ()
        {
            OnSubmit?.Invoke(Value!);
        }
    }
}
