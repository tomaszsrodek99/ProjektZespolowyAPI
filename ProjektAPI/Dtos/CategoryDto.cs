using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Dtos
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}
