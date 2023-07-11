using ProjektAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Dtos
{
    public class PrivateCategoryDto
    {
        public int PrivateCategoryId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int UserId { get; set; }
        public string? Icon { get; set; }
        public string? Color { get; set; }
    }
}
