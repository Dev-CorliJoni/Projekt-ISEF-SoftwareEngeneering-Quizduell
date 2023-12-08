using Microsoft.AspNetCore.Identity;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class AppUser : IdentityUser
    {
        public List<Studyset> CreatedStudyset { get; set; } = new List<Studyset>();
        public List<Studyset> StudysetPermissions { get; set; } = new List<Studyset>();
        public List<ProcessedQuestion> ProcessedQuestions { get; set; } = new List<ProcessedQuestion>();

    }
}