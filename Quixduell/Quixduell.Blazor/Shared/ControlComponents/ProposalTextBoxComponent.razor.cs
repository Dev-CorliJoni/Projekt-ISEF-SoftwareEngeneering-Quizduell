using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Quixduell.Blazor.Services;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.Shared.ControlComponents
{
    public partial class ProposalTextBoxComponent
    {
        [Inject]
        private UserService UserService { get; set; } = default!;
        [Inject]
        public UserManager<User> UserManager { get; set; } = default!;

        private string _contributorName = "";

        [Parameter]
        public Func<User, Task> ContributorSelectedAsync { get; set; }

        private List<User> ContributorProposal { get; set; }

        public async Task OnKeyDownAsync(KeyboardEventArgs e)
        {
            ContributorProposal = await UserService.LoadUserProposalAsync(UserManager, _contributorName, 10);

            if (new string[] {"NumpadEnter", "Enter"}.Contains(e.Key) && ContributorProposal.Count > 0)
            {
                await ContributorSelectedAsync(ContributorProposal[0]);
            }
        }
    }
}
