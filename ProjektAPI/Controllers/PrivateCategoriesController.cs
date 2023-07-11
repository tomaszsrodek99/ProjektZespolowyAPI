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
    public class PrivateCategoriesController : ControllerBase
    {
        private readonly IPrivateCategoryRepository _repository;
        private readonly IMapper _mapper;
        public PrivateCategoriesController(IMapper mapper, IPrivateCategoryRepository privateCategoryRepository)
        {
            _mapper = mapper;
            _repository = privateCategoryRepository;
        }

        // GET: api/PrivateCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrivateCategory>>> GetPrivateCategories()
        {
            var categories = await _repository.GetAllAsync();
            var records = _mapper.Map<List<PrivateCategory>>(categories);
            return Ok(records);
        }

        // GET: api/PrivateCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrivateCategoryDto>> GetPrivateCategory(int id)
        {
            var privateCategory = await _repository.GetAsync(id);
            if (privateCategory == null)
            {
                return NotFound();
            }
            return _mapper.Map<PrivateCategoryDto>(privateCategory);
        }


        // PUT: api/PrivateCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrivateCategory(int id, PrivateCategory privateCategory)
        {
            if (id != privateCategory.PrivateCategoryId)
            {
                return BadRequest();
            }

            var category = await _repository.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _mapper.Map(category, category);
            try
            {
                await _repository.UpdateAsync(category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PrivateCategoryExists(id))
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

        // POST: api/PrivateCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PrivateCategory>> PostPrivateCategory(PrivateCategoryDto privateCategoryDto)
        {
            var privateCategory = _mapper.Map<PrivateCategory>(privateCategoryDto);
            await _repository.AddAsync(privateCategory);

            return CreatedAtAction("GetPrivateCategory", new { id = privateCategory.PrivateCategoryId }, privateCategory);
        }

        // DELETE: api/PrivateCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrivateCategory(int id)
        {
            var privateCategory = await _repository.GetAsync(id);
            if (privateCategory == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> PrivateCategoryExists(int id)
        {
            return await _repository.Exists(id);
        }
    }
}
