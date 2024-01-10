using Microsoft.AspNetCore.Components;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.ServiceLayer;

namespace Quixduell.Blazor.Shared.StudysetView
{
    public partial class StudysetViewContentRating
    {

        [Inject]
        private ServiceLayer.ServiceLayer.StudysetView StudysetView { get; set; } = default!;

        private Studyset _studyset;

        [Parameter]
        public User User { get; set; }

        [Parameter]
        public Studyset Studyset
        {
            get => _studyset;
            set
            {
                _studyset = value;
                Connection = Studyset.Connections.SingleOrDefault(c => c.User == User);
                Connections = Studyset.Connections.Where(c => c != Connection).ToList();

                if (Connection != null && Connection.Rating != null)
                {
                    Rating = Connection.Rating.Value;
                    RatingText = Connection.Rating.Description;
                }
            }
        }


        private bool _isEditing;

        public UserStudysetConnection Connection { get; set; }
        public List<UserStudysetConnection> Connections { get; set; }

        public float Rating { get; set; } = 0;
        public string RatingText { get; set; }

        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
            }
        }

        private async Task<bool> Rate(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            if (Rating == 0)
            {
                return false;
            }

            await StudysetView.RateAsync(Studyset, Connection, User, Rating, RatingText);
            return true; 
        }

    }
}
