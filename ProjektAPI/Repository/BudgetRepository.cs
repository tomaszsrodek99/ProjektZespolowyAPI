using ProjektAPI.Contracts;
using ProjektAPI.Models;

namespace ProjektAPI.Repository
{
    public class BudgetRepository : GenericRepository<Budget>, IBudgetRepository
    {
        public BudgetRepository(AppDbContext context) : base(context)
        {
        }
    }
}
