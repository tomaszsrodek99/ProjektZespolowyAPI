using ProjektAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }

        [MaxLength(128), Required, EmailAddress]
        public string Email { get; set; } = null!;

        [MinLength(2), MaxLength(64), Required]
        public string FirstName { get; set; } = null!;

        [MinLength(2), MaxLength(64), Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!;
        public int BudgetId { get; set; }
        public BudgetDto Budget { get; set; }
        public ICollection<CategoryDto>? Categories { get; set; }

    }
}
