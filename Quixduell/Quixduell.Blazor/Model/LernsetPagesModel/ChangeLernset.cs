using Quixduell.ServiceLayer.DataAccessLayer.Model;
using System.ComponentModel.DataAnnotations;

namespace Quixduell.Blazor.Model.LernsetPagesModel
{
    public abstract class ChangeLernset
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
        public AppUser Creator { set; get; }


        public string GetContributortoString
        {
            get
            {
                return String.Join(",", Contributor);
            }
        }

        public ChangeLernset(AppUser creator)
        {
            Creator = creator;
        }

    }
}
