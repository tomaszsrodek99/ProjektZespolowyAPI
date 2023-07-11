using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly IPrivateCategoryRepository _privateCategoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(IMapper mapper, ICategoryRepository categoryRepository, IPrivateCategoryRepository privateCategoryRepository)
        {
            _mapper = mapper;
            _repository = categoryRepository;
            _privateCategoryRepository = privateCategoryRepository;
        }
        // GET: api/UserCategories
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetUserCategories(int id)
        {
            var categories = await _repository.GetAllAsync();
            var privateCategories = await _privateCategoryRepository.GetAllAsync();
            var records = _mapper.Map<List<CategoryDto>>(categories);
            var privateRecords = _mapper.Map<List<CategoryDto>>(privateCategories.Where(x=>x.UserId == id));
            var concat = records.Concat(privateRecords);
            return Ok(concat);
        }
        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _repository.GetAllAsync();
            var records = _mapper.Map<List<CategoryDto>>(categories);
            return Ok(records);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _repository.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return _mapper.Map<CategoryDto>(category);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest("Invalid Record Id");
            }

            var user = await _repository.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _mapper.Map(user, user);
            try
            {
                await _repository.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _repository.AddAsync(category);
            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _repository.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> CategoryExists(int id)
        {
            return await _repository.Exists(id);
        }
    }
}
