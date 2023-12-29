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
            // Golden Yellow || darkgray
            return Connection.IsStored ? "#FFC000" : "darkgray";
        }

        private Task StarFunction(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            return Task.Run(() =>
            {
                Connection.IsStored = !Connection.IsStored;
            });
        }
    }
}
