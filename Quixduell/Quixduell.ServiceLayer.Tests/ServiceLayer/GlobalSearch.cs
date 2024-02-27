using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;
using Quixduell.ServiceLayer.ServiceLayer;
using Quixduell.ServiceLayer.DataAccessLayer.Repository;


namespace Quixduell.ServiceLayer.Tests.ServiceLayer
{
    public class Tests
    {

        private GlobalSearch _sut;
        private AppDatabaseContext<User> _sutContext;

        private User creator;
        private User contributor1;
        private User contributor2;
        private User likeUser;


        private Studyset Studyset1;
        private Studyset Studyset2;
        private Studyset Studyset3;

        public Tests()
        {

        }

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<AppDatabaseContext<User>>().UseSqlServer("Server=localhost,4500;Database=QuixDB;Persist Security Info=False;User ID=sa;Password=bZYu04XMuyMqXWAcq9;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;").Options;
            //UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            

            using (var context = new AppDatabaseContext<User>(options))
            {
                context.Database.EnsureDeleted();
                context.Database.Migrate();

                creator = new User() { Email = "test.creator@iu-study.com" };
                contributor1 = new User() { Email = "test.contributor1@iu-study.com" };
                contributor2 = new User() { Email = "test.contributor2@iu-study.com" };
                likeUser = new User() { Email = "test.likeUser@iu-study.com" };

                context.Users.Add(creator);
                context.Users.Add(contributor1);
                context.Users.Add(contributor2);
                context.Users.Add(likeUser);


                Studyset1 = new Studyset("IT- Recht", new Category("Recht"), creator, new List<User>() { contributor1, contributor2 }, new List<DataAccessLayer.Model.Questions.BaseQuestion>(), new List<UserStudysetConnection>());                
                Studyset2 = new Studyset("Statistik", new Category("Mathe"), creator, new List<User>() { contributor1 }, new List<DataAccessLayer.Model.Questions.BaseQuestion>(), new List<UserStudysetConnection>());
                Studyset3 = new Studyset("Programmierung mit Java EE", new Category("Java"), creator, new List<User>() { contributor1 }, new List<DataAccessLayer.Model.Questions.BaseQuestion>(), new List<UserStudysetConnection>());
                
                context.Studysets.Add(Studyset1);
                context.Studysets.Add(Studyset2);
                context.Studysets.Add(Studyset3);


                await context.SaveChangesAsync();
            }


            _sutContext = new AppDatabaseContext<User>(options);
            _sut = new GlobalSearch(new StudysetDataAccess(new DBConnectionFactory(_sutContext)));

        }

        [Test]
        public async Task GetAll_WhenCalled_ReturnAllItems()
        {
            //Act 
            var items = await _sut.Search();

            //Assert
            Assert.That(items, Has.Count.EqualTo(3));
        }

        [Test]
        public async Task GetAllForContributor1_WhenCalled_ReturnAllItems()
        {
            //Act 
            var items = await _sut.Search(null, contributor1);

            //Assert
            Assert.That(items, Has.Count.EqualTo(3));
        }

        [Test]
        public async Task GetAllForContributor1_WhenCalled_ReturnOneItem()
        {
            //Act 
            var items = await _sut.Search(null, contributor2);

            //Assert
            Assert.That(items, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task GetAll_WhenLiked_ReturnTwoItems()
        {
            //Arrange
            var user = _sutContext.Users.First( o => o.Email == likeUser.Email );
            var study1 = _sutContext.Studysets.First(o => o.Name == Studyset1.Name);
            var study2 = _sutContext.Studysets.First(o => o.Name == Studyset2.Name);
            await _sut.NoticeStudyset(study1, user);
            await _sut.NoticeStudyset(study2, user);

            //Act 
            var items = await _sut.Search(null, likeUser);

            //Assert
            Assert.That(items, Has.Count.EqualTo(2));
        }
    }
}