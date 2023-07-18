namespace ProjektAPI.Dtos
{
    public class ExpensesByUserByDateResponseDto
    {
        public List<ExpenseDto> Expenses { get; set; }
        public double TotalExpense { get; set; }
    }

}
