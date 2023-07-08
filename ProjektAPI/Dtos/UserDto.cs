using ProjektAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Dtos
{
    // Do zmiany klasa
    public class UserDto
    {
        public int UserId { get; set; }

        [MaxLength(128)]
        [Required]
        public string Login { get; set; } = null!;

        [MaxLength(128)]
        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;

        [MinLength(9)]
        [MaxLength(256)]
        [Required]
        public string Password { get; set; } = null!;
        [MinLength(2)]
        [MaxLength(64)]
        [Required]
        public string FirstName { get; set; } = null!;

        [MinLength(2)]
        [MaxLength(64)]
        [Required]
        public string? LastName { get; set; } = null!;
    }
}
