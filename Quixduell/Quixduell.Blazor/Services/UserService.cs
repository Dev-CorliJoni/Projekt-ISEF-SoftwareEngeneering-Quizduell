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
        private readonly NavigationManager _navigationManager;

        public UserService(AuthenticationStateProvider authenticationStateProvider,  NavigationManager navigationManager)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _navigationManager = navigationManager;
        }

        public async Task<User?> GetAuthenticatedUser(UserManager<User> userManager)
        {
            var user = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;

            if (user.Identity!.IsAuthenticated)
            {
                return await userManager.GetUserAsync(user);
            }
            return null;
        }
        public async Task<User?> GetAuthenticatedUserOrRedirect(UserManager<User> userManager)
        {
            var currentUser = await GetAuthenticatedUser(userManager);

            if (currentUser is not null)
            {
                return currentUser;
            }
            _navigationManager.NavigateTo(PageUri.LoginPage, true);
            return null;
        }



    }
}
