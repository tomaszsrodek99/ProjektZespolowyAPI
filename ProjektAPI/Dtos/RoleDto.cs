using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Dtos
{
    public class RoleDto
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ICollection<UserDto> Users { get; set; } 
    }
}
