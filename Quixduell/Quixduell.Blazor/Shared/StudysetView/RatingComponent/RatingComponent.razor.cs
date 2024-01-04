using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace Quixduell.Blazor.Shared.StudysetView.RatingComponent
{
    public partial class RatingComponent
    {
        private bool _isEditable = false;
        private const int _maxRating = 5;
        private float _currentRating = 0;

        [Parameter]
        public bool IsEditable
        {
            get => _isEditable;
            set
            {
                if (_isEditable != value)
                {
                    _isEditable = value;
                    IsEditableChanged.InvokeAsync(value);
                }
            }
        }

        [Parameter]
        public float CurrentRating
        {
            get => _currentRating;
            set
            {
                if (_currentRating != value)
                {
                    _currentRating = value;
                    CurrentRatingChanged.InvokeAsync(value);
                }
            }
        }

        [Parameter]
        public EventCallback<bool> IsEditableChanged { get; set; }

        [Parameter]
        public EventCallback<float> CurrentRatingChanged { get; set; }

        private bool IsStarFilled(int starIndex) => starIndex <= CurrentRating;

        private void ToggleStar(int id)
        {
            if (IsEditable)
            {
                CurrentRating = id;
                CurrentRatingChanged.InvokeAsync(CurrentRating);
            }
        }
    }
}
