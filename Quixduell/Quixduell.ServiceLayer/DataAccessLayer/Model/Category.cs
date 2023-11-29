using System.ComponentModel.DataAnnotations;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class Category
    {
        [Key]
        public Guid CategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}