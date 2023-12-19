
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.Shared.UserComponent
{
    public partial class SelectUnselectUsers
    {
        [Inject]
        public IServiceProvider ServiceProvider { get; set; } = default!;

        [Inject]
        public UserManager<User> UserManager { get; set; } = default!;

        [Parameter]
        public List<User>? Value { get; set; }

        [Parameter]
        public EventCallback<List<User>> ValueChanged { get; set; }

        private List<User>? _displayedAppUsers;
        private List<User>? _appUsers;
        private string _searchString = "";


        protected override Task OnInitializedAsync()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
                _appUsers = userManager!.Users.ToList();
                SearchUser();
            }
            return base.OnInitializedAsync();
        }

        protected override void OnParametersSet()
        {
            if (Value is null) 
            {
                Value = new List<User>();
            }
            base.OnParametersSet();
        }

        private void SearchUser()
        {
            if (_appUsers is not null)
            {
                _displayedAppUsers = _appUsers.FindAll(o => (o.Email ?? "").ToLower().Contains(_searchString.ToLower()));
                StateHasChanged();
            }
        }

        private async Task DeleteUser(User appUser)
        {
            var user = UserManager.Users.First(o => o.Id == appUser.Id);
            Value?.Remove(user);
            await ValueChanged.InvokeAsync(Value);
        }


        private async Task AddUser(User appUser)
        {
            var user = UserManager.Users.First(o => o.Id == appUser.Id);
            if (!Value.Contains(user)) 
            {
                Value?.Add(user);
                await ValueChanged.InvokeAsync(Value);
            }
        }
    }
}
