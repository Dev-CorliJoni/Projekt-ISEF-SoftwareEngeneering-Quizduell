using Microsoft.AspNetCore.Components;

namespace Quixduell.Blazor.Shared.DialogComponent
{
    public partial class ShowDialogComponent
    {



        private RenderFragment? _componentBase;

        private string Header { get; set; }



    
        public void ShowDialog<Dialog,Value>(string header,Dialog dialog, Value value, Action<Value> onSubmit, Action onCancel) where Dialog : IComponent, IDialogBase<Value>
        {
            Header = header;
            Action<Value> onsubmit = (_) => { ResetDialog(); };
            onsubmit += onSubmit;
            Action oncancel = () => { ResetDialog(); };
            oncancel += onCancel;

            _componentBase = new RenderFragment(builder =>
            {
                builder.OpenComponent<Dialog>(0);
                builder.AddAttribute(1, "OnSubmit", onsubmit);
                builder.AddAttribute(2, "OnCancel", oncancel);
                builder.AddAttribute(3, "Value", value);
                builder.CloseComponent();
            });


            StateHasChanged();
        }


        private void ResetDialog ()
        {
            _componentBase = null;
            StateHasChanged();
        }

    }
}
