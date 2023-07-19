using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektAPI.Contracts;
using ProjektAPI.Dtos;
using ProjektAPI.Models;

namespace ProjektAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseRepository _repository;
        private readonly IBudgetRepository _budgetRepository;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public ExpensesController(IMapper mapper, IExpenseRepository expenseRepository, IBudgetRepository budgetRepository, AppDbContext dbContext)
        {
            _mapper = mapper;
            _repository = expenseRepository;
            _budgetRepository = budgetRepository;
            _dbContext = dbContext;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            var expenses = await _repository.GetAllAsync();
            var records = _mapper.Map<List<Expense>>(expenses);
            return Ok(records);
        }

        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseDto>> GetExpense(int id)
        {
            var expense = await _repository.GetAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            return _mapper.Map<ExpenseDto>(expense);
        }

        // PUT: api/Expenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, ExpenseDto expenseDto)
        {
            if (id != expenseDto.ExpenseId)
            {
                return BadRequest("Invalid Record Id");
            }

            var expense = await _repository.GetAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            try
            {
                await _repository.UpdateAsync(expense);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ExpenseExists(id))
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

        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(ExpenseDto expensedDto)
        {
            var expense = _mapper.Map<Expense>(expensedDto);
            await _repository.AddAsync(expense);
            var userId = expense.UserId;
            var totalSpent = await _repository.GetTotalSpentAmountForUser(userId);

            var budget = await _dbContext.Budgets.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            budget.BudgetSpent = totalSpent;
            _dbContext.Update(budget);
            _dbContext.SaveChanges();

            return CreatedAtAction("GetExpense", new { id = expense.ExpenseId }, expense);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _repository.GetAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> ExpenseExists(int id)
        {
            return await _repository.Exists(id);
        }
    }
}
