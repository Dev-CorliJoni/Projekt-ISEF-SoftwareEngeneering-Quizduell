using System.ComponentModel.DataAnnotations;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class Category : IdModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        public Category(Guid id, string name) : base(id)
        { 
            Name = name;
        }
    }
}