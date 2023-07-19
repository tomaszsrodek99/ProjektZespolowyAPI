using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetRepository _repository;
        private readonly IMapper _mapper;

        public BudgetsController(IMapper mapper, IBudgetRepository budgetRepository)
        {
            _mapper = mapper;
            _repository = budgetRepository;
        }

        // GET: api/Budgets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BudgetDto>>> GetBudgets()
        {
            var budgets = await _repository.GetAllAsync();
            var records = _mapper.Map<List<Budget>>(budgets);
            return Ok(records);
        }

        [HttpGet]
        [Route("GetBudgetForUser")]
        public async Task<ActionResult<decimal>> GetBudgetForUser(int userId)
        {
            var budget = await _repository.GetBudgetByUserId(userId);

            if (budget == null)
            {
                return NotFound(); 
            }

            return Ok(budget);
        }

        [HttpGet]
        [Route("GetTotalExpensesForUser")]
        public async Task<ActionResult<decimal>> GetTotalExpensesForUser(int userId)
        {
            var totalExpenses = await _repository.GetTotalExpensesForUser(userId);
            var expenseDtos = _mapper.Map<List<ExpenseDto>>(totalExpenses);
            var total = expenseDtos.Sum(e => e.Price);
            return Ok(new { Expenses = expenseDtos, TotalExpenses = total });
        }

        [HttpGet]
        [Route("GetExpenseSummaries")]
        public async Task<ActionResult<object>> GetExpenseSummaries(int userId)
        {
            var expenses = await _repository.GetTotalExpensesForUser(userId);

            var yearly = expenses.GroupBy(e => e.Date.Year)
                .Select(g => new
                {
                    Year = g.Key,
                    TotalExpenses = g.Sum(e => e.Price),
                    Expenses = g.ToList()
                })
                .ToList();

            var monthly = new List<object>();
            for (int month = 1; month <= 12; month++)
            {
                var monthExpenses = expenses.Where(e => e.Date.Month == month);
                var monthTotalExpenses = monthExpenses.Sum(e => e.Price);
                var weeks = monthExpenses.GroupBy(e => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(e.Date, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                                         .Select(g => new
                                         {
                                             Week = g.Key,
                                             TotalExpenses = g.Sum(e => e.Price),
                                             Expenses = g.ToList()
                                         })
                                         .ToList();

                monthly.Add(new
                {
                    Year = DateTime.Now.Year,
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month),
                    TotalExpenses = monthTotalExpenses,
                    Weeks = weeks
                });
            }

            var currentMonthStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var currentMonthEndDate = currentMonthStartDate.AddMonths(1).AddDays(-1);
            var currentMonthExpenses = expenses.Where(e => e.Date >= currentMonthStartDate && e.Date <= currentMonthEndDate);
            var currentMonthTotalExpenses = currentMonthExpenses.Sum(e => e.Price);

            var daily = new List<object>();
            var startDate = DateTime.Today.AddDays(-6);

            for (int i = 0; i < 7; i++)
            {
                var currentDate = startDate.AddDays(i);
                var dayExpenses = expenses.Where(e => e.Date.Date == currentDate.Date);
                var dayTotalExpenses = dayExpenses.Sum(e => e.Price);

                daily.Add(new
                {
                    Date = currentDate,
                    DayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(currentDate.DayOfWeek),
                    TotalExpenses = dayTotalExpenses,
                    Expenses = dayExpenses.ToList()
                });
            }

            return Ok(new { Yearly = yearly, Monthly = monthly, CurrentMonth = currentMonthTotalExpenses, Daily = daily });
        }



        [HttpGet]
        [Route("GetExpensesByUserByDate")]
        public async Task<ActionResult<ExpensesByUserByDateResponseDto>> GetExpensesByUserByDate(int userId, DateTime startDate, DateTime endDate)
        {
            var expenses = await _repository.GetExpensesByUserAndDate(userId, startDate, endDate);
            var expenseDtos = _mapper.Map<List<ExpenseDto>>(expenses);
            var totalExpense = expenses.Sum(e => e.Price);

            var responseDto = new ExpensesByUserByDateResponseDto
            {
                Expenses = expenseDtos,
                TotalExpense = totalExpense
            };

            return Ok(responseDto);
        }

        [HttpGet]
        [Route("GetTotalSpendingForUserPerPeriod")]
        public async Task<ActionResult<object>> GetTotalSpendingForUserPerPeriod(int userId, int days)
        {
            var lastExpense = await _repository.GetLastExpenseForUserPerPeriod(userId, days);

            if (lastExpense == null)
            {
                return Ok(new { TotalSpending = 0f, Expenses = new List<ExpenseDto>() });
            }

            var startDate = lastExpense.Date.AddDays(-days);
            var expenses = await _repository.GetExpensesByUserAndDate(userId, startDate, lastExpense.Date);
            var totalSpending = expenses.Sum(e => (double)e.Price);

            var expenseDtos = _mapper.Map<List<ExpenseDto>>(expenses);

            return Ok(new { TotalSpending = totalSpending, Expenses = expenseDtos });
        }

        // GET: api/Budgets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BudgetDto>> GetBudget(int id)
        {
            var budget = await _repository.GetAsync(id);
            if (budget == null)
            {
                return NotFound();
            }
            return _mapper.Map<BudgetDto>(budget);
        }

        // PUT: api/Budgets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBudget(int id, Budget budget)
        {
            if (id != budget.BudgetId)
            {
                return BadRequest();
            }

            var bu = await _repository.GetAsync(id);
            if (bu == null)
            {
                return NotFound();
            }
            _mapper.Map(budget, budget);
            try
            {
                await _repository.UpdateAsync(budget);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BudgetExists(id))
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

        // POST: api/Budgets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Budget>> PostBudget(BudgetDto budgetdto)
        {
            var budget = _mapper.Map<Budget>(budgetdto);
            await _repository.AddAsync(budget);

            return CreatedAtAction("GetBudget", new { id = budget.BudgetId }, budget);
        }

        // DELETE: api/Budgets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(int id)
        {
            var budget = await _repository.GetAsync(id);
            if (budget == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> BudgetExists(int id)
        {
            return await _repository.Exists(id);
        }
    }
}
