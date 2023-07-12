namespace ProjektAPI.Dtos
{
    public class ExpensesByUserByDateResponseDto
    {
        public List<ExpenseDto> Expenses { get; set; }
        public decimal TotalExpense { get; set; }
    }

}
