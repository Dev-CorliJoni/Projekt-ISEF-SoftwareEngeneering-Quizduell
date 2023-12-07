using Quixduell.ServiceLayer.DataAccessLayer.Model;
using System.ComponentModel.DataAnnotations;

namespace Quixduell.Blazor.Model.LernsetPagesModel
{
    /// <summary>
    /// Request Class for Validation of new Lernset, used to differ between new Lernset and Updated existing
    /// </summary>
    public class CreateLernset : ChangeLernset
    {
        public CreateLernset(AppUser appUser) : base(appUser) { }


    }
}
