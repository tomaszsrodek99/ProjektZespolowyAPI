using Microsoft.AspNetCore.Mvc;
using ProjektAPI.Dtos;
using ProjektAPI.Models;

namespace ProjektAPI.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public string GenerateToken(User user);
        public Task<ActionResult<User>> Register(UserRegisterRequestDto request);
        public Task<User?> GetUserByLogin(string request);
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        public Task<List<User>> GetUsersByRoleId(string role);
    }
}
