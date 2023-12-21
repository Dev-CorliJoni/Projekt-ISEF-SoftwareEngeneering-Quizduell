using Microsoft.AspNetCore.Components;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Quixduell.Blazor.Shared.ControlComponents
{
    public partial class StarComponent
    {
        [Parameter]
        public UserStudysetConnection Connection { get; set; } = default!;

        private string GetStarColor()
        {
            return Connection.IsStored ? "yellow" : "lightgray";
        }

        private async Task StarFunction(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            await Task.Run(() =>
            {
                Connection.IsStored = !Connection.IsStored;
            });
        }
    }
}
