// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public User CurrentUser { get; set; }

        public IndexModel(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }



        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            CurrentUser = GetUserWithFullInfo(user);
            return Page();
        }

        private User GetUserWithFullInfo(User user)
        {
            return _userManager.Users
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
                            .Where(o => o.Id == user.Id)
                            .First();
        }

    }
}
