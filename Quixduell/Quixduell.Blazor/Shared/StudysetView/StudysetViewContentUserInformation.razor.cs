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

        [CascadingParameter]
        public MainLayout Layout { get; set; }

        [Parameter]
        public User User { get; set; }
        [Parameter]
        public Studyset Studyset { get; set; }

        private bool _isAddContributorActive = false;

        public bool IsAddContributorActive() => _isAddContributorActive;
        public bool IsUserAdmin() => Studyset.Creator == User || Studyset.Contributors.Contains(User);

        public async Task ActivateAddContributor(MouseEventArgs e)
        {
            await Task.Run(() =>
            {
                _isAddContributorActive = true;
            });
        }

        public async Task AddContributorAsync(User u)
        {
            bool result = await StudysetView.AddContributorAsync(Studyset, u);

            if (result == false)
            {
                Layout.Alert.AddAlert("The user is already creator or contributor and cannot be added therefore!", new TimeSpan(0,0, 5), AlertComponent.AlertMessageType.Error);
            }

            _isAddContributorActive = false;
            StateHasChanged();
        }

        public async Task<bool> RequestToBeAContributor(MouseEventArgs e)
        {
            return await Task.Run(async () =>
            {
                await StudysetView.SendContributorRequest(Studyset, User);
                return true;
            });
        }
        
    }
}
