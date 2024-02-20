using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Quixduell.Blazor.Services;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using System.Linq;

namespace Quixduell.Blazor.Shared.ControlComponents
{
    public partial class ProposalTextBoxComponent
    {
        [Inject]
        private UserService UserService { get; set; } = default!;
        [Inject]
        public UserManager<User> UserManager { get; set; } = default!;

        public string ContributorName { get; set; } = "";

        public ElementReference _textbox;

        [Parameter]
        public Func<User, Task> ContributorSelectedAsync { get; set; }

        [Parameter]
        public List<User> ExcludeUsers { get; set; }

        private List<User> ContributorProposal { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await _textbox.FocusAsync();
            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task OnKeyDownAsync(KeyboardEventArgs e)
        {
            string key = e.Key.Length == 1 ? e.Key : "";
            ContributorProposal = (await UserService.LoadUserProposalAsync(UserManager, $"{ContributorName}", 10)).Except(ExcludeUsers).ToList();

            if (new string[] {"NumpadEnter", "Enter"}.Contains(e.Key) && ContributorProposal.Count > 0)
            {
                await ContributorSelectedAsync(ContributorProposal[0]);
            }
        }
    }
}
