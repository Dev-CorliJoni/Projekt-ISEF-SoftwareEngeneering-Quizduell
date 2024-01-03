using Microsoft.AspNetCore.Components;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.Shared.StudysetView
{
    public partial class StudysetViewContentHighscore
    {
        [Parameter]
        public User User { get; set; } = default!;
        [Parameter]
        public Studyset Studyset { get; set; } = default!;

        //public IEnumerable<UserStudysetConnection> Connections
        //{
        //    get  
        //    {
        //        var top = Studyset.Connections.OrderByDescending(c => c.Highscore).Take(10);

        //        if (top.Any(c => c.User == User) == false && Studyset.Connections.Any(c => c.User == User))
        //        {
        //            top = top.Take(9);
        //            top.Append(Studyset.Connections.Single(c => c.User == User));
        //        }

        //        return top;
        //    }
        //}

        public List<UserStudysetConnection> Connections
        {
            get =>  new List<UserStudysetConnection>
                    {
                        new (User, Studyset, false, new Rating(), 10),
                        new (User, Studyset, false, new Rating(), 100),
                        new (User, Studyset, false, new Rating(), 90),
                        new (User, Studyset, false, new Rating(), 50),
                        new (User, Studyset, false, new Rating(), 30),
                        new (User, Studyset, false, new Rating(), 40),
                        new (User, Studyset, false, new Rating(), 80),
                        new (User, Studyset, false, new Rating(), 20),
                        new (User, Studyset, false, new Rating(), 5),
                        new (User, Studyset, false, new Rating(), 10)
                    }
                    .OrderByDescending(c => c.Highscore)
                    .ToList();
        }

        public IEnumerable<float> HighscoreMarks
        {
            get
            {
                float lowest = Connections.Max(c => c.Highscore) / 10;

                for (int i = 10; i > 0; i--)
                {
                    yield return lowest * i;
                }
            }
        }
    }
}
