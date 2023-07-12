using System.ComponentModel.DataAnnotations;

namespace AdminAPI.Dtos
{
    public class AdminDto
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
    }
}
