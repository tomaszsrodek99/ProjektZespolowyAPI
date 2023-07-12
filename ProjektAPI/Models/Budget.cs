using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektAPI.Models
{
    public class Budget
    {
        [Key]
        public int BudgetId { get; set; }

        [Required]
        public int UserId { get; set; }
        public  virtual User User { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal BudgetLimit { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal BudgetSpent { get; set; }

        [NotMapped]
        public decimal BudgetRemaining => BudgetLimit - BudgetSpent;
    }
}
