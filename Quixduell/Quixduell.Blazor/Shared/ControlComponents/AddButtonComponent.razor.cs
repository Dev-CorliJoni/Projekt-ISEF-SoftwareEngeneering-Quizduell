using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Quixduell.Blazor.Shared.ControlComponents
{
    public partial class AddButtonComponent
    {
        [Parameter] public Func<MouseEventArgs, Task> OnClickAsync { get; set; }

        public bool OnHover { get; set; }
    }
}
