using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer;
using Quixduell.ServiceLayer.ServiceLayer;
using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;
using Quixduell.ServiceLayer.DataAccessLayer.Repository;
using Moq;
using Quixduell.ServiceLayer.Services.MailSender;
using Quixduell.ServiceLayer.Tests.Helpers;


namespace Quixduell.ServiceLayer.Tests.ServiceLayer
{
    internal class StudysetViewTests
    {

        private StudysetView _sut;
        private AppDatabaseContext<User> _sutContext;
        private StudysetDataAccess _sutDataAccess;
        private StudysetFakeData _fakeData;

        private bool mailSended = false;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<AppDatabaseContext<User>>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _sutContext = new AppDatabaseContext<User>(options);

            _fakeData = await StudysetFakeData.GenerateFakeDatainDatabase(_sutContext, false);
            mailSended = false;

            var mockMailSender = new Mock<IMailSender>();
            mockMailSender.Setup(o => o.SendMailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Callback<string, string, string, string, bool>((from, to, sub, body, html) =>
                { Console.WriteLine($"Mail sended to: {to} with sub {sub} and text; {body}"); mailSended = true; });

            var mockNavManager = new Mock<MockNavigationManager>();


            _sutDataAccess = new StudysetDataAccess(new DBConnectionFactory(_sutContext));
            _sut = new StudysetView(_sutDataAccess, mockMailSender.Object,mockNavManager.Object) ;
        }

        [TearDown]
        public void Close()
        {
            _sutContext.Dispose();
        }

        [Test]
        public async Task GetStudyset1_WhenStar_ReturnOneItemWithStarAttribute()
        {
            //Arrange
            var user = _fakeData.LikeUser;
            var studyset = _fakeData.Studyset1;

            //Act 
            await _sut.StarStudysetAsync(studyset,null,user);

            var updatedStudyset = _sutContext.Studysets.Single(o => o.Name == _fakeData.Studyset1.Name);


            //Assert
            Assert.That(updatedStudyset.Connections,Has.Count.EqualTo(1));
            Assert.That(updatedStudyset.Connections.First().IsStored, Is.True);
        }

        [Test]
        public async Task GetStudyset1_WhenUnStar_ReturnOneItemWithNoStarAttribute()
        {
            //Arrange
            var user = _fakeData.LikeUser;
            var studyset = _fakeData.Studyset1;
            await _sut.StarStudysetAsync(studyset, null, user);
            studyset = _sutContext.Studysets.Single(o => o.Name == _fakeData.Studyset1.Name);

            //Act 
            await _sut.StarStudysetAsync(studyset, studyset.Connections.First(), user);
            var updatedStudyset = _sutContext.Studysets.Single(o => o.Name == _fakeData.Studyset1.Name);


            //Assert
            Assert.That(updatedStudyset.Connections, Has.Count.EqualTo(1));
            Assert.That(updatedStudyset.Connections.First().IsStored, Is.False);
        }

        [Test]
        [TestCase("Supper Sache", float.MaxValue, true)]
        [TestCase("Sehr Schlecht", float.MinValue, false)]
        public async Task GetStudyset1_WhenRated_ReturnOneItemWithRateValue(string rateText, float rating, bool isStored)
        {
            //Arrange
            var user = _fakeData.LikeUser;
            var studyset = _fakeData.Studyset1;
            studyset.Connections.Add(new UserStudysetConnection(user, studyset, isStored));
            studyset = _sutContext.Studysets.Single(o => o.Name == _fakeData.Studyset1.Name);

            //Act 

            await _sut.RateAsync(studyset, studyset.Connections.First(), user, rating, rateText);

            var updatedStudyset = _sutContext.Studysets.Single(o => o.Name == _fakeData.Studyset1.Name);


            //Assert
            Assert.That(updatedStudyset.Connections, Has.Count.EqualTo(1));
            Assert.That(updatedStudyset.Connections.First().Rating, Is.Not.Null);
            Assert.That(updatedStudyset.Connections.First().Rating.Description, Is.EqualTo(rateText));
            Assert.That(updatedStudyset.Connections.First().Rating.Value, Is.EqualTo(rating));
        }


        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public async Task SendContributorRequest_WhenRequested_ReturnMailToUser(bool isStored)
        {
            //Arrange
            var user = _fakeData.LikeUser;
            var studyset = _fakeData.Studyset1;
            studyset.Connections.Add(new UserStudysetConnection(user, studyset, isStored));
            studyset = _sutContext.Studysets.First(o => o.Name == _fakeData.Studyset1.Name);

            //Act 
            await _sut.SendContributorRequest(studyset, user);
            var updatedStudyset = _sutContext.Studysets.Single(o => o.Name == _fakeData.Studyset1.Name);

            //Assert
            Assert.That(mailSended,Is.True);
            Assert.That(updatedStudyset.UsersRequestedToBecomeContributor.Count(), Is.EqualTo(1));

        }

    }
}
