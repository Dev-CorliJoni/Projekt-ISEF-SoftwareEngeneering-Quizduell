using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Quixduell.Blazor.Services;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.Shared.StudysetView
{
    public partial class StudysetViewContentUserInformation
    {
        [Inject]
        private ServiceLayer.ServiceLayer.StudysetView StudysetView { get; set; } = default!;

        [Parameter]
        public User User { get; set; }
        [Parameter]
        public Studyset Studyset { get; set; }

        private bool _isAddContributorActive = false;

        public bool IsAddContributorActive() => _isAddContributorActive;
        public bool IsUserAdmin() => Studyset.Creator == User || Studyset.Contributors.Contains(User);

        public void ActivateAddContributor(MouseEventArgs e)
        {
            _isAddContributorActive = true;
        }

        public async Task AddContributorAsync(User u)
        {
            await StudysetView.AddContributorAsync(Studyset, u);

            _isAddContributorActive = false;
            StateHasChanged();
        }
    }
}
