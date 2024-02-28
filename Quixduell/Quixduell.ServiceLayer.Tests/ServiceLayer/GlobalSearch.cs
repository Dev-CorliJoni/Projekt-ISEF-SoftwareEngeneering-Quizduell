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

        private Category Category1;
        private Category Category2;
        private Category Category3;

        public Tests()
        {

        }

        [TearDown]
        public void Close()
        {
            _sutContext.Dispose();
        }

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<AppDatabaseContext<User>>()//.UseSqlServer("Server=localhost,4500;Database=QuixDB;Persist Security Info=False;User ID=sa;Password=bZYu04XMuyMqXWAcq9;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;").Options;
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            
            using (var context = new AppDatabaseContext<User>(options))
            {
                //context.Database.EnsureDeleted();
                //context.Database.Migrate();

                creator = new User() { Email = "test.creator@iu-study.com" };
                contributor1 = new User() { Email = "test.contributor1@iu-study.com" };
                contributor2 = new User() { Email = "test.contributor2@iu-study.com" };
                likeUser = new User() { Email = "test.likeUser@iu-study.com" };

                context.Users.Add(creator);
                context.Users.Add(contributor1);
                context.Users.Add(contributor2);
                context.Users.Add(likeUser);

                Category1 = new Category("Recht");
                Category2 = new Category("Mathe");
                Category3 = new Category("Java");

                Studyset1 = new Studyset("IT- Recht", Category1, creator, new List<User>() { contributor1, contributor2 }, new List<DataAccessLayer.Model.Questions.BaseQuestion>(), new List<UserStudysetConnection>());                
                Studyset2 = new Studyset("Statistik", Category2, creator, new List<User>() { contributor1 }, new List<DataAccessLayer.Model.Questions.BaseQuestion>(), new List<UserStudysetConnection>());
                Studyset3 = new Studyset("Programmierung mit Java EE", Category3, creator, new List<User>() { contributor1 }, new List<DataAccessLayer.Model.Questions.BaseQuestion>(), new List<UserStudysetConnection>());
                
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
        public async Task GetAllForContributor2_WhenCalled_ReturnOneItem()
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

        [Test]
        public async Task GetAll_WhenLikedUnliked_ReturnOneItem()
        {
            //Arrange
            var user = _sutContext.Users.First(o => o.Email == likeUser.Email);
            var study1 = _sutContext.Studysets.First(o => o.Name == Studyset1.Name);
            var study2 = _sutContext.Studysets.First(o => o.Name == Studyset2.Name);
            await _sut.NoticeStudyset(study1, user);
            await _sut.NoticeStudyset(study2, user);
            await _sut.UnNoticeStudyset(study2, user);

            //Act 
            var items = await _sut.Search(null, likeUser);

            //Assert
            Assert.That(items, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task GetStudyset1byName_WhenCalled_ReturnOneItem()
        {
            //Act 
            var items = await _sut.Search(Studyset1.Name);

            //Assert
            Assert.That(items, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task GetStudyset1Category_WhenCalled_ReturnOneItem()
        {
            //Act 
            var items = await _sut.Search(null,null,Category1.Name);

            //Assert
            Assert.That(items, Has.Count.EqualTo(1));
        }
    }
}