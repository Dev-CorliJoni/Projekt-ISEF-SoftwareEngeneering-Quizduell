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
            get => new List<UserStudysetConnection>
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

        public float GetMarkSize()
        {
            if (Connections.Any())
            {
                return Connections.Max(c => c.Highscore) / 10;
            }

            return 0;

        }

        public IEnumerable<float> HighscoreMarks
        {
            get
            {
                float lowest = GetMarkSize();

                for (int i = 10; i > 0; i--)
                {
                    yield return lowest * i;
                }
            }
        }

        public IEnumerable<string> GetRandomBackgrounds()
        {
            List<string> colors = ["#9b2948", "#E36414", "#5F0F40", "#ffcd74", "green", "#ff7251", "#ffedbf", "brown", "#fecf02", "pink"];
            foreach(var color in Shuffle(colors))
            {
                yield return color;
            }
        }

        static List<T> Shuffle<T>(List<T> list)
        {
            Random random = new Random();

            // Fisher-Yates shuffle algorithm
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }

            return list;
        }
    }
}
