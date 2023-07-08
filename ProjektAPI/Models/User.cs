using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Login { get; set; } = null!;
        [Required] 
        public string Password { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        public UserType UserType { get; set; }
    }

    public enum UserType
    {
        Normal,
        Admin
    }

}
