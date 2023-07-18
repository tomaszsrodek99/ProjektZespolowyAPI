using ProjektAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Dtos
{
    public class BudgetDto
    {
        public int BudgetId { get; set; }
       
  
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public double BudgetLimit { get; set; }
        public double BudgetSpent { get; set; }
        public double BudgetRemaining { get; set; }
    }
}
