using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Quixduell.Blazor.Shared.ControlComponents
{
    public partial class AddButtonComponent
    {
        [Parameter] public Action<MouseEventArgs> OnClick { get; set; }

    }
}
