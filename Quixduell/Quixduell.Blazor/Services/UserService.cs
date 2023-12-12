using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Quixduell.Blazor.Helpers;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.Services
{
    public class UserService
    {


        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly UserManager<User> _userManager;
        private readonly NavigationManager _navigationManager;

        public UserService(AuthenticationStateProvider authenticationStateProvider, UserManager<User> userManager, NavigationManager navigationManager)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _userManager = userManager;
            _navigationManager = navigationManager;
        }

        public async Task<User?> GetAuthenticatedUser()
        {
            var user = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;

            if (user.Identity!.IsAuthenticated)
            {
                return await _userManager.GetUserAsync(user);
            }
            return null;
        }
        public async Task<User?> GetAuthenticatedUserOrRedirect()
        {
            var currentUser = await GetAuthenticatedUser();

            if (currentUser is not null)
            {
                return currentUser;
            }
            _navigationManager.NavigateTo(PageUri.LoginPage, true);
            return null;
        }



    }
}
