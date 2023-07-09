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
        public string? LastName { get; set; } = null!;

        public int? RoleId { get; set; }

        public RoleDto? Role { get; set; }
    }
}
