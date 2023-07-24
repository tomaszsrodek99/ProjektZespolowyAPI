using ProjektAPI.Contracts;
using ProjektAPI.Models;

namespace ProjektAPI.Repository
{
    public class GoalRepository : GenericRepository<Goal>, IGoalRepository
    {
        public GoalRepository(AppDbContext context) : base(context)
        {
        }

    }    
}