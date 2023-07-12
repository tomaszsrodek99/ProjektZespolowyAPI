using Microsoft.EntityFrameworkCore;
using ProjektAPI.Contracts;
using ProjektAPI.Models;

namespace ProjektAPI.Repository
{
    public class BudgetRepository : GenericRepository<Budget>, IBudgetRepository
    {
        private readonly AppDbContext _context;
        public BudgetRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Expense>> GetExpensesByUserAndDate(int userId, DateTime startDate, DateTime endDate)
        {
            return await _context.Expenses
                .Where(e => e.UserId == userId && e.Date >= startDate && e.Date <= endDate)
                .ToListAsync();
        }
        public async Task<Expense> GetLastExpenseForUserPerPeriod(int userId, int days)
        {
            var startDate = DateTime.Now.AddDays(-days);
            return await _context.Expenses
                .Where(e => e.UserId == userId && e.Date >= startDate)
                .OrderByDescending(e => e.Date)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Expense>> GetTotalExpensesForUser(int userId)
        {
            return await _context.Expenses.Where(e => e.UserId == userId).ToListAsync();
        }
        public async Task<Budget> GetBudgetByUserId(int userId)
        {
            return await _context.Budgets.FirstOrDefaultAsync(b => b.UserId == userId);
        }
    }
}
