using Microsoft.AspNetCore.Components;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.Shared.StudysetView
{
    public partial class StudysetViewContent
    {
        [Parameter]
        public User User { get; set; }
        [Parameter]
        public Studyset Studyset { get; set; }
    }
}
