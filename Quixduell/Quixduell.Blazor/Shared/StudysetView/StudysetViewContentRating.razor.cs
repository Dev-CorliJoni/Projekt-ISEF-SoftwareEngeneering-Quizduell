using Microsoft.AspNetCore.Components;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.Shared.StudysetView
{
    public partial class StudysetViewContentRating
    {
        [Parameter]
        public User User { get; set; }
        [Parameter]
        public Studyset Studyset { get; set; }


        private bool _isEditing;

        public UserStudysetConnection Connection { get => Studyset.Connections.SingleOrDefault(c => c.User == User); }

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

            if (Rating >= 0)
            {
                Connection.Rating.Value = Rating;
                Connection.Rating.Description = RatingText;
                //Update
            }

            return true; // return false if failed
        }

    }
}
