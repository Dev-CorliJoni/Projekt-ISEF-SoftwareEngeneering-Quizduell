using Microsoft.AspNetCore.Components;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Quixduell.Blazor.Shared.ControlComponents
{
    public partial class StarComponent
    {
        [Inject]
        private ServiceLayer.ServiceLayer.StudysetView StudysetView { get; set; } = default!;

        [Parameter]
        public User User { get; set; } = default!;

        [Parameter]
        public Studyset Studyset { get; set; } = default!;
        [Parameter]
        public UserStudysetConnection Connection { get; set; } = default!;

        private string GetStarColor()
        {
            // Golden Yellow || darkgray
            return Connection.IsStored ? "#FFC000" : "darkgray";
        }

        private async Task StarFunction(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {            
            await StudysetView.StarStudysetAsync(Studyset, Connection, User);
        }
    }
}
