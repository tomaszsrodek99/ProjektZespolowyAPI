using Microsoft.EntityFrameworkCore;
using ProjektAPI.Contracts;
using ProjektAPI.Models;

namespace ProjektAPI.Repository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly AppDbContext _context;
        public RoleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Role> GetRoleByUserId(int roleId)
        {
            return await _context.Roles.FirstOrDefaultAsync(u => u.RoleId == roleId);
        }
    }
}
