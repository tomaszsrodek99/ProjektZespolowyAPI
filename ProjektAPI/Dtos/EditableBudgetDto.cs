namespace ProjektAPI.Dtos
{
    public class EditableBudgetDto
    {
        public int UserId { get; set; }
        public int BudgetId { get; set; }
        public double BudgetLimit { get; set; }
        public double BudgetSpent { get; set; }
        public double BudgetRemaining { get; set; }
    }
}
