using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class Rating : IdModel
    {
        [Range(0, 5)]
        public float? Value { get; set; } = null;
        public string? Description { get; set; } = null;

        public Rating() { }

        public Rating(float value, string description) : base()
        {
            Value = value;
            Description = description;
        }
    }
}