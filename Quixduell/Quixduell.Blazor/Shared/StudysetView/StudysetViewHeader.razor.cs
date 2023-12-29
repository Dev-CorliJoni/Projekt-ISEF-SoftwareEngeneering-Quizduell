using Microsoft.AspNetCore.Components;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.Shared.StudysetView
{
    public partial class StudysetViewHeader
    {
        [Parameter]
        public User User { get; set; }
        [Parameter] 
        public Studyset Studyset { get; set; }


        private UserStudysetConnection GetConnection()
        {
            return Studyset?.Connections.Find((sc) => sc.User == User)!;
        }
    }
}
