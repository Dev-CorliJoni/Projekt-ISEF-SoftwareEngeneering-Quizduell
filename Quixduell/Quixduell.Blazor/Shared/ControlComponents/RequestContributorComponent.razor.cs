using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Quixduell.Blazor.Shared.ControlComponents
{
    public partial class RequestContributorComponent
    {
        [CascadingParameter]
        public MainLayout Layout { get; set; }

        [Parameter]
        public bool Requested { get; set; } = false;

        [Parameter]
        public Func<MouseEventArgs, Task<bool>> Request { get; set; } = default!;

        public async Task Click(MouseEventArgs e)
        {
            if (Requested == false)
            {
                bool validAction = await Request(e);
                if (validAction == false)
                {
                    return;
                }

                this.Requested = true;
                Layout.Alert.AddAlert("The request to become a contributor has been sent!", new TimeSpan(0, 0, 5), AlertComponent.AlertMessageType.Success);
            }
            else
            {
                Layout.Alert.AddAlert("You have already applied to become a contributor!", new TimeSpan(0, 0, 5), AlertComponent.AlertMessageType.Information);
            }
        }
    }
}
