using Microsoft.AspNetCore.Components;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.Shared.StudysetView
{
    public partial class StudysetViewContentUserInformation
    {
        [Parameter]
        public Studyset Studyset { get; set; }

    }
}
