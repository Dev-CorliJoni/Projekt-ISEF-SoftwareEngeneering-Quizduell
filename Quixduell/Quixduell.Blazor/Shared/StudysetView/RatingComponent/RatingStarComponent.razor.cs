using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Quixduell.Blazor.Shared.StudysetView.RatingComponent
{
    public partial class RatingStarComponent
    {
        [Parameter]
        public int Id { get; set; }
        [Parameter]
        public bool IsFilled { get; set; }

        [Parameter]
        public EventCallback<int> OnToggle { get; set; }

        private string StarColor => IsFilled ? "#FFD700" : "#C0C0C0";


        private void ToggleStar(MouseEventArgs e)
        {
            if (e.Button == 0)
            {
                IsFilled = !IsFilled;
                OnToggle.InvokeAsync(Id);
            }
        }
    }
}
