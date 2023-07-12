using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektAPI.Dtos
{
    public class BudgetDto
    {
        public int BudgetId { get; set; }
        [Required]
        public int UserId { get; set; }
        public UserDto? User { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public decimal BudgetLimit { get; set; }
        public decimal BudgetSpent { get; set; }
        public decimal BudgetRemaining { get; set; }
        public decimal BudgetSpentLast30Days { get; set; }
        public decimal BudgetSpentLast7Days { get; set; }
    }
}
