using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using ProjektAPI.Contracts;
using ProjektAPI.Dtos;
using ProjektAPI.Models;
using ProjektAPI.Repository;

namespace ProjektAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IMapper _mapper;
        public GoalsController(IMapper mapper, IGoalRepository goalRepository)
        {
            _mapper = mapper;
            _goalRepository = goalRepository;
        }
        [HttpGet]
        // GET: api/Goals
        public async Task<ActionResult<IEnumerable<GoalDto>>> GetGoals()
        {
            var goals = await _goalRepository.GetAllAsync();
            var records = _mapper.Map<List<GoalDto>>(goals);

            return Ok(records);
        }

        [HttpGet("{id}")]
        // GET: api/Goals/5
        public async Task<ActionResult<GoalDto>> GetGoal(int id)
        {
            var goal = await _goalRepository.GetAsync(id);
            if (goal == null)
            {
                return NotFound();
            }
            return _mapper.Map<GoalDto>(goal);
        }

        [HttpPut("{id}")]
        // PUT: api/Goals/5
        public async Task<IActionResult> PutGoal(int id, GoalDto goalDto)
        {
            if (id != goalDto.Id)
            {
                return BadRequest("Invalid Record Id");
            }

            var goal = await _goalRepository.GetAsync(id);
            if (goal == null)
            {
                return NotFound();
            }
            _mapper.Map(goalDto, goal);
            try
            {
                await _goalRepository.UpdateAsync(goal);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GoalExists(id))
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
        [HttpGet("user/{userId}")]
        // GET: api/Goals/user/1
        public async Task<ActionResult<IEnumerable<GoalDto>>> GetGoalsByUserId(int userId)
        {
            var goals = await _goalRepository.GetAllAsync(); // Assuming this method returns all goals
            var userGoals = goals.Where(g => g.UserId == userId);
            var records = _mapper.Map<List<GoalDto>>(userGoals);

            return Ok(records);
        }
        [HttpPost]
        // POST: api/Goals
        public async Task<ActionResult<Goal>> PostGoal(GoalDto goalDto)
        {
            var goal = _mapper.Map<Goal>(goalDto);
            await _goalRepository.AddAsync(goal);
            return CreatedAtAction("GetGoal", new { id = goal.Id }, goal);
        }
        // DELETE: api/Goals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoal(int id)
        {
            var goal = await _goalRepository.GetAsync(id);
            if (goal == null)
            {
                return NotFound();
            }
            await _goalRepository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> GoalExists(int id)
        {
            return await _goalRepository.Exists(id);
        }
    }
}
