using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Dtos
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Icon { get; set; }
        public string? Color { get; set; }
        public int? UserId { get; set; }
    }
}
