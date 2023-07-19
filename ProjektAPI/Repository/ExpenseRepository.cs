using Microsoft.EntityFrameworkCore;
using ProjektAPI.Contracts;
using ProjektAPI.Models;

namespace ProjektAPI.Repository
{
    public class ExpenseRepository : GenericRepository<Expense>, IExpenseRepository
    {
        private readonly AppDbContext _context;

        public ExpenseRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<double> GetTotalSpentAmountForUser(int userId)
        {
            var currentDate = DateTime.Now;
            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var expensesInCurrentMonth = await _context.Expenses.Where(
                e => e.Date >= firstDayOfMonth && 
                e.Date <= lastDayOfMonth && 
                e.UserId == userId).ToListAsync();

            var totalExpensesInCurrentMonth = expensesInCurrentMonth.Sum(e => e.Price);
            return totalExpensesInCurrentMonth;
        }
    }
}
