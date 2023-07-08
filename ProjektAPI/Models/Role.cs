using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Models
{
    public class Role
    {
        [Required]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ICollection<User> Users { get; set;} = new HashSet<User>();
    }
}
