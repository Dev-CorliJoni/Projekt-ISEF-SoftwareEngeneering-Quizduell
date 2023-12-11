using System.ComponentModel.DataAnnotations;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class Category : IdModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        private Category() { }

        public Category(string name) : base()
        { 
            Name = name;
        }
    }
}