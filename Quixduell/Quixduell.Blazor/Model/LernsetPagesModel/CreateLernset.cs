using Quixduell.ServiceLayer.DataAccessLayer.Model;
using System.ComponentModel.DataAnnotations;

namespace Quixduell.Blazor.Model.LernsetPagesModel
{
    /// <summary>
    /// Request Class for Validation of new Lernset
    /// </summary>
    public class CreateLernset
    {
        [Required]
        public Category? Category { get; set; }
        public List<AppUser> Contributor { get; set; } = new List<AppUser>();


        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; } = String.Empty;

        [MinLength(1)]
        public List<Question> Questions { get; set; } = new List<Question>();

        [Required]
        public AppUser Creator { get;}


        public string GetContributortoString
        {
            get
            {
                return String.Join(",", Contributor);
            }
        }

        public CreateLernset(AppUser creator)
        {

            Creator = creator;

        }

    }
}
