using Microsoft.AspNetCore.Components;
using Quixduell.Blazor.EditFormModel;
using Quixduell.Blazor.Helpers;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using System.Runtime.CompilerServices;

namespace Quixduell.Blazor.Shared.ControlComponents
{
    public partial class StartGameComponent
    {

        [Parameter]
        public Studyset? Value { get; set; }

        [Parameter]
        public EventCallback<Studyset> ValueChanged { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        private void Play()
        {
            if (Value is not null)
            {
                NavigationManager.NavigateTo($"{PageUri.SingeplayerPage}?studyset={Value.Id}");
            }
        }
        private void PlayMulti()
        {
            if (Value is not null)
            {
                NavigationManager.NavigateTo($"{PageUri.MultiplayerPage}?studyset={Value.Id}");
            }
        }
    }
}
