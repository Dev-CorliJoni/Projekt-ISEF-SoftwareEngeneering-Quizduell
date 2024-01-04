using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.Shared.StudysetView
{
    public partial class StudysetViewContentUserInformation
    {
        [Parameter]
        public Studyset Studyset { get; set; }

        public void AddContributor(MouseEventArgs e)
        {

        }

    }
}
