using ProjektAPI.Contracts;
using ProjektAPI.Models;

namespace ProjektAPI.Repository
{
    public class PrivateCategoryRepository : GenericRepository<PrivateCategory>, IPrivateCategoryRepository
    {
        public PrivateCategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
