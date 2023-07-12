using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Icon { get; set; }
        public string? Color { get; set; }
        public int? UserId { get; set; }
        public virtual ICollection<Expense>? Expenses { get; set; }
    }
}
