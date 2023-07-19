using ProjektAPI.Models;

namespace ProjektAPI.Contracts
{
    public interface IExpenseRepository : IGenericRepository<Expense>
    {
        public Task<double> GetTotalSpentAmountForUser(int userId);
    }
}
