using Microsoft.AspNetCore.Identity;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.Services
{
    public class CustomEmailValidator : IUserValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {

            var allowedDomains = new List<string> { "iu-study.org" , "iubh-fernstudium.de" }; // Liste erlaubter Domänen

            var userEmailDomain = user.Email.Split('@').LastOrDefault();

            if (!allowedDomains.Contains(userEmailDomain, StringComparer.OrdinalIgnoreCase))
            {
                return Task.FromResult(IdentityResult.Failed(
                    new IdentityError { Description = $"Email domain is not allowed for registration." }));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
