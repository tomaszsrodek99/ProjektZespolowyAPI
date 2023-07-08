using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektAPI.Models
{
    [Table("Expenses")]
    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }

        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Column(TypeName="decimal(5,2)")]
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime Date { get; set; } 
        public int CategoryId { get; set; } 
        public Category? Category { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
