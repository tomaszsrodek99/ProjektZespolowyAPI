using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Dtos
{
    public class ExpenseDto
    {
        public int ExpenseId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}