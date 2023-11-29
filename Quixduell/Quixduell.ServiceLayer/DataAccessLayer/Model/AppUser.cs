using Microsoft.AspNetCore.Identity;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class AppUser : IdentityUser
    {
        public List<Lernset> CreatedLernsets { get; set; } = new List<Lernset>();
        public List<Lernset> LernsetPermissions { get; set; } = new List<Lernset>();
        public List<ProcessedQuestion> ProcessedQuestions { get; set; } = new List<ProcessedQuestion>();

    }
}