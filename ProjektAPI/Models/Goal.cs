namespace ProjektAPI.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Icon { get; set; }
        public DateTime Deadline { get; set; }
        public double GoalAmount { get; set; }
        public double ActualAmount { get; set; }
    }
}
