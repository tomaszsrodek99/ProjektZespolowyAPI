using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Models
{

    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required] 
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        public int RoleId { get; set; }
    }
}
