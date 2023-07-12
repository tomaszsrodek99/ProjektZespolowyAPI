using ProjektAPI.Models;

namespace ProjektAPI.Contracts
{
    public interface IBudgetRepository : IGenericRepository<Budget>
    {
        public Task<List<Expense>> GetExpensesByUserAndDate(int userId, DateTime startDate, DateTime endDate);
        public Task<Expense> GetLastExpenseForUserPerPeriod(int userId, int days);
        public Task<List<Expense>> GetTotalExpensesForUser(int userId);
    }
}
