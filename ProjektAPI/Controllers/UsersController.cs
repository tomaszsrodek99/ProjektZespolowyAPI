using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektAPI.Contracts;
using ProjektAPI.Dtos;
using ProjektAPI.Models;
using ProjektAPI.Repository;

namespace ProjektAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IMapper _mapper;

        public UsersController(IMapper mapper, IUserRepository userRepository, IExpenseRepository expenseRepository, IBudgetRepository budgetRepository)
        {
            _mapper = mapper;
            _repository = userRepository;
            _expenseRepository = expenseRepository;
            _budgetRepository = budgetRepository;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _repository.GetAllAsync();
            var records = _mapper.Map<List<UserDto>>(users);

            return Ok(records);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _repository.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return _mapper.Map<UserDto>(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDto userDto)
        {
            if (id != userDto.UserId)
            {
                return BadRequest("Invalid Record Id");
            }

            var user = await _repository.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _mapper.Map(userDto, user);
            try
            {
                await _repository.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _repository.AddAsync(user);
            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _repository.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> UserExists(int id)
        {
            return await _repository.Exists(id);
        }


        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestDto request)
        {
            var user = await _repository.Register(request);
            if (user == null)
                return BadRequest("User already exists");
            else
                return Ok("User successfully created!");
        }
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestDto request)
        {
            var user = await _repository.GetUserByLogin(request.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var passwordValid = _repository.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);
            if (!passwordValid)
            {
                return BadRequest("Invalid credentials");
            }

            double totalSpentAmount = await _expenseRepository.GetTotalSpentAmountForUser(user.UserId);
            var budget = await _budgetRepository.GetBudgetByUserId(user.BudgetId);
            budget.BudgetSpent = totalSpentAmount;
            await _budgetRepository.UpdateAsync(budget);

            string token = _repository.GenerateToken(user);
            return Ok(token);
        }       
    }
}
