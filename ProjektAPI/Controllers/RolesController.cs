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
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RolesController(IMapper mapper, IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _repository = roleRepository;
            _userRepository = userRepository;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRole()
        {
            var roles = await _repository.GetAllAsync();
            var records = _mapper.Map<List<RoleDto>>(roles);
            return Ok(records);
        }
        // GET: api/RolesWithUsers
        [Route("RoleWithUsers")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleWithUsersDto>>> GetRoleWithUsers()
        {
            var roles = await _repository.GetAllAsync();
            var roleDtos = _mapper.Map<List<RoleDto>>(roles);
            var roleWithUsersDtos = new List<RoleWithUsersDto>();

            foreach (var roleDto in roleDtos)
            {
                var users = await _userRepository.GetUsersByRoleId(roleDto.RoleId);
                var userDtos = _mapper.Map<List<UserDto>>(users);

                var roleWithUsersDto = new RoleWithUsersDto
                {
                    RoleId = roleDto.RoleId,
                    Name = roleDto.Name,
                    Description = roleDto.Description,
                    Users = userDtos
                };

                roleWithUsersDtos.Add(roleWithUsersDto);
            }

            return Ok(roleWithUsersDtos);
        }


        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRole(int id)
        {
            var role = await _repository.GetAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return _mapper.Map<RoleDto>(role);
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, RoleDto roleDto)
        {
            if (id != roleDto.RoleId)
            {
                return BadRequest("Invalid Record Id");
            }

            var role = await _repository.GetAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            _mapper.Map(role, role);
            try
            {
                await _repository.UpdateAsync(role);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RoleExists(id))
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

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(RoleDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            await _repository.AddAsync(role);
            return CreatedAtAction("GetRole", new { id = role.RoleId }, role);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _repository.GetAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> RoleExists(int id)
        {
            return await _repository.Exists(id);
        }
    }
}
