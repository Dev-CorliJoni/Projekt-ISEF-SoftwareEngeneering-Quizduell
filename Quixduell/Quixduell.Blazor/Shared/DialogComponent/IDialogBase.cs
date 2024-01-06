using Microsoft.AspNetCore.Components;

namespace Quixduell.Blazor.Shared.DialogComponent
{
    public interface IDialogBase<T>
    {


        public T Value { get; set; }
        public Action<T> OnSubmit { get; set; }

        [Parameter]
        public Action OnCancel { get; set;}
    }
}
