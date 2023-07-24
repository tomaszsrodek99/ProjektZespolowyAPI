using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Dtos
{
    public class GoalDto
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z][A-Za-z ]*$")]
        public string Name { get; set; } = null!;
        public string? Icon { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public double GoalAmount { get; set; }
        public double ActualAmount { get; set; }
    }
}
