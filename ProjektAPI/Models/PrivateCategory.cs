using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Models
{
    public class PrivateCategory
    {
        [Key]
        public int PrivateCategoryId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int UserId { get; set; }
        public string? Icon { get; set; }
        public string? Color { get; set; }
    }
}
