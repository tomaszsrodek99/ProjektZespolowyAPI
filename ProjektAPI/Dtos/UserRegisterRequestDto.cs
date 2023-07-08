using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Dtos
{
    public class UserRegisterRequestDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [MinLength(2)]
        [MaxLength(64)]
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        public string LastName { get; set; } = null!;
    }
}
