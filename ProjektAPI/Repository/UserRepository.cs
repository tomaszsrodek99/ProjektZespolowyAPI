﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjektAPI.Contracts;
using ProjektAPI.Dtos;
using ProjektAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProjektAPI.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private IConfiguration _config;
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _config = configuration;
        }

        public async Task<User> GetUserByLogin(UserLoginRequestDto request)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        }

        public async Task<User> Register(UserRegisterRequestDto request)
        {
            if(_context.Users.Any(u => u.Email == request.Email))
            {
                return null;
            }

            CreatePasswordHash(request.Password,
                out byte[] passwordHash,
                out byte[] passwordSalt);

            var user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
        /*public async Task<User> Authenticate(UserLogin user)
{
   User currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Login.ToLower() == user.Login.ToLower() && u.Password == user.Password);

   if (currentUser != null)
       return currentUser;

   return null;
}

public async Task<string> GenerateToken(User user)
{
   var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
   var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

   var claims = new[]
   {
       new Claim(ClaimTypes.NameIdentifier, user.Login),
       new Claim(ClaimTypes.Email, user.Email),
       new Claim(ClaimTypes.GivenName, user.FirstName),
       new Claim(ClaimTypes.Role, user.Role.Name)
   };

   var token = new JwtSecurityToken(_config["Jwt:issuer"], _config["Jwt:Audience"], claims,
       expires: DateTime.Now.AddMinutes(15),
       signingCredentials: credentials);

   return new JwtSecurityTokenHandler().WriteToken(token);
}*/

    }

}
