using Microsoft.AspNetCore.Identity;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class User : IdentityUser
    {

        public List<Studyset> CreatedStudysets { get; set; }
        public List<Studyset> ContributedStudysets { get; set; }

        public List<Studyset> UsersRequestedToBecomeContributors { get; set; }
        public List<UserStudysetConnection> StudysetConnections { get; set; }

       


        public User()
        {
            StudysetConnections = new List<UserStudysetConnection>();
            CreatedStudysets = new List<Studyset>();
            ContributedStudysets = new List<Studyset>();
        }
    }
}