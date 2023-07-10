using ProjektAPI.Models;

namespace ProjektAPI.Contracts
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        public Task<Role?> GetRoleByUserId(int? roleId);
    }
}
