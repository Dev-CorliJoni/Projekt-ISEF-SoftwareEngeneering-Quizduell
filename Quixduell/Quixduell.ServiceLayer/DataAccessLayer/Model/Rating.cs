using System.ComponentModel.DataAnnotations;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class Rating : IdModel
    {
        [Range(0, 5)]
        public float Value { get; set; }
        public string Description { get; set; }

        public bool IsEmpty { get => Value == 0 && Description == string.Empty; }

        public Rating() 
        {
            Value = 0;
            Description = string.Empty;
        }

        public Rating(float value, string description) : base()
        {
            Value = value;
            Description = description;
        }
    }
}