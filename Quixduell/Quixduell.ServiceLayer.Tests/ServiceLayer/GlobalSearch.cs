using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;
using Quixduell.ServiceLayer.ServiceLayer;
using Quixduell.ServiceLayer.DataAccessLayer.Repository;
using Quixduell.ServiceLayer.Tests.Helpers;


namespace Quixduell.ServiceLayer.Tests.ServiceLayer
{
    public class GlobalSearchTests
    {

        private GlobalSearch _sut;
        private AppDatabaseContext<User> _sutContext;

        private StudysetFakeData _fakeData;


        public GlobalSearchTests()
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
            var options = new DbContextOptionsBuilder<AppDatabaseContext<User>>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;



            _sutContext = new AppDatabaseContext<User>(options);

            _fakeData = await StudysetFakeData.GenerateFakeDatainDatabase(context: _sutContext, isSQLDB: false);
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
            var items = await _sut.Search(null, _fakeData.Contributor1);

            //Assert
            Assert.That(items, Has.Count.EqualTo(3));
        }

        [Test]
        public async Task GetAllForContributor2_WhenCalled_ReturnOneItem()
        {
            //Act 
            var items = await _sut.Search(null, _fakeData.Contributor2);

            //Assert
            Assert.That(items, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task GetAll_WhenLiked_ReturnTwoItems()
        {
            //Arrange
            var user =  _fakeData.LikeUser;
            var study1 = _fakeData.Studyset1;
            var study2 = _fakeData.Studyset2;
            await _sut.NoticeStudyset(study1, user);
            await _sut.NoticeStudyset(study2, user);

            //Act 
            var items = await _sut.Search(null, _fakeData.LikeUser);

            //Assert
            Assert.That(items, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task GetAll_WhenLikedUnliked_ReturnOneItem()
        {
            //Arrange
            var user = _fakeData.LikeUser;
            var study1 =_fakeData.Studyset1;
            var study2 = _fakeData.Studyset2;
            await _sut.NoticeStudyset(study1, user);
            await _sut.NoticeStudyset(study2, user);
            await _sut.UnNoticeStudyset(study2, user);

            //Act 
            var items = await _sut.Search(null, _fakeData.LikeUser);

            //Assert
            Assert.That(items, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task GetStudyset1byName_WhenCalled_ReturnOneItem()
        {
            //Act 
            var items = await _sut.Search(_fakeData.Studyset1.Name);

            //Assert
            Assert.That(items, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task GetStudyset1Category_WhenCalled_ReturnOneItem()
        {
            //Act 
            var items = await _sut.Search(null,null, _fakeData.Category1.Name);

            //Assert
            Assert.That(items, Has.Count.EqualTo(1));
        }
    }
}