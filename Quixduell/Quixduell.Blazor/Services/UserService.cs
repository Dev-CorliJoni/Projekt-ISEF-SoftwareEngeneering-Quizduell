using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quixduell.Blazor.Helpers;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

namespace Quixduell.Blazor.Services
{
    public class UserService
    {


        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly NavigationManager _navigationManager;

        public UserService(AuthenticationStateProvider authenticationStateProvider, NavigationManager navigationManager)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _navigationManager = navigationManager;
        }

        public async Task<User?> GetAuthenticatedUser(UserManager<User> userManager)
        {
            var user = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;

            if (user.Identity!.IsAuthenticated)
            {
                var userWithOutProps = await userManager.GetUserAsync(user);
                return userManager.Users
                    .Include(o => o.StudysetConnections)
                        .ThenInclude(o => o.Studyset)
                            .ThenInclude(o => o.Creator)
                    .Include(o => o.StudysetConnections)
                        .ThenInclude(o => o.Studyset)
                            .ThenInclude(o => o.Questions)
                    .Include(o => o.StudysetConnections)
                        .ThenInclude(o => o.Studyset)
                            .ThenInclude(o => o.Contributors)
                    .Include(o => o.StudysetConnections)
                        .ThenInclude(o => o.Studyset)
                            .ThenInclude(o => o.Category)
                    .Where(o => o.Id == userWithOutProps!.Id)
                    .First();
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

        public async Task<List<User>> LoadUserProposalAsync(UserManager<User> userManager, string name, int amount)
        {
            List<User> users = new List<User>();

            await Task.Run(() =>
            {
                users = userManager.Users.Where(u => u.UserName.Contains(name)).Take(amount).ToList();
            });

            return users;
        }
    }
}
