using System;
using System.Collections.Generic;
using System.Linq;
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
            if(bu == null)
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
