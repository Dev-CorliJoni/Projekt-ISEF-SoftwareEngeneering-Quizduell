using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer;

namespace Quixduell.ServiceLayer.Tests.Helpers
{
    internal class StudysetFakeData
    {

        public User Creator { get; set; }
        public User Contributor1 { get; set; }
        public User Contributor2 { get; set; }
        public User LikeUser { get; set; }


        public Studyset Studyset1 { get; set; }
        public Studyset Studyset2 { get; set; }
        public Studyset Studyset3 { get; set; }

        public Category Category1 { get; set; }
        public Category Category2 { get; set; }
        public Category Category3 { get; set; }


        private StudysetFakeData() { }
        public static async Task<StudysetFakeData> GenerateFakeDatainDatabase(AppDatabaseContext<User> context, bool isSQLDB)
        {

            var data = new StudysetFakeData();


            if (isSQLDB)
            {
                context.Database.EnsureDeleted();
                context.Database.Migrate();
            }

            data.Creator = new User() { Email = "test.creator@iu-study.com" };
            data.Contributor1 = new User() { Email = "test.contributor1@iu-study.com" };
            data.Contributor2 = new User() { Email = "test.contributor2@iu-study.com" };
            data.LikeUser = new User() { Email = "test.likeUser@iu-study.com" };

            context.Users.Add(data.Creator);
            context.Users.Add(data.Contributor1);
            context.Users.Add(data.Contributor2);
            context.Users.Add(data.LikeUser);
            data.Category1 = new Category("Recht");
            data.Category2 = new Category("Mathe");
            data.Category3 = new Category("Java");

            data.Studyset1 = new Studyset("IT- Recht", data.Category1, data.Creator, new List<User>() { data.Contributor1, data.Contributor2 }, new List<DataAccessLayer.Model.Questions.BaseQuestion>(), new List<UserStudysetConnection>());
            data.Studyset2 = new Studyset("Statistik", data.Category2, data.Creator, new List<User>() { data.Contributor1 }, new List<DataAccessLayer.Model.Questions.BaseQuestion>(), new List<UserStudysetConnection>());
            data.Studyset3 = new Studyset("Programmierung mit Java EE", data.Category3, data.Creator, new List<User>() { data.Contributor1 }, new List<DataAccessLayer.Model.Questions.BaseQuestion>(), new List<UserStudysetConnection>());
            context.Studysets.Add(data.Studyset1);
            context.Studysets.Add(data.Studyset2);
            context.Studysets.Add(data.Studyset3);


            await context.SaveChangesAsync();


            return data;
        }
    }
}
