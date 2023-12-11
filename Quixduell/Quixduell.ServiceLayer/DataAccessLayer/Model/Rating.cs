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
        public float Value { get; set; }
        public string Description { get; set; }

        public Rating(Guid id, float value, string description) : base(id)
        {
            Value = value;
            Description = description;
        }
    }
}