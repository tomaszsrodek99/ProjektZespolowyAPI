using Microsoft.AspNetCore.Mvc;
using ProjektAPI.Dtos;
using ProjektAPI.Models;

namespace ProjektAPI.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        //public Task<User> Authenticate(UserLogin user);
        public string GenerateToken(User user);
        public Task<ActionResult<User>> Register(UserRegisterRequestDto request);
        public Task<User> GetUserByLogin(UserLoginRequestDto request);
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        public Task<List<User>> GetUsersByRoleId(int roleId);
    }
}
