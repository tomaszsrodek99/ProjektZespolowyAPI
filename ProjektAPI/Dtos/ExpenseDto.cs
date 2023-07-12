using ProjektAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Dtos
{
    public class ExpenseDto
    {
        [Required]
        public int ExpenseId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public CategoryDto? Category { get; set; }
    }
}